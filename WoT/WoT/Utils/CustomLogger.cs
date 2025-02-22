using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WoT.Utils
{
    /// <summary>
    /// ログレベルEnum
    /// </summary>
    public enum LogLevel{
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
        private readonly string _logDirectoryName = "log";
        private readonly string _logDirectoryPath;
        private readonly string _logFilePath;

        public CustomLogger() : this(LogLevel.INFO) { }

        public CustomLogger(LogLevel logLevel)
        {
            this._logLevel = logLevel;
            _logDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), _logDirectoryName);
            string logFileName = $"{DateTime.Now:yyyyMMdd_HHmmss}.log";
            _logFilePath = Path.Combine(_logDirectoryPath, logFileName);
            CreateLogDirectory();
            CleanupOldLogs();
        }

        /// <summary>
        /// ログディレクトリがない場合は作成
        /// </summary>
        public void CreateLogDirectory()
        {
            if (!Directory.Exists(_logDirectoryPath))
            {
                try
                {
                    Directory.CreateDirectory(_logDirectoryPath);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"ログディレクトリの作成に失敗しました: {ex}");
                }
            }
        }

        /// <summary>
        /// 7日以上前に作成されたログファイルは削除
        /// </summary>
        public void CleanupOldLogs()
        {
            DateTime sevenDaysAgo = DateTime.Now.AddDays(-7);
            string[] logFiles = Directory.GetFiles(_logDirectoryName);
            foreach (string logFile in logFiles)
            {
                DateTime logFileCreatedTime = File.GetCreationTime(logFile);
                try
                {
                    if (logFileCreatedTime < sevenDaysAgo)
                        File.Delete(logFile);
                }
                catch (Exception ex)
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
        public void DEBUG(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string functionName = "")
        {
            if (_logLevel == LogLevel.DEBUG)
                PrintLog(message, filePath, lineNumber, functionName);
        }

        /// <summary>
        /// INFO用
        /// </summary>
        public void INFO(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string functionName = "")
        {
            if (_logLevel == LogLevel.DEBUG || _logLevel == LogLevel.INFO)
                PrintLog(message, filePath, lineNumber, functionName);
        }

        /// <summary>
        /// ERROR用
        /// </summary>
        public void ERROR(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string functionName = "")
        {
            if (_logLevel == LogLevel.DEBUG || _logLevel == LogLevel.INFO || _logLevel == LogLevel.ERROR)
                PrintLog(message, filePath, lineNumber, functionName);
        }

        /// <summary>
        /// 共通ログ出力
        /// </summary>
        private void PrintLog(string message, string filePath, int lineNumber, string functionName)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff zzz");
            string fileName = Path.GetFileName(filePath);
            string formattedLogMessage = $"[{timestamp}] [{fileName}: {lineNumber}] [{functionName}] {message}";
            try
            {
                Debug.WriteLine(formattedLogMessage);
                 File.AppendAllText(_logFilePath, formattedLogMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ログファイルの書き込みに失敗しました: {ex}");
            }
        }
    }
}
