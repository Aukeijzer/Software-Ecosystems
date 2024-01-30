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

namespace spider.Logging;
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