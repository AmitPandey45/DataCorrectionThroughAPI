using MemberAndOrganizationDataCorrectionInEBS.Interface;
using MemberAndOrganizationDataCorrectionInEBS.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberAndOrganizationDataCorrectionInEBS.Extension
{
    /// <summary>
    /// Extensions for the logger
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Debugs the specified logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public static void Debug(this ILogger logger, string message, params object[] args)
        {
            logger.Log(new LogEntry(LogLevel.DEBUG, message, args));
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public static void Debug(this ILogger logger, Func<string> message, params object[] args)
        {
            logger.Log(new LogEntry(LogLevel.DEBUG, message(), args));
        }

        /// <summary>
        /// Errors the specified logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public static void Error(this ILogger logger, string message, params object[] args)
        {
            logger.Log(new LogEntry(LogLevel.ERROR, message, args));
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The arguments.</param>
        public static void Error(this ILogger logger, string message, Exception ex, params object[] args)
        {
            logger.Log(new LogEntry(LogLevel.ERROR, message, args, ex));
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public static void Error(this ILogger logger, Func<string> message, params object[] args)
        {
            logger.Log(new LogEntry(LogLevel.ERROR, message(), args));
        }

        /// <summary>
        /// Fatals the specified logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public static void Fatal(this ILogger logger, string message, params object[] args)
        {
            logger.Log(new LogEntry(LogLevel.FATAL, message, args));
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public static void Fatal(this ILogger logger, Func<string> message, params object[] args)
        {
            logger.Log(new LogEntry(LogLevel.FATAL, message(), args));
        }

        /// <summary>
        /// Informations the specified logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public static void Info(this ILogger logger, string message, params object[] args)
        {
            logger.Log(new LogEntry(LogLevel.INFO, message, args));
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public static void Info(this ILogger logger, Func<string> message, params object[] args)
        {
            logger.Log(new LogEntry(LogLevel.INFO, message(), args));
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void LogException(this ILogger logger, LogLevel logLevel, string message, Exception exception)
        {
            logger.Log(new LogEntry(logLevel, message, new object[0], exception));
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="exception">The exception.</param>
        public static void LogException(this ILogger logger, LogLevel logLevel, string message, object[] args, Exception exception)
        {
            logger.Log(new LogEntry(logLevel, message, args, exception));
        }

        /// <summary>
        /// Traces the specified logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public static void Trace(this ILogger logger, string message, params object[] args)
        {
            logger.Log(new LogEntry(LogLevel.TRACE, message, args));
        }

        /// <summary>
        /// Traces the specified message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public static void Trace(this ILogger logger, Func<string> message, params object[] args)
        {
            logger.Log(new LogEntry(LogLevel.TRACE, message(), args));
        }

        /// <summary>
        /// Warns the specified logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public static void Warn(this ILogger logger, string message, params object[] args)
        {
            logger.Log(new LogEntry(LogLevel.WARN, message, args));
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public static void Warn(this ILogger logger, Func<string> message, params object[] args)
        {
            logger.Log(new LogEntry(LogLevel.WARN, message(), args));
        }

        /// <summary>
        /// Log sql with execution time
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="functionName"></param>
        /// <param name="repositoryName"></param>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <param name="startTime"></param>
        public static void LogSql(this ILogger logger, string functionName, string repositoryName, string sqlQuery, long startTime, List<SqlParameter> parameters = null)
        {
            string query = string.Empty;
            if (parameters != null)
            {
                query = parameters.Aggregate(sqlQuery, (current, p) => current.Replace(p.ParameterName, p.Value.ToString()));
            }
            else
            {
                query = sqlQuery;
            }

            long queryExecutionTime = GetCurrentExecutionTime() - startTime;
            logger.Debug(
                "Repository - " + repositoryName + " || Method - " + functionName + Environment.NewLine + query
                + Environment.NewLine + " Execution Time :" + queryExecutionTime);
        }

        /// <summary>
        /// Log exception
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void LogException(this ILogger logger, string message, Exception exception)
        {
            logger.Log(new LogEntry(LogLevel.ERROR, message, new object[0], exception));
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <param name="exception"></param>
        public static void LogException(this ILogger logger, string message, object[] args, Exception exception)
        {
            logger.Log(new LogEntry(LogLevel.ERROR, message, args, exception));
        }

        /// <summary>
        /// LogInfo to trace method execution
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="functionName"></param>
        /// <param name="startTime"></param>
        public static void LogExecutionInfo(this ILogger logger, string functionName, long startTime)
        {
            long queryExecutionTime = GetCurrentExecutionTime() - startTime;
            logger.Debug(
                "Method - " + functionName + Environment.NewLine + " Execution Time :" + queryExecutionTime);
        }

        /// <summary>
        /// Get current execution time 
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentExecutionTime()
        {
            TimeSpan ts = new TimeSpan(DateTime.UtcNow.Ticks);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
    }
}
