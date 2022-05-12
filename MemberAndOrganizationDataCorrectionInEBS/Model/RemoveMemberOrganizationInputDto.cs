using Newtonsoft.Json;

namespace MemberAndOrganizationDataCorrectionInEBS.Model
{
    public class RemoveMemberOrganizationInputDto
    {
        [JsonProperty("accountNumber")]
        public string OrganizationAccountNumber { get; set; }

        [JsonProperty("userType")]
        public string UserType { get; set; }

        [JsonProperty("createdByModule")]
        public string CreatedByModule { get; set; }

        [JsonProperty("attachedTo")]
        public string AttachedTo { get; set; }

        [JsonProperty("siteId")]
        public string SiteId { get; set; }

        [JsonProperty("inactiveContacts")]
        public InactiveContactsInputDto[] InactiveContacts { get; set; }
    }
}
