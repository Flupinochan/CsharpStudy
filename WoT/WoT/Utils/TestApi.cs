using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace WoT.Utils;

/// <summary>
/// サンプル
/// </summary>
public class TestApi
{
    /// <summary>
    /// GET
    /// </summary>
    /// <param name="testRequestData">リクエストデータクラス</param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> Get(TestRequestData testRequestData)
    {
        using(HttpClient httpClient = new HttpClient())
        {
            // リクエストデータクラスからクエリパラメータを作成
            Dictionary<String, String?> queryParams = testRequestData.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(testRequestData)?.ToString());

            // リクエストURLとクエリパラメータを結合
            String baseUrl = App.BaseUrl + "account/list/";
            String url = $"{baseUrl}?{String.Join("&", queryParams.Select(p => $"{HttpUtility.UrlEncode(p.Key)}={HttpUtility.UrlEncode(p.Value)}"))}";
            App.Logger.DEBUG($"GETリクエストURL: {url}");

            // GETリクエスト送信
            HttpResponseMessage response = await httpClient.GetAsync(url);

            // レスポンス返却
            return response;
        }
    }


    /// <summary>
    /// POST
    /// WoTのAPIは、POSTもクエリパラメータでデータ送信する必要があるため、以下のようなBodyを利用した通信は失敗するため注意
    /// </summary>
    /// <param name="testRequestData">リクエストデータクラス</param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> Post(PlayersRequestData testRequestData)
    {
        using(HttpClient httpClient = new HttpClient())
        {
            // Header設定
            // _httpClient.DefaultRequestHeaders.Add("UserId", "1234");

            // Body設定 (リクエストデータクラスをJSON形式に変換)
            StringContent jsonContent = new StringContent(JsonSerializer.Serialize(testRequestData),
                                                      Encoding.UTF8,
                                                      "application/json");
            String requestBody = await jsonContent.ReadAsStringAsync();
            App.Logger.DEBUG($"POSTリクエストボディ: {requestBody}");

            // POSTリクエスト送信
            HttpResponseMessage response = await httpClient.PostAsync(App.BaseUrl + "account/list/", jsonContent);

            // レスポンス返却
            return response;
        }
    }
}


// ※シリアライズ/デシリアライズするには、Public { get; set; } の自動実装プロパティである必要がある
// ※フィールド名(applicaton_id)がそのままJSONのキーになる
/// <summary>
/// リクエストデータクラス
/// </summary>
public class TestRequestData
{
    public String application_id { get; set; }
    public String search { get; set; }

    public TestRequestData(String application_id, String search)
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
    public String? Status { get; set; }

    [JsonPropertyName("meta")]
    public TestMetaData? Meta { get; set; }

    [JsonPropertyName("data")]
    public List<TestDataDetail>? Data { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}

public class TestMetaData
{
    [JsonPropertyName("count")]
    public Int32? Count { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}

public class TestDataDetail
{
    [JsonPropertyName("nickname")]
    public String? Nickname { get; set; }

    [JsonPropertyName("account_id")]
    public Int32? AccountId { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}


/// <summary>
/// エラー時レスポンスデータクラス
/// </summary>
public class TestErrorResponseData
{
    [JsonPropertyName("status")]
    public String? Status { get; set; }

    [JsonPropertyName("error")]
    public TestErrorDetails? Error { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}

public class TestErrorDetails
{
    [JsonPropertyName("field")]
    public String? Field { get; set; }

    [JsonPropertyName("message")]
    public String? Message { get; set; }

    [JsonPropertyName("code")]
    public Int32? Code { get; set; }

    [JsonPropertyName("value")]
    public String? Value { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}