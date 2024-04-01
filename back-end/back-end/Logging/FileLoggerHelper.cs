// Copyright (C) <2024>  <ODINDash>
// 
// This file is part of SECODash.
// 
// SECODash is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SECODash is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

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