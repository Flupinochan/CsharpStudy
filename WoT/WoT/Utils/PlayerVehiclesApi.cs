using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace WoT.Utils;

class PlayerVehiclesApi
{
    /// <summary>
    /// プレイヤーIDからTank情報を取得
    /// </summary>
    /// <param name="playerVehiclesRequestData">リクエストデータクラス</param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> Get(PlayerVehiclesRequestData playerVehiclesRequestData)
    {
        using(HttpClient httpClient = new HttpClient())
        {
            // リクエストデータクラスからクエリパラメータを作成
            Dictionary<String, String?> queryParams = playerVehiclesRequestData.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(playerVehiclesRequestData)?.ToString());

            // リクエストURLとクエリパラメータを結合
            String baseUrl = App.BaseUrl + "account/tanks/";
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
public class PlayerVehiclesRequestData
{
    public String application_id { get; set; }
    public String account_id { get; set; }
    public String language { get; set; }

    public PlayerVehiclesRequestData(String application_id, String account_id)
    {
        this.application_id = application_id;
        this.account_id = account_id;
        this.language = "en";
    }
}


/// <summary>
/// 正常時レスポンスデータクラス
/// </summary>
public class PlayerVehiclesResponseData
{
    [JsonPropertyName("status")]
    public String? Status { get; set; }

    [JsonPropertyName("meta")]
    public PlayerVehiclesMeta? Meta { get; set; }

    [JsonPropertyName("data")]
    public Dictionary<String, List<PlayersVehiclesData>> data { get; set; } = new();

    public override String ToString() => JsonSerializer.Serialize(this);

    /// <summary>
    /// DataGridにバインドするための整形したデータクラスを返す
    /// </summary>
    /// <returns></returns>
    public ObservableCollection<PlayerVehicleSummary> ConvertoToplayerVehicleSummaries(TankInfoResponseData tankInfoResponseData)
    {
        ObservableCollection<PlayerVehicleSummary> observablePlayerVehicleSummary = new ObservableCollection<PlayerVehicleSummary>();

        // keyがtankIdのDictを初期化
        Dictionary<Int32, TankInfoData> tankInfoData = new Dictionary<Int32, TankInfoData>();
        // tankInfoResponseDataからkeyがtankIdで、valueがTankInfoのDictを再生成
        if(tankInfoResponseData.Data != null)
        {
            foreach(KeyValuePair<String, TankInfoData> tankInfo in tankInfoResponseData.Data)
            {
                tankInfoData[tankInfo.Value.Tank_id] = tankInfo.Value;
            }
        }

        // プレイヤーのTankごとに繰り返し処理
        foreach(List<PlayersVehiclesData> vehiclesList in this.data.Values)
        {
            foreach(PlayersVehiclesData vehicleData in vehiclesList)
            {
                // 勝率を計算
                Double winRate = 0;
                if(vehicleData.statistics.wins.HasValue && vehicleData.statistics.battles.HasValue && vehicleData.statistics.battles.Value > 0)
                {
                    winRate = (Double)vehicleData.statistics.wins.Value / vehicleData.statistics.battles.Value * 100;
                    winRate = Math.Round(winRate, 2);
                }
                else
                {
                    winRate = 0;
                }

                // tank_idに基づいてTankInfoDataを取得
                TankInfoData tankInfo = null;
                if(tankInfoData.ContainsKey(vehicleData.tank_id))
                {
                    tankInfo = tankInfoData[vehicleData.tank_id];
                }

                // TankInfoDataが見つかった場合
                if(tankInfo != null)
                {
                    PlayerVehicleSummary playerVehicleSummary = new PlayerVehicleSummary
                    {
                        TankId = vehicleData.tank_id,
                        Tier = tankInfo.Tier,
                        Name = tankInfo.Name ?? "Unknown",
                        Nation = tankInfo.Nation ?? "Unknown",
                        MarkOfMastery = vehicleData.mark_of_mastery,
                        WinRate = winRate,
                        Battles = Int32.Parse(vehicleData.statistics.battles.ToString()!)
                    };
                    observablePlayerVehicleSummary.Add(playerVehicleSummary);
                }
                // TankInfoDataが見つからない場合
                else
                {
                    PlayerVehicleSummary playerVehicleSummary = new PlayerVehicleSummary
                    {
                        TankId = vehicleData.tank_id,
                        Tier = -1,
                        Name = "Unknown",
                        Nation = "Unknown",
                        MarkOfMastery = vehicleData.mark_of_mastery,
                        WinRate = winRate,
                        Battles = Int32.Parse(vehicleData.statistics.battles.ToString()!)
                    };
                    observablePlayerVehicleSummary.Add(playerVehicleSummary);
                }
            }
        }

        return observablePlayerVehicleSummary;
    }
}

public class PlayerVehiclesMeta
{
    [JsonPropertyName("count")]
    public Int32? Count { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}

public class PlayersVehiclesData
{
    [JsonPropertyName("statistics")]
    public playerVehiclesStatistics statistics { get; set; } = new();

    [JsonPropertyName("mark_of_mastery")]
    public Int32 mark_of_mastery { get; set; }

    [JsonPropertyName("tank_id")]
    public Int32 tank_id { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}

public class playerVehiclesStatistics
{
    [JsonPropertyName("wins")]
    public Int32? wins { get; set; }

    [JsonPropertyName("battles")]
    public Int32? battles { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}


/// <summary>
/// 上記レスポンスを整形し、DataGridにバインドするためのデータクラス
/// </summary>
public class PlayerVehicleSummary
{
    public Int32 TankId { get; set; }
    public Int32 Tier { get; set; }
    public String? Name { get; set; }
    public String? Nation { get; set; }
    public Int32 MarkOfMastery { get; set; }
    public Double WinRate { get; set; }
    public Int32 Battles { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}




