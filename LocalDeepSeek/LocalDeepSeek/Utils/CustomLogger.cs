using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace LocalDeepSeek.Utils;

/// <summary>
/// ログレベルEnum
/// </summary>
public enum LogLevel
{
    DEBUG,
    INFO,
    ERROR
}

/// <summary>
/// カスタムロガークラス
/// </summary>
public class CustomLogger
{
    private readonly LogLevel _logLevel;
    private readonly String _logDirectoryPath;
    private readonly String _logFilePath;

    // コンストラクタ
    public CustomLogger() : this(LogLevel.INFO, Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!, "log")) { }
    public CustomLogger(LogLevel logLevel) : this(logLevel, Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!, "log")) { }
    public CustomLogger(LogLevel logLevel, String logDirectoryPath)
    {
        this._logLevel = logLevel;
        this._logDirectoryPath = logDirectoryPath;
        String logFileName = $"{DateTime.Now:yyyyMMdd_HHmmss}.log";
        this._logFilePath = Path.Combine(logDirectoryPath, logFileName);
        this.CreateLogDirectory();
        this.CleanupOldLogs();
    }

    /// <summary>
    /// ログディレクトリがない場合は作成
    /// </summary>
    private void CreateLogDirectory()
    {
        if(!Directory.Exists(this._logDirectoryPath))
        {
            try
            {
                _ = Directory.CreateDirectory(this._logDirectoryPath);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"ログディレクトリの作成に失敗しました: {ex}");
            }
        }
    }

    /// <summary>
    /// 7日以上前に作成されたログファイルは削除
    /// </summary>
    private void CleanupOldLogs()
    {
        DateTime sevenDaysAgo = DateTime.Now.AddDays(-7);
        String[] logFiles = Directory.GetFiles(this._logDirectoryPath);
        foreach(String logFile in logFiles)
        {
            DateTime logFileCreatedTime = File.GetCreationTime(logFile);
            try
            {
                if(logFileCreatedTime < sevenDaysAgo)
                    File.Delete(logFile);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"ログファイルの削除に失敗しました: {ex}");
            }
        }
    }

    /// <summary>
    /// DEBUG用
    /// </summary>
    /// <param name="message">ログメッセージ</param>
    /// <param name="filePath">呼び出し元のファイル名</param>
    /// <param name="lineNumber">呼び出し元の行数</param>
    /// <param name="functionName">呼び出し元の関数名</param>
    public void DEBUG(String message, [CallerFilePath] String filePath = "", [CallerLineNumber] Int32 lineNumber = 0, [CallerMemberName] String functionName = "")
    {
        if(this._logLevel == LogLevel.DEBUG)
            this.PrintLog(message, filePath, lineNumber, functionName);
    }

    /// <summary>
    /// INFO用
    /// </summary>
    public void INFO(String message, [CallerFilePath] String filePath = "", [CallerLineNumber] Int32 lineNumber = 0, [CallerMemberName] String functionName = "")
    {
        if(this._logLevel is LogLevel.DEBUG or LogLevel.INFO)
            this.PrintLog(message, filePath, lineNumber, functionName);
    }

    /// <summary>
    /// ERROR用
    /// </summary>
    public void ERROR(String message, [CallerFilePath] String filePath = "", [CallerLineNumber] Int32 lineNumber = 0, [CallerMemberName] String functionName = "")
    {
        if(this._logLevel is LogLevel.DEBUG or LogLevel.INFO or LogLevel.ERROR)
            this.PrintLog(message, filePath, lineNumber, functionName);
    }

    /// <summary>
    /// 共通ログ出力
    /// </summary>
    private void PrintLog(String message, String filePath, Int32 lineNumber, String functionName)
    {
        String timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff zzz");
        String fileName = Path.GetFileName(filePath);
        String formattedLogMessage = $"[{timestamp}] [{fileName} : {lineNumber}] [{functionName}] {message}";
        try
        {
            Debug.WriteLine(formattedLogMessage);
            File.AppendAllText(this._logFilePath, formattedLogMessage + Environment.NewLine);
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"ログファイルの書き込みに失敗しました: {ex}");
        }
    }
}