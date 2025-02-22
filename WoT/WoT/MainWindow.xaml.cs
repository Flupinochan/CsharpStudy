using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WoT.Utils;

namespace WoT;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Window画面表示時のEvent処理
    /// </summary>
    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await TestRequest();
    }

    /// <summary>
    /// テストリクエスト用
    /// </summary>
    /// <returns></returns>
    private async Task TestRequest()
    {
        TestApiHelper helper = new TestApiHelper();
        TestRequestData testRequestData = new TestRequestData(App.ApplicationId!, "metalmental");
        HttpResponseMessage response = await helper.Get(testRequestData);
        // HttpResponseMessage response =  await helper.Post(testRequestData);
        string responseBody = await response.Content.ReadAsStringAsync();
        App.Logger.DEBUG($"ステータスコード: {response.StatusCode}");
        // ※Wot APIでは、全てHttpStatusCode.OKのステータスコードのため、意味がない
        // 判別するとしたら、レスポンスボディのステータスコード
        //if (response.StatusCode == HttpStatusCode.OK)
        if (response.IsSuccessStatusCode)
        {
            TestSuccessResponseData responseData = JsonSerializer.Deserialize<TestSuccessResponseData>(responseBody)!;
            App.Logger.DEBUG($"正常時レスポンス: {responseData}");
        }
        else
        {
            TestErrorResponseData responseData = JsonSerializer.Deserialize<TestErrorResponseData>(responseBody)!;
            App.Logger.DEBUG($"エラー時レスポンス: {responseData}");
        }
    }
}