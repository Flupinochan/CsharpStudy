using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WoT.Utils
{
    public class TestApiHelper
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<HttpResponseMessage> Post(TestRequestData testRequestData)
        {
            // Header設定
            _httpClient.DefaultRequestHeaders.Add("UserId", "1234");

            // Body設定
            StringContent jsonContent = new StringContent(JsonSerializer.Serialize(testRequestData),
                                                          Encoding.UTF8,
                                                          "application/json");

            // POSTリクエスト送信
            HttpResponseMessage response = await _httpClient.PostAsync(App.Url + "account/list/", jsonContent);

            // レスポンス返却
            return response;
        }
    }
    
    // ※シリアライズ/デシリアライズするにはPublicである必要がある
    /// <summary>
    /// リクエストデータクラス
    /// </summary>
    public class TestRequestData
    {
        public string ApiKey;
        public string SecretKey;

        public TestRequestData(string apiKey, string secretKey)
        {
            ApiKey = apiKey;
            SecretKey = secretKey;
        }
    }


    /// <summary>
    /// 正常時レスポンスデータクラス
    /// </summary>
    public class TestSuccessResponseData
    {
        [JsonPropertyName("account_id")]
        public int? AccountId { get; set; }

        [JsonPropertyName("nickname")]
        public string? NickName { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }


    /// <summary>
    /// エラー時レスポンスデータクラス
    /// </summary>
    public class TestErrorResponseData
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("error")]
        public ErrorDetails? Error { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class ErrorDetails
    {
        [JsonPropertyName("field")]
        public string? Field { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("code")]
        public int? Code { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }



}
