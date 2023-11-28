namespace SECODashBackend.Logging;
/// <summary>
/// This class stores the options for the <see cref="FileLoggerProvider"/> as <see cref="string"/> FilePath and <see cref="string"/> FolderPath.
/// </summary>
public class FileLoggerOptions
{
    public virtual string FilePath { get; set; }
    
    public virtual string FolderPath { get; set; }
}