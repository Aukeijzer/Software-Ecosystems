using Microsoft.Extensions.Options;

namespace SECODashBackend.Logging
{   
    /// <summary>
    /// Can create an instance of the <see cref="FileLogger"/>.
    /// </summary>
    [ProviderAlias("File")]
    public class FileLoggerProvider : ILoggerProvider
    {
        public readonly FileLoggerOptions Options;
        
        /// <summary>
        /// This constructor uses the <see cref="FileLoggerOptions"/> to create a directory for storing log files if this folder does not yet exist.
        /// </summary>
        /// <param name="_options">Represents the file location the <see cref="FileLogger"/> should write to.</param>
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