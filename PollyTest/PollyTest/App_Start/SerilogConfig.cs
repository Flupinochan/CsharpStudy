using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace PollyTest.App_Start;

/// <summary>
/// Serilog DI設定クラス (最低限以下のライブラリが必要)
///   1. Serilog
///   2. Serilog.Extensions.Logging
///   3. Serilog.Settings.Configuration
///   4. Microsoft.Extensions.Configuration.Json
///   5. Serilog.Sinks.Console (コンソール出力用)
///   6. Serilog.Sinks.File (ファイル出力用)
///   7. Serilog.Exceptions (Exception用)
/// </summary>
public class SerilogConfig
{
    // Serilog DI設定
    public static void Configure(IServiceCollection services)
    {
        // ファイル読み込み
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Logger作成
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        // AddLoggingで、ILoggerにSerilogを実装
        services.AddLogging(builder => builder.AddSerilog(dispose: true));
    }
}

