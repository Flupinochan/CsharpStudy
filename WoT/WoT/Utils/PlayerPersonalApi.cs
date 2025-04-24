using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace WoT.Utils;

class PlayerPersonalApi
{
    /// <summary>
    /// プレイヤーIDから様々なプレイヤー情報を取得する
    /// </summary>
    /// <param name="playerPersonalRequestData"></param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> Get(PlayerPersonalRequestData playerPersonalRequestData)
    {
        using(HttpClient httpClient = new HttpClient())
        {
            // リクエストデータクラスからクエリパラメータを作成
            Dictionary<String, String?> queryParams = playerPersonalRequestData.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(playerPersonalRequestData)?.ToString());

            // リクエストURLとクエリパラメータを結合
            String baseUrl = App.BaseUrl + "account/info/";
            String url = $"{baseUrl}?{String.Join("&", queryParams.Select(p => $"{HttpUtility.UrlEncode(p.Key)}={HttpUtility.UrlEncode(p.Value)}"))}";
            App.Logger.DEBUG($"GETリクエストURL: {url}");

            // GETリクエスト送信
            HttpResponseMessage response = await httpClient.GetAsync(url);

            // レスポンス返却
            return response;
        }
    }
}


/// <summary>
/// リクエストデータクラス
/// </summary>
public class PlayerPersonalRequestData
{
    public String application_id { get; set; }
    public Int32 account_id { get; set; }
    public String language { get; set; }
    public String fields { get; set; }

    public PlayerPersonalRequestData(String application_id, Int32 account_id)
    {
        this.application_id = application_id;
        this.account_id = account_id;
        this.language = "en";
        // カンマ「,」区切りで取得するフィールドを指定可能、ネスト要素はドット「.」でアクセス可能
        this.fields = "statistics.all, created_at, clan_id, global_rating, last_battle_time";
    }
}


/// <summary>
/// レスポンスデータクラス
/// </summary>
public class PlayerPersonalResponseData
{
    [JsonPropertyName("status")]
    public String status { get; set; } = String.Empty;

    [JsonPropertyName("meta")]
    public Meta meta { get; set; } = new();

    // Stringのキーは動的なPlayerId
    [JsonPropertyName("data")]
    public Dictionary<String, PlayerData> data { get; set; } = new();

    public override String ToString() => JsonSerializer.Serialize(this);
}

public class Meta
{
    [JsonPropertyName("count")]
    public Int32 count { get; set; } = 1;

    [JsonPropertyName("page_total")]
    public Int32 page_total { get; set; } = 1;

    [JsonPropertyName("total")]
    public Int32 total { get; set; } = 1;

    [JsonPropertyName("limit")]
    public Int32 limit { get; set; } = 1;

    [JsonPropertyName("page")]
    public Int32 page { get; set; } = 1;

    public override String ToString() => JsonSerializer.Serialize(this);
}

public class PlayerData
{
    [JsonPropertyName("statistics")]
    public Statistics statistics { get; set; } = new();

    [JsonPropertyName("created_at")]
    public Int32 created_at { get; set; }

    [JsonPropertyName("clan_id")]
    public Int32 clan_id { get; set; }

    [JsonPropertyName("global_rating")]
    public Int32 global_rating { get; set; }

    [JsonPropertyName("last_battle_time")]
    public Int32 last_battle_time { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}

public class Statistics
{
    // 様々なデータ
    [JsonPropertyName("all")]
    public Dictionary<String, Object> all { get; set; } = new();

    public override String ToString() => JsonSerializer.Serialize(this);
}
