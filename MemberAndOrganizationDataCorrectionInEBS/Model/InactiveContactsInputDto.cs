using Newtonsoft.Json;
using System;

namespace MemberAndOrganizationDataCorrectionInEBS.Model
{
    public class InactiveContactsInputDto
    {
        [JsonProperty("contactId")]
        public string ContactId { get; set; }

        [JsonProperty("inactiveFlag")]
        public string InactiveFlag { get; set; } = "Y";

        [JsonProperty("inactiveDate")]
        public string InactiveDate { get; set; } = DateTime.UtcNow.ToString("dd-MMM-yyyy hh:mm:ss");
    }
}
