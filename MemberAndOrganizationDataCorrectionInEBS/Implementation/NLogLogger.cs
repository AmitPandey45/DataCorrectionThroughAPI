using MemberAndOrganizationDataCorrectionInEBS.Interface;
using MemberAndOrganizationDataCorrectionInEBS.Model;
using NLog;
using System;
using ILogger = MemberAndOrganizationDataCorrectionInEBS.Interface.ILogger;

namespace MemberAndOrganizationDataCorrectionInEBS.Implementation
{
    /// <summary>
    ///  NLog Logger class. This will log error/event using Nlog
    /// </summary>
    public class NLogLogger : ILogger
    {
        private readonly Logger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NLogLogger"/> class.
        /// </summary>
        public NLogLogger()
        {
            this._logger = LogManager.GetLogger("Default");
        }

        /// <summary>
        /// Logs the specified log entry.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        public void Log(LogEntry logEntry)
        {
            if (logEntry == null)
            {
                throw new ArgumentNullException(nameof(logEntry));
            }

            var message = this.FormatMessage(logEntry);
            var logLevel = global::NLog.LogLevel.FromString(logEntry.LogLevel.Name);

            if (logEntry.Exception != null)
            {
                this._logger.Log(logLevel, logEntry.Exception, message + logEntry.Exception.StackTrace);
            }
            else
            {
                this._logger.Log(logLevel, message);
            }
        }

        /// <summary>
        /// Flush log
        /// </summary>
        public void Flush()
        {
            if (this._logger.Factory != null)
            {
                this._logger.Factory.Flush();
            }
        }

        /// <summary>
        /// Formats the message.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        /// <returns>String.</returns>
        private string FormatMessage(LogEntry logEntry)
        {
            if (logEntry.Parameters == null || logEntry.Parameters.Length == 0)
            {
                return logEntry.Message;
            }

            try
            {
                return string.Format(logEntry.Message, logEntry.Parameters);
            }
            catch (Exception exception)
            {
                this._logger.Warn(exception, "Error when formatting a message: {0}", exception);
                return logEntry.Message;
            }
        }
    }
}
