using Polly;
using Polly.Fallback;
using Polly.Retry;
using Polly.Timeout;
using System.Diagnostics;
using System.Net;

class Program
{
    static async Task Main()
    {
        HttpClient httpClient = new HttpClient();
        String url = "https://www.metalmental.net/";
        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);

        // Timeout設定
        // 1. ExecuteAsyncにおけるTimeout時間
        // 2. Timeoutしたら例外が発生するため、ExecuteAsyncは、try-catchで囲むか
        // 3. 例外発生前に、OnTimeout処理が実行される
        TimeoutStrategyOptions timeoutStrategyOptions = new TimeoutStrategyOptions
        {
            Timeout = TimeSpan.FromSeconds(5),
            OnTimeout = (OnTimeoutArguments onTimeoutArguments) =>
            {
                Console.WriteLine("Timeoutが発生しました");
                return default;
            }
        };

        // Retry設定
        RetryStrategyOptions<HttpResponseMessage> retryStrategyOptions = new RetryStrategyOptions<HttpResponseMessage>
        {
            Delay = TimeSpan.FromSeconds(5),
            MaxRetryAttempts = 5,
            OnRetry = (OnRetryArguments<HttpResponseMessage> onRetryArguments) =>
            {
                Console.WriteLine($"{onRetryArguments.AttemptNumber + 1}回目のリトライを実行します");
                return default;
            },
            ShouldHandle = (RetryPredicateArguments<HttpResponseMessage> retryPredicateArguments) =>
            {
                if(retryPredicateArguments.Outcome.Exception is Exception ||
                    retryPredicateArguments.Outcome.Result is HttpResponseMessage response1 && response1 == null)
                {
                    Console.WriteLine("通信エラーが発生しました");
                    return PredicateResult.True();
                }
                else if(retryPredicateArguments.Outcome.Result is HttpResponseMessage response2 && response2.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("異常な応答がありました");
                    return PredicateResult.True();
                }
                else
                {
                    Console.WriteLine("処理が正常に完了しました");
                    return PredicateResult.False();
                }
            },
        };

        // Fallback設定 (Retryしてもダメだった場合の最終処理用)
        // 1. ExecuteAsyncにおける例外処理 (Try-Catchの代わりでresponseのnullを回避するためなどに利用)
        // 2. ExecuteAsyncにおけるFinallyで最後に実行される処理
        FallbackStrategyOptions<HttpResponseMessage> fallbackStrategyOptions = new FallbackStrategyOptions<HttpResponseMessage>
        {
            ShouldHandle = (FallbackPredicateArguments<HttpResponseMessage> fallbackPredicateArguments) =>
            {
                // 例外発生 または response が null の場合(Timeoutによる例外TimeoutRejectedExceptionも含む)、Fallback処理を実行
                if(fallbackPredicateArguments.Outcome.Exception is Exception ||
                    fallbackPredicateArguments.Outcome.Result is HttpResponseMessage response1 && response1 == null)
                {
                    Console.WriteLine("通信エラーが発生しました");
                    return PredicateResult.True();
                }
                // response が OK(200) でない場合、フォールバックせずそのままresponseを返却
                else if(fallbackPredicateArguments.Outcome.Result is HttpResponseMessage response2 && response2.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("異常な応答がありました");
                    return PredicateResult.False();
                }
                // その他の場合
                else
                {
                    Console.WriteLine("処理が正常に完了しました");
                    return PredicateResult.False();
                }
            },
            OnFallback = (OnFallbackArguments<HttpResponseMessage> OnFallbackArguments) =>
            {
                // FallbackAction実行前の処理
                Console.WriteLine("Fallback処理を実行します");
                return default;
            },
            FallbackAction = (FallbackActionArguments<HttpResponseMessage> fallbackActionArguments) =>
            {
                // responseを生成して返却
                Console.WriteLine("Fallback処理を実行中...");
                HttpResponseMessage response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Fallback処理で生成されたレスポンスです")
                };
                return Outcome.FromResultAsValueTask(response);
            }
        };


        // .Addの順番は大事
        ResiliencePipeline<HttpResponseMessage> pipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
            .AddFallback(fallbackStrategyOptions)
            .AddRetry(retryStrategyOptions)
            .AddTimeout(timeoutStrategyOptions)
            .Build();

        HttpResponseMessage response = await pipeline.ExecuteAsync(async (CancellationToken token) =>
        {
            // HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);
            await Task.Delay(100000, token);

            // This causes the action fail, thus using the fallback strategy above
            return httpResponseMessage;
        }, CancellationToken.None);

        Console.WriteLine(response.Content);
        
    }
}