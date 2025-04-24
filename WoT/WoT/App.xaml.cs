using System.Configuration;
using System.Diagnostics;
using System.Windows;
using WoT.Utils;

namespace WoT;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App:Application
{
    public static CustomLogger Logger = new CustomLogger();
    public static String? BaseUrl;
    public static String? ApplicationId;

    public App()
    {
        try
        {
            // 環境変数セットアップ
            BaseUrl = ConfigurationManager.AppSettings["BASE_URL"]!;
            ApplicationId = ConfigurationManager.AppSettings["APPLICATION_ID"];
            String logLevelString = ConfigurationManager.AppSettings["LOG_LEVEL"]!;

            // ロガー初期化
            LogLevel logLevel = LogLevel.INFO;
            if(!Enum.TryParse(logLevelString, true, out logLevel))
                Debug.WriteLine($"無効なログレベルが設定されています: {logLevelString}");
            Logger = new CustomLogger(logLevel);
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"アプリケーション起動に失敗しました: {ex}");
        }
    }

}