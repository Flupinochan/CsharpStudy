using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using WoT.Utils;

namespace WoT;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow:Window
{
    public static ObservableCollection<PlayerVehicleSummary> _observablePlayerVehicleSummary;
    public static ObservableCollection<PlayerVehicleSummary> _filterObservablePlayerVehicleSummary;

    public MainWindow()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Window画面表示時のEvent処理
    /// </summary>
    private void Window_Loaded(Object sender, RoutedEventArgs e) { }


    /// <summary>
    /// [PlayerNameを検索]ボタンクリック時のEvent処理
    /// </summary>
    /// <returns></returns>
    private async void playerSearchButton_Click(Object sender, RoutedEventArgs e)
    {
        await this.GetPlayerId();
        await this.GetPlayerPersonal();
        TankInfoResponseData tankInfoResponseData = await this.GetTankInfo();
        await this.GetPlayerVehicles(tankInfoResponseData);

    }


    /// <summary>
    /// PlayerNameからPlayerIDを取得し、表示
    /// </summary>
    /// <returns></returns>
    private async Task GetPlayerId()
    {
        String playerName = this.playerNameTextBox.Text;
        if(String.IsNullOrEmpty(playerName))
            return;
        PlayersApi playersApi = new PlayersApi();
        PlayersRequestData playersRequestData = new PlayersRequestData(App.ApplicationId!, playerName);
        HttpResponseMessage response = await playersApi.Get(playersRequestData);
        String responseBody = await response.Content.ReadAsStringAsync();

        try
        {
            PlayersResponseData playersResponseData = JsonSerializer.Deserialize<PlayersResponseData>(responseBody)!;
            App.Logger.DEBUG($"プレイヤーID: {playersResponseData}");
            // 指定したコントロールへデータバインド
            this.playerNameStackPanel.DataContext = playersResponseData.Data![0];

            if(playersResponseData.Status == "error")
            {
                ErrorResponseData errorResponseData = JsonSerializer.Deserialize<ErrorResponseData>(responseBody)!;
                App.Logger.ERROR("プレイヤーIDの取得に失敗しました");
                App.Logger.ERROR($"{errorResponseData}");
            }
        }
        catch(Exception ex)
        {
            App.Logger.ERROR("プレイヤーIDの取得に失敗しました");
            App.Logger.ERROR($"{ex}");
        }
    }

    /// <summary>
    /// PlayerIDから様々なPlayer情報を取得し、表示
    /// </summary>
    /// <returns></returns>
    private async Task GetPlayerPersonal()
    {
        String playerId = this.playerIdTextBlock.Text;
        if(String.IsNullOrEmpty(playerId))
            return;
        PlayerPersonalApi playerPersonalApi = new PlayerPersonalApi();
        PlayerPersonalRequestData playerPersonalRequestData = new PlayerPersonalRequestData(App.ApplicationId!, Int32.Parse(playerId));
        HttpResponseMessage response = await playerPersonalApi.Get(playerPersonalRequestData);
        String responseBody = await response.Content.ReadAsStringAsync();

        try
        {
            PlayerPersonalResponseData playerPersonalResponseData = JsonSerializer.Deserialize<PlayerPersonalResponseData>(responseBody)!;

            App.Logger.DEBUG($"プレイヤー情報: {playerPersonalResponseData}");
            App.Logger.DEBUG($"プレイヤー作成日: {playerPersonalResponseData.data[playerId].created_at}");
            App.Logger.DEBUG($"総戦闘数: {playerPersonalResponseData.data[playerId].statistics.all["battles"]}");

            String battles = playerPersonalResponseData.data[playerId].statistics.all["battles"].ToString()!;
            String avgDamage = (Int32.Parse(playerPersonalResponseData.data[playerId].statistics.all["damage_dealt"].ToString()!) / Int32.Parse(playerPersonalResponseData.data[playerId].statistics.all["battles"].ToString()!)).ToString();
            Double winRate = (Double)Int32.Parse(playerPersonalResponseData.data[playerId].statistics.all["wins"].ToString()!) / Int32.Parse(playerPersonalResponseData.data[playerId].statistics.all["battles"].ToString()!) * 100;
            String winRateString = winRate.ToString("0.##");
            this.card1.Text = $"総戦闘数 : {battles}\n平均ダメージ : {avgDamage}\n勝率 : {winRateString}";

            Int64 createdAt = Int64.Parse(playerPersonalResponseData.data[playerId].created_at.ToString())!;
            DateTime createdAtDateTime = DateTimeOffset.FromUnixTimeSeconds(createdAt).DateTime;
            String createdAtString = createdAtDateTime.ToString("yyyy/MM/dd");
            Int64 lastBattle = Int64.Parse(playerPersonalResponseData.data[playerId].last_battle_time.ToString())!;
            DateTime lastBattleDateTime = DateTimeOffset.FromUnixTimeSeconds(lastBattle).DateTime;
            String lastBattleString = lastBattleDateTime.ToString("yyyy/MM/dd");
            this.card2.Text = $"アカウント作成日 : {createdAtString}\n最終戦闘日: {lastBattleString}";

            String maxKill = playerPersonalResponseData.data[playerId].statistics.all["max_frags"].ToString()!;
            String maxDamage = playerPersonalResponseData.data[playerId].statistics.all["max_damage"].ToString()!;
            this.card3.Text = $"最大キル数 : {maxKill}\n最大ダメージ: {maxDamage}";

            if(playerPersonalResponseData.status == "error")
            {
                ErrorResponseData errorResponseData = JsonSerializer.Deserialize<ErrorResponseData>(responseBody)!;
                App.Logger.ERROR("プレイヤー情報の取得に失敗しました");
                App.Logger.ERROR($"{errorResponseData}");
            }
        }
        catch(Exception ex)
        {
            App.Logger.ERROR("プレイヤー情報の取得に失敗しました");
            App.Logger.ERROR($"{ex}");
        }
    }


    /// <summary>
    /// PlayerIDから様々なVehicles情報を取得し、表示
    /// </summary>
    /// <returns></returns>
    private async Task GetPlayerVehicles(TankInfoResponseData tankInfoResponseData)
    {
        String playerId = this.playerIdTextBlock.Text;
        if(String.IsNullOrEmpty(playerId))
            return;
        PlayerVehiclesApi playerVehiclesApi = new PlayerVehiclesApi();
        PlayerVehiclesRequestData playerVehiclesRequestData = new PlayerVehiclesRequestData(App.ApplicationId!, playerId);
        HttpResponseMessage response = await playerVehiclesApi.Get(playerVehiclesRequestData);
        String responseBody = await response.Content.ReadAsStringAsync();

        try
        {
            PlayerVehiclesResponseData playerVehiclesResponseData = JsonSerializer.Deserialize<PlayerVehiclesResponseData>(responseBody)!;
            App.Logger.DEBUG($"プレイヤーTank情報: {playerVehiclesResponseData}");
            _observablePlayerVehicleSummary = playerVehiclesResponseData.ConvertoToplayerVehicleSummaries(tankInfoResponseData);
            _filterObservablePlayerVehicleSummary = new ObservableCollection<PlayerVehicleSummary>(_observablePlayerVehicleSummary);
            // バインド
            this.dataGrid.DataContext = _filterObservablePlayerVehicleSummary;
            this.paginate();


            if(playerVehiclesResponseData.Status == "error")
            {
                ErrorResponseData errorResponseData = JsonSerializer.Deserialize<ErrorResponseData>(responseBody)!;
                App.Logger.ERROR("プレイヤーTank情報の取得に失敗しました");
                App.Logger.ERROR($"{errorResponseData}");
            }
        }
        catch(Exception ex)
        {
            App.Logger.ERROR("プレイヤーTank情報の取得に失敗しました");
            App.Logger.ERROR($"{ex}");
        }
    }


    /// <summary>
    /// 全てのTank情報を取得
    /// </summary>
    /// <returns></returns>
    private async Task<TankInfoResponseData> GetTankInfo()
    {
        TankInfoApi tankInfoApi = new TankInfoApi();
        TankInfoRequestData tankInfoRequestData = new TankInfoRequestData(App.ApplicationId!, 1);
        HttpResponseMessage response = await tankInfoApi.GetPagination(tankInfoRequestData);
        String responseBody = await response.Content.ReadAsStringAsync();

        TankInfoResponseData tankInfoResponseData = null;
        try
        {
            tankInfoResponseData = JsonSerializer.Deserialize<TankInfoResponseData>(responseBody)!;
            App.Logger.DEBUG($"全Tank情報: {tankInfoResponseData}");



            if(tankInfoResponseData.Status == "error")
            {
                ErrorResponseData errorResponseData = JsonSerializer.Deserialize<ErrorResponseData>(responseBody)!;
                App.Logger.ERROR("プレイヤーTank情報の取得に失敗しました");
                App.Logger.ERROR($"{errorResponseData}");
            }
        }
        catch(Exception ex)
        {
            App.Logger.ERROR("プレイヤーTank情報の取得に失敗しました");
            App.Logger.ERROR($"{ex}");
        }

        return tankInfoResponseData;
    }




    /// <summary>
    /// ComboBoxでページサイズ選択時のEvent
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void pageSize_SelectionChanged(Object sender, SelectionChangedEventArgs e)
    {
        this.paginate();
    }

    private void paginate()
    {
        if(_filterObservablePlayerVehicleSummary is null)
            return;
        // ページサイズを取得
        if(this.pageSize.SelectedItem is not ComboBoxItem selectedItem || !Int32.TryParse(selectedItem.Content.ToString(), out Int32 pageSize) || pageSize <= 0)
        {
            return;
        }
        // 現在のページ数を取得
        Int32 currentPage = Int32.Parse(this.currentPage.Text);
        ObservableCollectionOperation observableCollectionOperation = new ObservableCollectionOperation();

        ObservableCollection<PlayerVehicleSummary> playerVehicleSummaries = observableCollectionOperation.Paginate(_filterObservablePlayerVehicleSummary, pageSize, currentPage);
        this.dataGrid.DataContext = playerVehicleSummaries;
    }

    /// <summary>
    /// DataGrid行番号追加用Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void dataGrid_LoadingRow(Object sender, DataGridRowEventArgs e)
    {
        // 行番号を設定
        e.Row.Header = (e.Row.GetIndex() + 1).ToString();
    }

    private void firstPageButton_Click(Object sender, RoutedEventArgs e)
    {
        this.currentPage.Text = "1";
        this.paginate();
    }

    private void lastPageButton_Click(Object sender, RoutedEventArgs e)
    {
        Int32 pageSize = Int32.Parse(this.pageSize.Text);
        Int32 totalItems = _filterObservablePlayerVehicleSummary.Count;
        Int32 totalPage = (Int32)Math.Ceiling((Double)totalItems / pageSize);
        this.currentPage.Text = totalPage.ToString();
        this.paginate();
    }

    private void prePageButtonlick(Object sender, RoutedEventArgs e)
    {
        Int32 currentPageInt = Int32.Parse(this.currentPage.Text);
        Int32 pageSize = Int32.Parse(this.pageSize.Text);
        Int32 totalItems = _filterObservablePlayerVehicleSummary.Count;
        Int32 totalPage = (Int32)Math.Ceiling((Double)totalItems / pageSize);
        if(currentPageInt < totalPage)
        {
            this.currentPage.Text = (currentPageInt + 1).ToString();
            this.paginate();
        }
    }

    private void backPageButton_Click(Object sender, RoutedEventArgs e)
    {
        Int32 currentPageInt = Int32.Parse(this.currentPage.Text);
        if(currentPageInt > 1)
        {
            this.currentPage.Text = (currentPageInt - 1).ToString();
            this.paginate();
        }
    }

    private void filterButton_Click(Object sender, RoutedEventArgs e)
    {
        FilterWindow filterWindow = new FilterWindow();
        filterWindow.Owner = this;
        filterWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        filterWindow.ShowDialog();
        this.dataGrid.DataContext = MainWindow._filterObservablePlayerVehicleSummary;
        this.paginate();
    }







    /// <summary>
    /// テストリクエスト用
    /// </summary>
    /// <returns></returns>
    private async Task TestRequest()
    {
        TestApi testHelper = new TestApi();
        TestRequestData testRequestData = new TestRequestData(App.ApplicationId!, "metalmental");
        HttpResponseMessage response = await testHelper.Get(testRequestData);
        // HttpResponseMessage response =  await testHelper.Post(testRequestData);
        String responseBody = await response.Content.ReadAsStringAsync();
        App.Logger.DEBUG($"ステータスコード: {response.StatusCode}");
        // ※Wot APIでは、全てHttpStatusCode.OKのステータスコードのため、意味がない
        // if (response.StatusCode == HttpStatusCode.OK)
        // if(response.IsSuccessStatusCode)
        try
        {
            TestSuccessResponseData responseData = JsonSerializer.Deserialize<TestSuccessResponseData>(responseBody)!;
            App.Logger.DEBUG($"正常時レスポンス: {responseData}");
            if(responseData.Status == "error")
            {
                TestErrorResponseData errorResponseData = JsonSerializer.Deserialize<TestErrorResponseData>(responseBody)!;
                App.Logger.DEBUG($"エラー時レスポンス: {errorResponseData}");
            }
        }
        catch(Exception ex)
        {
            App.Logger.ERROR($"エラー時レスポンス: {ex}");
        }


    }


}