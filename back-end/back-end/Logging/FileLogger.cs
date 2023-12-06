namespace SECODashBackend.Logging;

/// <summary>
/// This class represents a logger that logs to a file.
/// </summary>
/// <param name="fileLoggerProvider"></param>
public class FileLogger(FileLoggerProvider fileLoggerProvider) : ILogger 
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState,
        Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }
        // Construct the file path from options and using the current date for the file name
        var fullFilePath = string.Format("{0}/{1}",
            fileLoggerProvider.Options.FolderPath, fileLoggerProvider.Options.FilePath.Replace(
                "{date}",DateTime.Now.ToString("yyyyMMdd")));
        // Construct the string with format: yyyy-MM-dd HH:mm:ss [LogLevel] Message StackTrace
        var logRecord = string.Format("{0:yyyy-MM-dd HH:mm:ss} [{1}] {2} {3}", DateTime.Now,
            logLevel.ToString(), formatter(state, exception), (exception != null ? exception.StackTrace : ""));
        FileLoggerHelper.AddRecord(logRecord, fullFilePath);
    }
}