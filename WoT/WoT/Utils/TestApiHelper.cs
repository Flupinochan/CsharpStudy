using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Documents;

namespace WoT.Utils
{
    /// <summary>
    /// サンプル
    /// </summary>
    public class TestApiHelper
    {
        private readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="testRequestData">リクエストデータクラス</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Get(TestRequestData testRequestData)
        {
            // リクエストデータクラスからクエリパラメータを作成
            var queryParams = testRequestData.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(testRequestData)?.ToString());

            // リクエストURLとクエリパラメータを結合
            string baseUrl = App.BaseUrl + "account/list/";
            var url = $"{baseUrl}?{string.Join("&", queryParams.Select(p => $"{HttpUtility.UrlEncode(p.Key)}={HttpUtility.UrlEncode(p.Value)}"))}";
            App.Logger.DEBUG($"GETリクエストURL: {url}");

            // GETリクエスト送信
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            
            // レスポンス返却
            return response;
        }


        /// <summary>
        /// POST
        /// WoTのAPIは、POSTもクエリパラメータでデータ送信する必要があるため、以下のようなBodyを利用した通信は失敗するため注意
        /// </summary>
        /// <param name="testRequestData">リクエストデータクラス</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Post(TestRequestData testRequestData)
        {
            // Header設定
            // _httpClient.DefaultRequestHeaders.Add("UserId", "1234");

            // Body設定 (リクエストデータクラスをJSON形式に変換)
            StringContent jsonContent = new StringContent(JsonSerializer.Serialize(testRequestData),
                                                          Encoding.UTF8,
                                                          "application/json");
            string requestBody = await jsonContent.ReadAsStringAsync();
            App.Logger.DEBUG($"POSTリクエストボディ: {requestBody}");

            // POSTリクエスト送信
            HttpResponseMessage response = await _httpClient.PostAsync(App.BaseUrl + "account/list/", jsonContent);

            // レスポンス返却
            return response;
        }
    }


    // ※シリアライズ/デシリアライズするには、Public { get; set; } の自動実装プロパティである必要がある
    // ※フィールド名(applicaton_id)がそのままJSONのキーになる
    /// <summary>
    /// リクエストデータクラス
    /// </summary>
    public class TestRequestData
    {
        public string application_id { get; set; }
        public string search { get; set; }

        public TestRequestData(string application_id, string search)
        {
            this.application_id = application_id;
            this.search = search;
        }
    }


    /// <summary>
    /// 正常時レスポンスデータクラス
    /// </summary>
    public class TestSuccessResponseData
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("meta")]
        public TestMetaData? Meta { get; set; }

        [JsonPropertyName("data")]
        public List<TestDataDetail>? Data { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class TestMetaData
    {
        [JsonPropertyName("count")]
        public int? Count { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class TestDataDetail
    {
        [JsonPropertyName("nickname")]
        public string? Nickname { get; set; }

        [JsonPropertyName("account_id")]
        public int? AccountId { get; set; }

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
        public TestErrorDetails? Error { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class TestErrorDetails
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
