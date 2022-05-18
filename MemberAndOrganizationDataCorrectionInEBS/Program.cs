using MemberAndOrganizationDataCorrectionInEBS.Extension;
using MemberAndOrganizationDataCorrectionInEBS.Interface;
using MemberAndOrganizationDataCorrectionInEBS.Model;
using MemberAndOrganizationDataCorrectionInEBS.Utility;
using System;
using System.Collections.Generic;
using System.IO;

namespace MemberAndOrganizationDataCorrectionInEBS
{
    class Program
    {
        /// <summary>
        /// Account number of an organization which CompanyId = 200 in MCS2.0
        /// </summary>
        const string OrganizationAccountNumber = "000113065";
        const string FileName = "AllMemberAndOrganizationAccountDetail.xlsx";
        const string AllMemberAndCorrectOrgJsonFileName = "AllMemberAndOrganizationAccountDetail.json";
        const string MemberAccountNumberColumnName = "MemberAccountNumber";
        const string MemberContactIdColumnName = "MemberContactId";
        const string OrgAccountNumberColumnName = "OrgAccountNumber";

        private readonly IMemberSystemLoggerService memberSystemLogger;
        private readonly IExternalService externalService;

        public Program()
        {
            this.memberSystemLogger = Instantiator.GetInstantiator().Get<IMemberSystemLoggerService>();
            this.externalService = Instantiator.GetInstantiator().Get<IExternalService>();
        }

        static void Main(string[] args)
        {
            var program = new Program();
            var logger = program.memberSystemLogger;
            var externalService = program.externalService;

            try
            {
                string error = string.Empty;
                bool isStatus = false;
                /////***********Load data from Json file that is created through manual process (will be used on Prod Environment)************//////
                List<MemberAndOrganizationAccountDetail> allMemberAndOrgAccountDetails = ReadDataFromJsonFile();

                if (allMemberAndOrgAccountDetails != null)
                {
                    CreateLog("MemberAccountNumber; MemberContactId; PreviousOrgAccount; UpdatedOrgAccount");

                    foreach (var memberAndOrgDetails in allMemberAndOrgAccountDetails)
                    {
                        logger.LogTrace($"MemberAccountNumber - {memberAndOrgDetails.MemberAccountNumber} MemberContactId - {memberAndOrgDetails.MemberContactId} PreviousOrgAccount - {OrganizationAccountNumber} UpdatedOrgAccount - {memberAndOrgDetails.OrgAccountNumber}");
                        CreateLog($"{memberAndOrgDetails.MemberAccountNumber}; {memberAndOrgDetails.MemberContactId}; {OrganizationAccountNumber}; {memberAndOrgDetails.OrgAccountNumber}");

                        //// This Mule API will break the relationship with Member Account & Fixed Org Account- 000113065 in EBS System
                        if (!string.IsNullOrEmpty(OrganizationAccountNumber) && memberAndOrgDetails.MemberContactId > 0)
                        {
                            (isStatus, error) = InActivateOrgAndMemberRelationInEBSSystem(externalService, memberAndOrgDetails);
                            if (isStatus)
                            {
                                //// This Mule API will create the relationship with Member Account & Correct Organization Account
                                if (!string.IsNullOrEmpty(memberAndOrgDetails.OrgAccountNumber) && !string.IsNullOrEmpty(memberAndOrgDetails.MemberAccountNumber))
                                {
                                    externalService.EBSOrganizationMemberRelation(memberAndOrgDetails.OrgAccountNumber, memberAndOrgDetails.MemberAccountNumber);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
        }

        private static(bool isStatus, string error) InActivateOrgAndMemberRelationInEBSSystem(IExternalService externalService, MemberAndOrganizationAccountDetail memberAndOrgAccountDetails)
        {
            var memberContactId = new InactiveContactsInputDto[]
            {
                new InactiveContactsInputDto
                {
                    ContactId = memberAndOrgAccountDetails.MemberContactId.ToString()
                }
            };

            return externalService.RemoveMemberAndOrganizationFacilityRelationshipInEBS(OrganizationAccountNumber, memberContactId);
        }

        public static List<MemberAndOrganizationAccountDetail> ReadDataFromJsonFile()
        {
            var allMemberAndCorrectOrgData = JsonFileReader.ReadJsonDataByFileName<List<MemberAndOrganizationAccountDetail>>(AllMemberAndCorrectOrgJsonFileName);
            return allMemberAndCorrectOrgData;
        }

        public static void CreateLog(string message)
        {
            using (StreamWriter writer = File.AppendText("Log.txt"))
            {
                writer.WriteLine(message);
            }
        }
    }
}
