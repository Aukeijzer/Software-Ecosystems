using System.Collections.Concurrent;

namespace SECODashBackend.Logging;

public static class FileLoggerHelper
{
    private static BlockingCollection<(string, string)> _logs;

    static FileLoggerHelper()
    {
        _logs = new BlockingCollection<(string, string)>();
        InitiateLogging();
    }

    public static void AddRecord(string logMessage, string filePath)
    {
       _logs.Add((logMessage, filePath));
    }

    private static async void InitiateLogging()
    {
        await Task.Run(LogLoop);
    }

    private static void LogLoop()
    {
        while (true)
        {
            var record = _logs.Take();
            using var streamWriter = new StreamWriter(record.Item2, true);
            streamWriter.WriteLine(record.Item1);
        }
    }
}