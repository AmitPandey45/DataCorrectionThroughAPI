using System;

namespace MemberAndOrganizationDataCorrectionInEBS.Interface
{
    public interface IMemberSystemLoggerService
    {
        void LogException(Exception exception, string prefixMessage = null, string postfixMessage = null);

        void LogWarning(string message, string prefixMessage = null, string postfixMessage = null);

        void LogFatal(string message, int? statusCode = null, string prefixMessage = null, string postfixMessage = null);

        void LogInfo(string message, int? statusCode = null, string prefixMessage = null, string postfixMessage = null);

        void LogDebug(string message, int? statusCode = null, string prefixMessage = null, string postfixMessage = null);

        void LogTrace(string message, int? statusCode = null, string prefixMessage = null, string postfixMessage = null);
    }
}
