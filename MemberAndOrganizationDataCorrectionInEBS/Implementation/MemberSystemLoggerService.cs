using MemberAndOrganizationDataCorrectionInEBS.Extension;
using MemberAndOrganizationDataCorrectionInEBS.Interface;
using MemberAndOrganizationDataCorrectionInEBS.Model;
using MemberAndOrganizationDataCorrectionInEBS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberAndOrganizationDataCorrectionInEBS.Implementation
{
    public class MemberSystemLoggerService : IMemberSystemLoggerService
    {
        private readonly ILogger _logger;

        public MemberSystemLoggerService(ILogger logger)
        {
            this._logger = logger;
        }

        public void LogException(Exception exception, string prefixMessage = null, string postfixMessage = null)
        {
            int statusCode = 500;
            string message = exception.ToString();
            message = prefixMessage == null ? message : $"{prefixMessage}{Constants.SingleSpace}{message}";
            message = postfixMessage == null ? message : $"{postfixMessage}{Constants.SingleSpace}{message}";
            string finalJson = CustomLog.GetCustomLog(SystemAPIConstant.LoggingMemberSystemAPI, SystemAPIConstant.FatalLogLevel, message, statusCode);
            this._logger.LogException(LogLevel.ERROR, finalJson, null);
        }

        public void LogWarning(string message, string prefixMessage = null, string postfixMessage = null)
        {
            int statusCode = 200;
            message = prefixMessage == null ? message : $"{prefixMessage}{Constants.SingleSpace}{message}";
            message = postfixMessage == null ? message : $"{postfixMessage}{Constants.SingleSpace}{message}";
            string finalJson = CustomLog.GetCustomLog(SystemAPIConstant.LoggingMemberSystemAPI, SystemAPIConstant.WarnLogLevel, message, statusCode);
            this._logger.Warn(finalJson, null);
        }

        public void LogTrace(string message, int? statusCode = null, string prefixMessage = null, string postfixMessage = null)
        {
            message = prefixMessage == null ? message : $"{prefixMessage}{Constants.SingleSpace}{message}";
            message = postfixMessage == null ? message : $"{postfixMessage}{Constants.SingleSpace}{message}";
            string finalJson = CustomLog.GetCustomLog(SystemAPIConstant.LoggingMemberSystemAPI, "TRACE", message, statusCode);
            LogEntry logEntry = new LogEntry(LogLevel.TRACE, finalJson);
            this._logger.Log(logEntry);
        }

        public void LogDebug(string message, int? statusCode = null, string prefixMessage = null, string postfixMessage = null)
        {
            message = prefixMessage == null ? message : $"{prefixMessage}{Constants.SingleSpace}{message}";
            message = postfixMessage == null ? message : $"{postfixMessage}{Constants.SingleSpace}{message}";
            string finalJson = CustomLog.GetCustomLog(SystemAPIConstant.LoggingMemberSystemAPI, "TRACE", message, statusCode);
            LogEntry logEntry = new LogEntry(LogLevel.DEBUG, finalJson);
            this._logger.Log(logEntry);
        }

        public void LogInfo(string message, int? statusCode = null, string prefixMessage = null, string postfixMessage = null)
        {
            message = prefixMessage == null ? message : $"{prefixMessage}{Constants.SingleSpace}{message}";
            message = postfixMessage == null ? message : $"{postfixMessage}{Constants.SingleSpace}{message}";
            string finalJson = CustomLog.GetCustomLog(SystemAPIConstant.LoggingMemberSystemAPI, "TRACE", message, statusCode);
            LogEntry logEntry = new LogEntry(LogLevel.INFO, finalJson);
            this._logger.Log(logEntry);
        }

        public void LogFatal(string message, int? statusCode = null, string prefixMessage = null, string postfixMessage = null)
        {
            statusCode = statusCode == null ? 503 : statusCode;
            message = prefixMessage == null ? message : $"{prefixMessage}{Constants.SingleSpace}{message}";
            message = postfixMessage == null ? message : $"{postfixMessage}{Constants.SingleSpace}{message}";
            string finalJson = CustomLog.GetCustomLog(SystemAPIConstant.LoggingMemberSystemAPI, "FATAL", message, statusCode);
            LogEntry logEntry = new LogEntry(LogLevel.FATAL, finalJson);
            this._logger.Log(logEntry);
        }
    }
}
