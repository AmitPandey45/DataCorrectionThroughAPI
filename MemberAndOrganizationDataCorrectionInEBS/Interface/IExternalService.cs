using MemberAndOrganizationDataCorrectionInEBS.Model;

namespace MemberAndOrganizationDataCorrectionInEBS.Interface
{
    public interface IExternalService
    {
        MemberOrOrganizationDemographicInfoDto DemoGraphicInformationCore(string orgOrmemberAccountNumber);

        (bool result, string error) EBSOrganizationMemberRelation(string organizationAccountNumber, string memberAccountNumber);

        (bool result, string error) RemoveMemberAndOrganizationFacilityRelationshipInEBS(string organizationAccountNumber, InactiveContactsInputDto[] allMemberContactId);
    }
}
