using System.Diagnostics.CodeAnalysis;

namespace spider.Logging;

public class FileLogger : ILogger 
{
    protected readonly FileLoggerProvider _fileLoggerProvider;

    public FileLogger([NotNull] FileLoggerProvider fileLoggerProvider)
    {
        _fileLoggerProvider = fileLoggerProvider;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var fullFilePath = string.Format("{0}/{1}", _fileLoggerProvider.Options.FolderPath, _fileLoggerProvider.Options.FilePath.Replace("{date}",DateTime.Now.ToString("yyyyMMdd")));
        var logRecord = string.Format("{0} [{1}] {2} {3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            logLevel.ToString(), formatter(state, exception), (exception != null ? exception.StackTrace : ""));
        using (var streamWriter = new StreamWriter(fullFilePath, true))
        {
           streamWriter.WriteLine(logRecord);
        }
    }
}