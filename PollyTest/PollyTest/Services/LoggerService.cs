using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Runtime.CompilerServices;

namespace PollyTest.Services;
public class LoggerService : ILoggerService
{
    private readonly ILogger<LoggerService> _logger;

    // ILoggerの実装としてSerilogが注入される
    public LoggerService(ILogger<LoggerService> logger)
    {
        _logger = logger;
    }

    public void Debug(String message,
        [CallerMemberName] String functionName = "",
        [CallerFilePath] String filePath = "",
        [CallerLineNumber] Int32 lineNumber = -1)
    {
        // LogContextを使用し、ファイル名、関数名、行番号をPropertiesに追加
        String fileName = Path.GetFileName(filePath);
        using(LogContext.PushProperty("LineNumber", lineNumber))
        using(LogContext.PushProperty("FunctionName", functionName))
        using(LogContext.PushProperty("FileName", fileName))
        {
            _logger.LogDebug(message);
        }
    }

    public void Info(String message,
        [CallerMemberName] String functionName = "",
        [CallerFilePath] String filePath = "",
        [CallerLineNumber] Int32 lineNumber = -1)
    {
        String fileName = Path.GetFileName(filePath);
        using(LogContext.PushProperty("LineNumber", lineNumber))
        using(LogContext.PushProperty("FunctionName", functionName))
        using(LogContext.PushProperty("FileName", fileName))
        {
            _logger.LogInformation(message);
        }
    }

    public void Error(String message,
        [CallerMemberName] String functionName = "",
        [CallerFilePath] String filePath = "",
        [CallerLineNumber] Int32 lineNumber = -1,
        Exception? exception = null)
    {
        String fileName = Path.GetFileName(filePath);
        using(LogContext.PushProperty("LineNumber", lineNumber))
        using(LogContext.PushProperty("FunctionName", functionName))
        using(LogContext.PushProperty("FileName", fileName))
        {
            _logger.LogError(exception, message);
        }
    }
}
