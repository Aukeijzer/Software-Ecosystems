using Microsoft.Extensions.Options;

namespace SECODashBackend.Logging
{
    [ProviderAlias("File")]
    public class FileLoggerProvider : ILoggerProvider
    {
        public readonly FileLoggerOptions Options;

        public FileLoggerProvider(IOptions<FileLoggerOptions> _options)
        {
            Options = _options.Value;
            if (!Directory.Exists(Options.FolderPath))
            {
                Directory.CreateDirectory((Options.FolderPath));
            }
        }

        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(this);
        }
    }
}