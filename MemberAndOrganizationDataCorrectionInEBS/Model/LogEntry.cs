using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberAndOrganizationDataCorrectionInEBS.Model
{
    /// <summary>
    /// Class LogEntry.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The message.</param>
        public LogEntry(LogLevel logLevel, string message)
            : this(logLevel, message, new object[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        public LogEntry(LogLevel logLevel, string message, object[] parameters)
        {
            this.LogLevel = logLevel;
            this.Message = message;
            this.Parameters = parameters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="exception">The exception.</param>
        public LogEntry(LogLevel logLevel, string message, object[] parameters, Exception exception)
        {
            this.LogLevel = logLevel;
            this.Message = message;
            this.Parameters = parameters;
            this.Exception = exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageId">The messageId.</param>
        /// <param name="correlationId">The correlationId.</param>
        public LogEntry(LogLevel logLevel, string message, string messageId, string correlationId)
        {
            this.LogLevel = logLevel;
            this.Message = message;
            this.MessageId = messageId;
            this.CorrelationId = correlationId;
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets the log level.
        /// </summary>
        /// <value>The log level.</value>
        public LogLevel LogLevel { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public object[] Parameters { get; private set; }

        /// <summary>
        /// Gets the MessageId.
        /// </summary>
        /// <value>The MessageId.</value>
        public string MessageId { get; private set; }

        /// <summary>
        /// Gets the CorrelationId.
        /// </summary>
        /// <value>The CorrelationId.</value>
        public string CorrelationId { get; private set; }
    }
}
