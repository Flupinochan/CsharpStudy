using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace WoT.Utils;

class TankInfoApi
{
    /// <summary>
    /// 全てのTank情報を取得
    /// </summary>
    /// <param name="tankInfoRequestData">リクエストデータクラス</param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> Get(TankInfoRequestData tankInfoRequestData)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            // リクエストデータクラスからクエリパラメータを作成
            Dictionary<String, String?> queryParams = tankInfoRequestData.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(tankInfoRequestData)?.ToString());

            // リクエストURLとクエリパラメータを結合
            String baseUrl = App.BaseUrl + "encyclopedia/vehicles/";
            String url = $"{baseUrl}?{String.Join("&", queryParams.Select(p => $"{HttpUtility.UrlEncode(p.Key)}={HttpUtility.UrlEncode(p.Value)}"))}";
            App.Logger.DEBUG($"GETリクエストURL: {url}");

            // GETリクエスト送信
            HttpResponseMessage response = await httpClient.GetAsync(url);

            // レスポンス返却
            return response;
        }
    }


    /// <summary>
    /// Tank情報は多いため、ページネーション処理して最終的なresponseデータ1つに統合して返す
    /// </summary>
    /// <param name="tankInfoRequestData"></param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> GetPagination(TankInfoRequestData tankInfoRequestData)
    {
        // 最終的な1つのレスポンスデータの定義
        TankInfoResponseData allData = new TankInfoResponseData
        {
            Status = null,
            Meta = null,
            Data = new Dictionary<String, TankInfoData>()
        };

        // ページ数1から取得
        Int32 pageNo = 1;
        TankInfoResponseData tankInfoResponseData;

        // 繰り返しリクエスト
        do
        {
            tankInfoRequestData.page_no = pageNo;

            // リクエストの送信
            HttpResponseMessage response = await this.Get(tankInfoRequestData);
            String responseBody = await response.Content.ReadAsStringAsync();
            tankInfoResponseData = JsonSerializer.Deserialize<TankInfoResponseData>(responseBody)!;

            // 全データに追加
            if(tankInfoResponseData?.Data != null)
            {
                foreach(KeyValuePair<String, TankInfoData> data in tankInfoResponseData.Data)
                {
                    allData.Data.Add(data.Key, data.Value);
                }
            }

            pageNo++;

        } while(tankInfoResponseData?.Meta?.Page_total >= pageNo);

        Int32 totalDataCount = allData.Data.Count;
        App.Logger.DEBUG($"最終的なデータ件数: {totalDataCount}");

        // HTTPレスポンスに戻して返却
        HttpResponseMessage finalResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(JsonSerializer.Serialize(allData))
        };

        return finalResponse;
    }
}


/// <summary>
/// リクエストデータクラス
/// </summary>
public class TankInfoRequestData
{
    public String application_id { get; set; }
    public String fields { get; set; }
    public String language { get; set; }
    public Int32 page_no { get; set; }

    public TankInfoRequestData(String application_id, Int32 page_no)
    {
        this.application_id = application_id;
        this.fields = "tank_id, name, tier, nation";
        this.language = "en";
        this.page_no = page_no;
    }
}


/// <summary>
/// 正常時レスポンスデータクラス
/// </summary>
public class TankInfoResponseData
{
    [JsonPropertyName("status")]
    public String? Status { get; set; }

    [JsonPropertyName("meta")]
    public TankInfoMeta? Meta { get; set; }

    [JsonPropertyName("data")]
    public Dictionary<String, TankInfoData>? Data { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}


public class TankInfoMeta
{
    [JsonPropertyName("count")]
    public Int32 Count { get; set; }

    [JsonPropertyName("page_total")]
    public Int32 Page_total { get; set; }

    [JsonPropertyName("total")]
    public Int32 Total { get; set; }

    [JsonPropertyName("limit")]
    public Int32 Limit { get; set; }

    // nullの可能性が高いので?にする
    [JsonPropertyName("page")]
    public Int32? Page { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}

public class TankInfoData
{
    [JsonPropertyName("tank_id")]
    public Int32 Tank_id { get; set; }

    [JsonPropertyName("tier")]
    public Int32 Tier { get; set; }

    [JsonPropertyName("name")]
    public String? Name { get; set; }

    [JsonPropertyName("nation")]
    public String? Nation { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}