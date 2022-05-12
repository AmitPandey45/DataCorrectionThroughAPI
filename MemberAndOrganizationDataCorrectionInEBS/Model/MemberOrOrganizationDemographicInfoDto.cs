namespace MemberAndOrganizationDataCorrectionInEBS.Model
{
    public class MemberOrOrganizationDemographicInfoDto
    {
        public string Result { get; set; }

        public string HasHasMoreRecords { get; set; }

        public MemberOrOrganizationAccountInformationDto Accounts { get; set; }
    }
}
