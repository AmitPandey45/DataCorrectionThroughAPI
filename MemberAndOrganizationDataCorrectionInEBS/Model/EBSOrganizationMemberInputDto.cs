using Newtonsoft.Json;

namespace MemberAndOrganizationDataCorrectionInEBS.Model
{
    public class EBSOrganizationMemberInputDto
    {
        [JsonProperty("userType")]
        public string UserType { get; set; }

        [JsonProperty("createByModule")]
        public string CreateByModule { get; set; }

        [JsonProperty("employeeAccount")]
        public string MemberAccountNumber { get; set; }
    }
}
