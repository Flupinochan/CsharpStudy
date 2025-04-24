using Microsoft.Extensions.DependencyInjection;
using Polly;
using System.Net;

namespace PollyTest.Services;
public class HttpRequestService
{
    private readonly ResiliencePipeline<HttpResponseMessage> _pipeline;
    private readonly HttpClient _httpClient;
    private readonly LoggerService _logger;

    public HttpRequestService(
        [FromKeyedServices("my-pipeline")]
        ResiliencePipeline<HttpResponseMessage> pipeline,
        HttpClient httpClient,
        LoggerService logger)
    {
        // pipelineは複数登録できるため、キー名を指定して取得する
        _pipeline = pipeline;
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task GetAsync(String url, CancellationToken outerToken)
    {
        HttpResponseMessage response = await _pipeline.ExecuteAsync(async (CancellationToken innerToken) =>
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            Random random = new Random();
            switch(random.Next(0, 10))
            {
                case 0:
                    // Timeout用のinnerTokenを渡すことで、HttpRequestにPollyのTimeoutが適用され
                    // Timeoutした際に、PollyのTimeoutがキャンセルされて例外が発生する
                    httpResponseMessage = await _httpClient.GetAsync(url, innerToken);
                    break;
                case 1:
                case 2:
                case 3:
                    // Timeoutテスト(例外TimeoutRejectedExceptionが発生)
                    await Task.Delay(10000000, innerToken); // Timeout用のinnerTokenを指定
                    break;
                case 4:
                case 5:
                case 6:
                    throw new HttpRequestException("テスト: HttpRequestException例外発生");
                case 7:
                case 8:
                case 9:
                    // 異常なStatusCodeのテスト
                    httpResponseMessage = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Content = new StringContent("テスト: サーバーがエラー応答を返しました"),
                    };
                    break;
            }

            return httpResponseMessage;
        }, outerToken); // ユーザからのキャンセルトークンを指定

        String responseBody = await response.Content.ReadAsStringAsync();
        _logger.Info("[結果]");
        _logger.Info($"[ステータスコード] {response.StatusCode}");
        _logger.Info($"[レスポンス] {responseBody}");
    }
}
