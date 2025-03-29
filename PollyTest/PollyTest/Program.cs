using Polly;
using Polly.Fallback;
using Polly.Retry;
using Polly.Timeout;
using System.Net;

class Program
{
    static async Task Main()
    {
        HttpClient httpClient = new HttpClient();
        String url = "https://www.metalmental.net/";
        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);

        // ユーザによるキャンセル
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken outerToken = cts.Token;
        // 別スレッドでEnterキーが押されたらキャンセルを実行
        Task cancelTask = MonitorEnterKeyPress(cts);


        // Timeout設定
        // 1. ExecuteAsyncにおけるTimeout時間
        // 2. Timeoutしたら例外が発生するため、ExecuteAsyncは、try-catchで囲むか
        // 3. 例外発生前に、OnTimeout処理が実行される
        // 4. Timeout専用のCancellationTokenを内部(inner)に所有していて
        //    Timeoutが発生した場合処理をキャンセルし、処理が完了していなければ例外をスローさせる
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
            // DelayBackoffType.Linear: リトライごとにDelayが倍増していく 3, 6, 9...
            // UseJitter: Delayが25%増減する
            // MaxDelay: Linearなどで増加したDelayの最大Delay
            // Delay: リトライまでの待機時間
            BackoffType = DelayBackoffType.Linear,
            UseJitter = true,
            Delay = TimeSpan.FromSeconds(3),
            MaxDelay = TimeSpan.FromSeconds(20),
            MaxRetryAttempts = 5,
            OnRetry = (OnRetryArguments<HttpResponseMessage> onRetryArguments) =>
            {
                Console.WriteLine($"{onRetryArguments.AttemptNumber + 1}回目のリトライを{onRetryArguments.RetryDelay}秒後に実行します");
                return default;
            },
            ShouldHandle = (RetryPredicateArguments<HttpResponseMessage> retryPredicateArguments) =>
            {
                if(retryPredicateArguments.Outcome.Exception is Exception ex ||
                    retryPredicateArguments.Outcome.Result is HttpResponseMessage response1 && response1 == null)
                {
                    Console.WriteLine($"通信エラーまたはTimeoutが発生しました: {retryPredicateArguments.Outcome.Exception!.GetType().FullName}");
                    return PredicateResult.True();
                }
                else if(retryPredicateArguments.Outcome.Result is HttpResponseMessage response2 && response2.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("異常な応答がありました");
                    Console.WriteLine($"Response: {retryPredicateArguments.Outcome.Result.StatusCode}");
                    return PredicateResult.True();
                }
                else
                {
                    Console.WriteLine("処理が正常に完了しました");
                    Console.WriteLine($"Response: {retryPredicateArguments.Outcome.Result!.StatusCode}");
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
                // 例外発生(response が null) の場合、Fallback処理を実行
                // ※Timeoutによる例外TimeoutRejectedExceptionも含む
                if(fallbackPredicateArguments.Outcome.Exception is Exception ||
                    fallbackPredicateArguments.Outcome.Result is HttpResponseMessage response1 && response1 == null)
                {
                    return PredicateResult.True();
                }
                // response が OK(200) でない場合、フォールバックせずそのままresponseを返却
                else if(fallbackPredicateArguments.Outcome.Result is HttpResponseMessage response2 && response2.StatusCode != HttpStatusCode.OK)
                {
                    return PredicateResult.False();
                }
                // ユーザの操作によってキャンセルされた場合 (HTTPリクエストの完了を待たずに実行される)
                else if(fallbackPredicateArguments.Outcome.Exception is OperationCanceledException)
                {
                    return PredicateResult.True();
                }
                // その他の場合
                else
                {
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

                // Exception or null時 (デフォルトはInternalServerError)
                HttpResponseMessage response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("エラー発生: Fallback処理で生成されたレスポンスです")
                };

                // Cancel時 (RequestTimeout)
                if(fallbackActionArguments.Outcome.Exception is OperationCanceledException)
                {
                    Console.WriteLine("処理がキャンセルされました");
                    response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.RequestTimeout,
                        Content = new StringContent("Cancel実行: Fallback処理で生成されたレスポンスです")
                    };
                }
                else
                {
                    Console.WriteLine("最終的にエラーで処理が終わりました");
                }

                    return Outcome.FromResultAsValueTask(response);

            }
        };


        // .Addの順番は大事
        ResiliencePipeline<HttpResponseMessage> pipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
            .AddFallback(fallbackStrategyOptions)
            .AddRetry(retryStrategyOptions)
            .AddTimeout(timeoutStrategyOptions)
            .Build();

        HttpResponseMessage response = await pipeline.ExecuteAsync(async (CancellationToken innerToken) =>
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            //httpResponseMessage = await httpClient.GetAsync(url);

            Random rand = new Random();
            if(rand.Next(7) == 0)
            {
                // Timeout用のinnerTokenを渡すことで、HttpRequestにPollyのTimeoutが適用され
                // Timeoutした際に、PollyのTimeoutがキャンセルされて例外が発生する
                httpResponseMessage = await httpClient.GetAsync(url, innerToken);
            }
            else if(rand.Next(3) == 0)
            {
                // Timeoutテスト(例外TimeoutRejectedExceptionが発生)
                await Task.Delay(10000000, innerToken); // Timeout用のinnerTokenを指定
            }
            else if(rand.Next(2) == 0)
            {
                throw new HttpRequestException("テスト: HttpRequestException例外発生");
            }
            else
            {
                // 異常なStatusCodeのテスト
                httpResponseMessage = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("テスト: サーバーがエラー応答を返しました"),
                };
            }

            return httpResponseMessage;
        }, outerToken); // ユーザからのキャンセルトークンを指定

        Console.WriteLine("結果");
        Console.WriteLine($"レスポンス: {response.Content}");
        Console.WriteLine($"ステータスコード: {response.StatusCode}");

    }


    // Enterキーが押されたらキャンセルする処理
    static async Task MonitorEnterKeyPress(CancellationTokenSource cts)
    {
        await Task.Run(() =>
        {
            Console.WriteLine("Enter キーを押すとキャンセルされます...");
            while(true)
            {
                if(Console.KeyAvailable && Console.ReadKey(intercept: true).Key == ConsoleKey.Enter)
                {
                    cts.Cancel();
                    break;
                }
            }
        });
    }
}