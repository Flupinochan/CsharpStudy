using Microsoft.Extensions.DependencyInjection;
using Polly.Fallback;
using Polly.Retry;
using Polly.Timeout;
using Polly;
using System.Net;
using PollyTest.Services;

namespace PollyTest.App_Start;

/// <summary>
/// Polly DI設定クラス (以下のライブラリが必要)
///   1. Microsoft.Extensions.DependencyInjection
///   2. Polly.Extensions
/// </summary>
public class PollyConfig
{
    public static void Configure(IServiceCollection services)
    {
        // RetryやTimeoutを追加する.Addの順番は大事
        // ※Pollyは、AddResiliencePipelineでDIコンテナに追加する
        services.AddResiliencePipeline<String, HttpResponseMessage>("my-pipeline", static (builder, context) =>
        {
            LoggerService logger = context.ServiceProvider.GetRequiredService<LoggerService>();

            // 処理全体(最終結果)のFallback (Retryしてもダメだった場合の最終処理用)
            /// 1. ExecuteAsyncにおける例外処理 (Try-Catchの代わりでresponseのnullを回避するためなどに利用)
            /// 2. ExecuteAsyncにおけるFinallyで最後に実行される処理
            builder.AddFallback(new FallbackStrategyOptions<HttpResponseMessage>
            {
                ShouldHandle = (FallbackPredicateArguments<HttpResponseMessage> fallbackPredicateArguments) =>
                {
                    HttpResponseMessage response = (HttpResponseMessage)fallbackPredicateArguments.Outcome.Result!;
                    Exception exception = (Exception)fallbackPredicateArguments.Outcome.Exception!;

                    /// 例外発生(response が null) の場合、Fallback処理を実行
                    /// ※Timeoutによる例外TimeoutRejectedExceptionも含む
                    if(response == null)
                    {
                        return PredicateResult.True();
                    }
                    /// response が OK(200) でない場合、フォールバックせずそのままresponseを返却
                    else if(response.StatusCode != HttpStatusCode.OK)
                    {
                        return PredicateResult.False();
                    }
                    /// ユーザの操作によってキャンセルされた場合 (HTTPリクエストの完了を待たずに実行される)
                    else if(exception is OperationCanceledException)
                    {
                        return PredicateResult.True();
                    }
                    /// その他の場合
                    else
                    {
                        return PredicateResult.False();
                    }
                },
                OnFallback = (OnFallbackArguments<HttpResponseMessage> OnFallbackArguments) =>
                {
                    /// FallbackAction実行前の処理
                    logger.Debug("Fallback処理を実行します");
                    return default;
                },
                FallbackAction = (FallbackActionArguments<HttpResponseMessage> fallbackActionArguments) =>
                {
                    /// responseを生成して返却
                    logger.Debug("Fallback処理を実行中...");

                    /// Exception or null時 (デフォルトはInternalServerError)
                    HttpResponseMessage response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Content = new StringContent("Fallback処理で生成されたレスポンスです。例外が発生し正常なレスポンスが得られませんでした。")
                    };

                    /// Cancel時 (RequestTimeout)
                    if(fallbackActionArguments.Outcome.Exception is OperationCanceledException)
                    {
                        logger.Info("処理がキャンセルされました");
                        response = new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.RequestTimeout,
                            Content = new StringContent("Fallback処理で生成されたレスポンスです。ユーザによるCancelが実行されました。")
                        };
                    }
                    else
                    {
                        logger.Error("最終的にエラーで処理が終わりました");
                    }

                    return Outcome.FromResultAsValueTask(response);
                }
            })
            // 処理全体のTimeout
            .AddTimeout(new TimeoutStrategyOptions
            {
                Timeout = TimeSpan.FromSeconds(60),
                OnTimeout = (OnTimeoutArguments onTimeoutArguments) =>
                {
                    logger.Error("処理全体でTimeoutが発生しました");
                    return default;
                }
            })
            // リクエスト(Retry)ごとのTimeout設定
            .AddRetry(new RetryStrategyOptions<HttpResponseMessage>
            {
                /// DelayBackoffType.Linear: リトライごとにDelayが倍増していく 3, 6, 9...
                /// UseJitter: Delayが25%増減する
                /// MaxDelay: Linearなどで増加したDelayの最大Delay
                /// Delay: リトライまでの待機時間
                BackoffType = DelayBackoffType.Linear,
                UseJitter = true,
                Delay = TimeSpan.FromSeconds(3),
                MaxDelay = TimeSpan.FromSeconds(20),
                MaxRetryAttempts = 10,
                OnRetry = (OnRetryArguments<HttpResponseMessage> onRetryArguments) =>
                {
                    logger.Debug($"{onRetryArguments.AttemptNumber + 1}回目のリトライを{onRetryArguments.RetryDelay}秒後に実行します");
                    return default;
                },
                ShouldHandle = (RetryPredicateArguments<HttpResponseMessage> retryPredicateArguments) =>
                {
                    HttpResponseMessage response = (HttpResponseMessage)retryPredicateArguments.Outcome.Result!;
                    Exception exception = (Exception)retryPredicateArguments.Outcome.Exception!;

                    if(response == null)
                    {
                        logger.Error($"通信エラー(Timeout or Exception)が発生しました", exception: exception);
                        return PredicateResult.True();
                    }
                    else if(response.StatusCode != HttpStatusCode.OK)
                    {
                        logger.Error($"異常な応答が返ってきました: {response.StatusCode}");
                        return PredicateResult.True();
                    }
                    else
                    {
                        logger.Info($"処理が正常に完了しました: {response.StatusCode}");
                        return PredicateResult.False();
                    }
                },
            })
            // リトライごとに適用されるTimeout
            .AddTimeout(new TimeoutStrategyOptions
            {
                Timeout = TimeSpan.FromSeconds(5),
                OnTimeout = (OnTimeoutArguments onTimeoutArguments) =>
                {
                    logger.Error("リクエスト(Retry)で、Timeoutが発生しました");
                    return default;
                }
            });
            // .AddFallback(xxx) // リトライ(各処理)ごとに適用されるFallback

            // Timeout設定メモ
            /// 1. ExecuteAsyncにおけるTimeout時間
            /// 2. Timeoutしたら例外が発生するため、ExecuteAsyncは、try-catchで囲むか
            /// 3. 例外発生前に、OnTimeout処理が実行される
            /// 4. Timeout専用のCancellationTokenを内部(inner)に所有していて
            ///    Timeoutが発生した場合処理をキャンセルし、処理が完了していなければ例外をスローさせる
        });
    }
}
