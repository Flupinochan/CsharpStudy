using LocalDeepSeek.Utils;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Security.Policy;
using System.Windows;

namespace LocalDeepSeek;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static String LogDirectoryPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!, "log");
    public static CustomLogger Logger = new CustomLogger();
    public static String DatabaseDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!, "database");
    public static String DatabasePath = Path.Combine(DatabaseDirectory, "LocalDeepSeek.db");
    public static String ModelId = "gemma2:9b";

    /// <summary>
    /// エントリーポイント
    /// </summary>
    public App()
    {
        try
        {
            // 環境変数初期化
            // モデルID
            ModelId = ConfigurationManager.AppSettings["MODEL_ID"]!;

            // データベースディレクトリの作成
            if(!Directory.Exists(DatabaseDirectory))
                Directory.CreateDirectory(DatabaseDirectory);

            // ロガー
            String logLevelStr = ConfigurationManager.AppSettings["LOG_LEVEL"]!;
            if(Enum.TryParse<LogLevel>(logLevelStr, false, out LogLevel logLevel))
            {
                Logger = new CustomLogger(logLevel);
                Logger.INFO("アプリケーション起動");
                // App.config (環境変数) ログ出力
                ConfigurationManager.AppSettings.AllKeys.ToList().ForEach(key => Logger.INFO($"{key}: {ConfigurationManager.AppSettings[key]}"));

                InitProcess();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"環境変数の設定に誤りがあります: {ex}");
            Shutdown();
        }
    }


    /// <summary>
    /// 初期化処理
    /// </summary>
    private void InitProcess()
    {
        SqliteManagement.CreateTable();
    }
}

