using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace WoT.Utils;

/// <summary>
/// サンプル
/// </summary>
public class PlayersApi
{
    /// <summary>
    /// プレイヤー名からプレイヤーID(アカウントID)を取得
    /// </summary>
    /// <param name="playersRequestData">リクエストデータクラス</param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> Get(PlayersRequestData playersRequestData)
    {
        using(HttpClient httpClient = new HttpClient())
        {
            // リクエストデータクラスからクエリパラメータを作成
            Dictionary<String, String?> queryParams = playersRequestData.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(playersRequestData)?.ToString());

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
}


/// <summary>
/// リクエストデータクラス
/// </summary>
public class PlayersRequestData
{
    public String application_id { get; set; }
    public String search { get; set; }

    public PlayersRequestData(String application_id, String search)
    {
        this.application_id = application_id;
        this.search = search;
    }
}


/// <summary>
/// 正常時レスポンスデータクラス
/// </summary>
public class PlayersResponseData
{
    [JsonPropertyName("status")]
    public String? Status { get; set; }

    [JsonPropertyName("meta")]
    public PlayersMeta? Meta { get; set; }

    [JsonPropertyName("data")]
    public List<PlayersData>? Data { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}

public class PlayersMeta
{
    [JsonPropertyName("count")]
    public Int32? Count { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}

public class PlayersData
{
    [JsonPropertyName("nickname")]
    public String? Nickname { get; set; }

    [JsonPropertyName("account_id")]
    public Int32? AccountId { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}