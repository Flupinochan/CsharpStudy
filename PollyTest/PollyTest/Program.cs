using Microsoft.Extensions.DependencyInjection;
using PollyTest.App_Start;
using PollyTest.Services;

partial class Program
{
    public static IServiceProvider Services { get; private set; }

    // staticコンストラクタで定義 (static変数のみのため)
    static Program()
    {
        Services = ConfigureServices();
    }

    // DI設定
    private static IServiceProvider ConfigureServices()
    {
        ServiceCollection services = new ServiceCollection();

        // Logger(Serilog)
        SerilogConfig.Configure(services);
        // LoggerService
        services.AddSingleton<LoggerService>();
        // Polly
        PollyConfig.Configure(services);
        // HttpClient
        services.AddHttpClient();
        // HttpRequestService
        services.AddSingleton<HttpRequestService>();

        // DIコンテナのビルド
        IServiceProvider Services = services.BuildServiceProvider();
        return Services;
    }


    // エントリーポイント
    static async Task Main()
    {
        LoggerService logger = Services.GetRequiredService<LoggerService>();

        // リクエストURL
        String url = "https://metalmental.net/";

        // ユーザによるキャンセル
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken outerToken = cts.Token;

        // Enterキーが押されたらキャンセルを実行
        Task cancelTask = MonitorEnterKeyPress(cts);

        // リクエスト実行
        HttpRequestService httpRequestService = Services.GetRequiredService<HttpRequestService>();
        await httpRequestService.GetAsync(url, outerToken);
    }

    // Enterキーが押されたらキャンセルする処理
    static async Task MonitorEnterKeyPress(CancellationTokenSource cts)
    {
        await Task.Run(() =>
        {
            Console.WriteLine("Enter キーを押すとキャンセルされます...");
            Console.WriteLine(Environment.NewLine);
            while(true)
            {
                if(Console.KeyAvailable && Console.ReadKey(intercept: true).Key == ConsoleKey.Enter)
                {
                    cts.Cancel();
                    break;
                }
            }
        });
    }

}