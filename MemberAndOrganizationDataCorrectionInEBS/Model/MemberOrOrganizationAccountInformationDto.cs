using System.Collections.Generic;

namespace MemberAndOrganizationDataCorrectionInEBS.Model
{
    public class MemberOrOrganizationAccountInformationDto
    {
        public MemberOrOrganizationAccountDetailsDto Account { get; set; }

        public List<MemberOrOrganizationAccountRelationshipDetailsDto> Relationships { get; set; }

        public List<MemberOrOrganizationAccountCommunicationDetailsDto> Communication { get; set; }

        public List<MemberOrOrganizationAccountAddressDetailsDto> Addresses { get; set; }
    }
}
