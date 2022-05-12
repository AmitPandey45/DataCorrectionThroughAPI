namespace MemberAndOrganizationDataCorrectionInEBS.Model
{
    public class NLogDetails
    {
        public string TimeStamp { get; internal set; }

        public string LogInfo { get; internal set; }

        public string LogLevel { get; internal set; }

        public int ThreadId { get; internal set; }

        public string TenantId { get; internal set; }

        public string AccountId { get; internal set; }

        public string UserId { get; internal set; }

        public string Api { get; internal set; }

        public string Message { get; internal set; }

        public string HttpMethod { get; internal set; }

        public string HostName { get; internal set; }

        public int? HttpStatusCode { get; internal set; }
    }
}
