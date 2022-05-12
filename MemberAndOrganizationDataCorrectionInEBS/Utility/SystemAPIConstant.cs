using MemberAndOrganizationDataCorrectionInEBS.Model;

namespace MemberAndOrganizationDataCorrectionInEBS.Utility
{
    public static class SystemAPIConstant
    {
        public const string MEMStockStartCode = "MEM";
        public const string LoggingMemberSystemAPI = "LoggingMemberSystemAPI";
        public static readonly string ErrorLogLevel = LogLevel.ERROR.ToString();
        public static readonly string WarnLogLevel = LogLevel.WARN.ToString();
        public static readonly string FatalLogLevel = LogLevel.FATAL.ToString();
    }
}
