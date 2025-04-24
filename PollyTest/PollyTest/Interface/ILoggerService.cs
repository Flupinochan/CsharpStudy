using System.Runtime.CompilerServices;

public interface ILoggerService
{
    public void Debug(String message,
                  [CallerMemberName] String functionName = "",
                  [CallerFilePath] String filePath = "",
                  [CallerLineNumber] Int32 lineNumber = -1);
    public void Info(String message,
                  [CallerMemberName] String functionName = "",
                  [CallerFilePath] String filePath = "",
                  [CallerLineNumber] Int32 lineNumber = -1);
    public void Error(String message,
                  [CallerMemberName] String functionName = "",
                  [CallerFilePath] String filePath = "",
                  [CallerLineNumber] Int32 lineNumber = -1,
                  Exception? exception = null);
}
