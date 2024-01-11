namespace SECODashBackend.Logging;
/// <summary>
/// This class configures and builds the <see cref="FileLoggerProvider"/>. 
/// </summary>
public static class FileLoggerExtensions
{
    public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, Action<FileLoggerOptions> configure)
    {
        builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>();
        builder.Services.Configure(configure);
        return builder;
    }
}