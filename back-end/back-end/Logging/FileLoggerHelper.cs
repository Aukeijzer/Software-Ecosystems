using System.Collections.Concurrent;

namespace SECODashBackend.Logging;

/// <summary>
/// This static class handles async logging to a file by storing records in an intermediate collection.
/// </summary>
public static class FileLoggerHelper
{
    private static readonly BlockingCollection<(string, string)> Logs;
    
    static FileLoggerHelper()
    {
        Logs = new BlockingCollection<(string, string)>();
        InitialiseLogging();
    }
    /// <summary>
    /// Initialise the <see cref="LogLoop"/> method on an async thread.
    /// </summary>
    private static async void InitialiseLogging()
    {
        await Task.Run(LogLoop);
    }
    
    /// <summary>
    /// Add a record to the collection to be logged later.
    /// </summary>
    /// <param name="logMessage">The log message as <see cref="string"/>.</param>
    /// <param name="filePath">Filepath as <see cref="string"/> for the location of the log file.</param>
    public static void AddRecord(string logMessage, string filePath)
    {
       Logs.Add((logMessage, filePath));
    }

    /// <summary>
    /// Start a loop to take a record from the collection and write the message to the accompanying file location.
    /// </summary>
    private static void LogLoop()
    {
        while (true)
        {
            var record = Logs.Take();
            using var streamWriter = new StreamWriter(record.Item2, true);
            streamWriter.WriteLine(record.Item1);
        }
    }
}