using System.Diagnostics.CodeAnalysis;

namespace SECODashBackend.Logging;
/// <summary>
/// An <see cref="ILogger"/> that writes logs to text files.
/// </summary>
public class FileLogger : ILogger 
{
    protected readonly FileLoggerProvider _fileLoggerProvider;

    /// <summary>
    /// Create an instance of <see cref="FileLogger"/> using the <see cref="FileLoggerProvider"/>
    /// </summary>
    /// <param name="fileLoggerProvider">Provides all configurations needed to use an instance of the <see cref="FileLogger"/>.</param>
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

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState,
        Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }
        //Construct the file path from options and using the date/time
        var fullFilePath = string.Format("{0}/{1}",
            _fileLoggerProvider.Options.FolderPath, _fileLoggerProvider.Options.FilePath.Replace(
                "{date}",DateTime.Now.ToString("yyyyMMdd")));
        //Construct the string with format: yyyy-MM-dd HH:mm:ss [LogLevel] Message StackTrace
        var logRecord = string.Format("{0} [{1}] {2} {3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            logLevel.ToString(), formatter(state, exception), (exception != null ? exception.StackTrace : ""));
        FileLoggerHelper.AddRecord(logRecord,fullFilePath);
    }
}