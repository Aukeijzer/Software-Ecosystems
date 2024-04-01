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
        /// <param name="options">Represents the file location the <see cref="FileLogger"/> should write to.</param>
        public FileLoggerProvider(IOptions<FileLoggerOptions> options)
        {
            Options = options.Value;
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