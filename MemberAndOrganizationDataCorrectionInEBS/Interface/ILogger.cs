using MemberAndOrganizationDataCorrectionInEBS.Model;

namespace MemberAndOrganizationDataCorrectionInEBS.Interface
{
    /// <summary>
    /// Default logger interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Flush log
        /// </summary>
        void Flush();

        /// <summary>
        /// Add log entry
        /// </summary>
        /// <param name="logEntry">Element to log</param>
        void Log(LogEntry logEntry);
    }
}
