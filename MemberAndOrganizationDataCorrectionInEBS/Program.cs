using ExcelDataReader;
using IronXL;
using MemberAndOrganizationDataCorrectionInEBS.Extension;
using MemberAndOrganizationDataCorrectionInEBS.Interface;
using MemberAndOrganizationDataCorrectionInEBS.Model;
using MemberAndOrganizationDataCorrectionInEBS.Utility;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MemberAndOrganizationDataCorrectionInEBS
{
    class Program
    {
        /// <summary>
        /// Account number of an organization which CompanyId = 200 in MCS2.0
        /// </summary>
        const string OrganizationAccountNumber = "000113065";
        const string FileName = "AllMemberAndOrganizationAccountDetail.xlsx";
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
                ////List<MemberOrOrganizationAccountRelationshipDetailsDto> activeDataAfter31March2022 =
                ////    GetAllMemberAssociatedWithAnOrganization(externalService);

                ////CreateExcelSheetForAllActiveMembersAfter30March2022(activeDataAfter31March2022);

                ////return;

                ////List<MemberAndOrganizationAccountDetail> allMemberAndOrgAccountDetails =
                ////    LoadMemberAndOrganizationAccountDetailFromExelWithFreeLibrary(logger);

                DataTable dt = LoadMemberAndOrganizationAccountDetailFromExelAsDatatable();
                List<MemberAndOrganizationAccountDetail> allMemberAndOrgAccountDetails =
                    ConvertDataTableToList(dt);

                //// List<MemberAndOrganizationAccountDetail> allMemberAndOrgAccountDetails1 =
                ////     LoadMemberAndOrganizationAccountDetailFromExel();

                //// allMemberAndOrgAccountDetails1.RemoveAt(174);

                ////var diff =
                ////     allMemberAndOrgAccountDetails
                ////     .Where(s => !allMemberAndOrgAccountDetails1
                ////     .Any(t => s.MemberAccountNumber.Equals(t.MemberAccountNumber) &&
                ////     s.MemberContactId.Equals(t.MemberContactId) &&
                ////     s.OrgAccountNumber.Equals(t.OrgAccountNumber))
                ////     );

                var testingWithOnlyTwoDataSet = allMemberAndOrgAccountDetails.Where(s => !string.IsNullOrEmpty(s.OrgAccountNumber)).ToList();
                testingWithOnlyTwoDataSet = allMemberAndOrgAccountDetails;
                ////Console.Read();
                ////return;
                logger.LogTrace("=================MemberAndOrganizationDataCorrectionInEBS Process Start=================");

                foreach (var memberAndOrgDetails in testingWithOnlyTwoDataSet)
                {
                    logger.LogTrace($"******************Start the data correction for Member Account- {memberAndOrgDetails.MemberAccountNumber} Member ContactId- {memberAndOrgDetails.MemberContactId} Previous Organization Account- {OrganizationAccountNumber} Correct Organization Account- {memberAndOrgDetails.OrgAccountNumber}******************");

                    //// This Mule API will break the relationship with Member Account & Fixed Org Account- 000113065 in EBS System
                    if (!string.IsNullOrEmpty(OrganizationAccountNumber) && memberAndOrgDetails.MemberContactId > 0)
                    {
                        InActivateOrgAndMemberRelationInEBSSystem(externalService, memberAndOrgDetails);
                    }
                    //// This Mule API will create the relationship with Member Account & Correct Organization Account
                    if (!string.IsNullOrEmpty(memberAndOrgDetails.OrgAccountNumber) && !string.IsNullOrEmpty(memberAndOrgDetails.MemberAccountNumber))
                    {
                        externalService.EBSOrganizationMemberRelation(memberAndOrgDetails.OrgAccountNumber, memberAndOrgDetails.MemberAccountNumber);
                    }

                    logger.LogTrace($"******************End the data correction for Member Account- {memberAndOrgDetails.MemberAccountNumber} Member ContactId- {memberAndOrgDetails.MemberContactId} Previous Organization Account- {OrganizationAccountNumber} Correct Organization Account- {memberAndOrgDetails.OrgAccountNumber}******************");
                }

                logger.LogTrace("=================MemberAndOrganizationDataCorrectionInEBS Process End=================");

                ////return;

                ////logger.LogTrace("Program execuation start");

                ////List<MemberOrOrganizationAccountRelationshipDetailsDto> activeDataAfter31March2022 =
                ////    GetAllMemberAssociatedWithAnOrganization(externalService);

                ////CreateExcelSheetForAllActiveMembersAfter30March2022(activeDataAfter31March2022);

                ////logger.LogTrace("Program execuation end");
            }
            catch(Exception ex)
            {
                logger.LogException(ex);
            }
        }

        private static List<MemberOrOrganizationAccountRelationshipDetailsDto> GetAllMemberAssociatedWithAnOrganization(IExternalService externalService)
        {
            MemberOrOrganizationDemographicInfoDto demographicInfo = externalService.DemoGraphicInformationCore(OrganizationAccountNumber);

            List<MemberOrOrganizationAccountRelationshipDetailsDto> allMemberAssociatedWithOrg =
                demographicInfo.Accounts.Relationships;

            var activeData = allMemberAssociatedWithOrg.Where(m => m.RelationshipStatus.Equals("A")).ToList();
            var inActiveData = allMemberAssociatedWithOrg.Where(m => m.RelationshipStatus.Equals("I")).ToList();

            DateTime march31_2022 = new DateTime(2022, 03, 31);
            var activeDataAfter31March2022 = allMemberAssociatedWithOrg
                .Where(m => m.Start_Date >= march31_2022 && m.RelationshipStatus.Equals("A")).ToList();
            var inActiveDataAfter31March2022 = allMemberAssociatedWithOrg
                .Where(m => m.Start_Date >= march31_2022 && m.RelationshipStatus.Equals("I")).ToList();
            return activeData;
        }

        private static List<MemberAndOrganizationAccountDetail> LoadMemberAndOrganizationAccountDetailFromExel()
        {
            var allMemberAndOrgAccountDetails = new List<MemberAndOrganizationAccountDetail>();
            WorkBook workbook = WorkBook.Load(FileName);
            WorksheetsCollection workSheets = workbook.WorkSheets;
            foreach (WorkSheet sheet in workSheets)
            {
                DataTable dtWorkSheet = sheet.ToDataTable(true);

                IEnumerable<string> columns = dtWorkSheet.GetColumnNames();

                for (int i = 0; i < dtWorkSheet.Rows.Count; i++)
                {
                    allMemberAndOrgAccountDetails.Add(
                        new MemberAndOrganizationAccountDetail
                        {
                            MemberAccountNumber = dtWorkSheet.Rows[i][0].ToString(),
                            MemberContactId = !string.IsNullOrEmpty(dtWorkSheet.Rows[i][1].ToString()) ? Convert.ToInt64(dtWorkSheet.Rows[i][1].ToString()) : 0,
                            OrgAccountNumber = dtWorkSheet.Rows[i][2].ToString(),
                        });
                }
            }

            return allMemberAndOrgAccountDetails;
        }

        private static List<MemberAndOrganizationAccountDetail> LoadMemberAndOrganizationAccountDetailFromExelWithFreeLibrary(IMemberSystemLoggerService logger)
        {
            var allMemberAndOrgAccountDetails = new List<MemberAndOrganizationAccountDetail>();
            int iterator = 0;
            using (var stream = File.Open(FileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            if (iterator > 0)
                            {
                                var memberAndOrgAccountDetails = new MemberAndOrganizationAccountDetail();

                                try
                                {
                                    memberAndOrgAccountDetails.MemberAccountNumber = Convert.ToString(reader.GetValue(0));
                                    memberAndOrgAccountDetails.MemberContactId = !string.IsNullOrEmpty(Convert.ToString(reader.GetValue(1))) ? Convert.ToInt64(Convert.ToString(reader.GetValue(1))) : 0;
                                    memberAndOrgAccountDetails.OrgAccountNumber = Convert.ToString(reader.GetValue(2));
                                }
                                catch(Exception ex)
                                {
                                    logger.LogException(ex);
                                }

                                allMemberAndOrgAccountDetails.Add(memberAndOrgAccountDetails);
                            }

                            iterator++;
                        }
                    } while (reader.NextResult());
                }
            }

            return allMemberAndOrgAccountDetails;
        }

        private static void InActivateOrgAndMemberRelationInEBSSystem(IExternalService externalService, List<MemberAndOrganizationAccountDetail> allMemberAndOrgAccountDetails)
        {
            InactiveContactsInputDto[] allMemberContactId = allMemberAndOrgAccountDetails.Select(s =>
            new InactiveContactsInputDto
            {
                ContactId = s.MemberContactId.ToString()
            }).ToArray();

            externalService.RemoveMemberAndOrganizationFacilityRelationshipInEBS(OrganizationAccountNumber, allMemberContactId);
        }

        private static void InActivateOrgAndMemberRelationInEBSSystem(IExternalService externalService, MemberAndOrganizationAccountDetail memberAndOrgAccountDetails)
        {
            var memberContactId = new InactiveContactsInputDto[]
            {
                new InactiveContactsInputDto
                {
                    ContactId = memberAndOrgAccountDetails.MemberContactId.ToString()
                }
            };

            externalService.RemoveMemberAndOrganizationFacilityRelationshipInEBS(OrganizationAccountNumber, memberContactId);
        }

        private static void CreateExcelSheetForAllActiveMembersAfter30March2022(List<MemberOrOrganizationAccountRelationshipDetailsDto> activeDataAfter31March2022)
        {
            WorkBook wb = WorkBook.Create(ExcelFileFormat.XLSX);
            wb.Metadata.Author = "ASTM";
            WorkSheet ws = wb.DefaultWorkSheet;
            int rowCount = 1;

            ////var relationshipProperties = new MemberOrOrganizationAccountRelationshipDetailsDto();
            ////Type type = relationshipProperties.GetType();
            ////PropertyInfo[] properties = type.GetProperties();
            ////int columnIndex = 0;
            ////char sheetColumnHeader = 'A';
            ////char[] sheetColumnHeaders = new char[properties.Count()];

            ////foreach (PropertyInfo pi in properties)
            ////{
            ////    sheetColumnHeaders[columnIndex] = sheetColumnHeader;
            ////    ws[sheetColumnHeader.ToString() + (rowCount)].Value = pi.Name;
            ////    sheetColumnHeader++;
            ////    columnIndex++;
            ////}

            ws["A" + (rowCount)].Value = MemberAccountNumberColumnName;
            ws["B" + (rowCount)].Value = MemberContactIdColumnName;
            ws["C" + (rowCount)].Value = OrgAccountNumberColumnName;

            rowCount++;

            foreach (var item in activeDataAfter31March2022)
            {
                ws["A" + (rowCount)].Value = item.AccountNumber;
                ws["B" + (rowCount)].Value = item.ContactId;

                rowCount++;
            }
            wb.SaveAsCsv(FileName);
        }

        public static List<MemberAndOrganizationAccountDetail> ConvertDataTableToList(DataTable dt)
        {
            var allMemberAndOrgAccountDetails = new List<MemberAndOrganizationAccountDetail>();

            foreach (DataRow row in dt.Rows)
            {
                var memberAndOrgAccountDetails = new MemberAndOrganizationAccountDetail();
                memberAndOrgAccountDetails.MemberAccountNumber = Convert.ToString(row[MemberAccountNumberColumnName]);
                memberAndOrgAccountDetails.MemberContactId = !string.IsNullOrEmpty(Convert.ToString(row[MemberContactIdColumnName])) ? Convert.ToInt64(Convert.ToString(row[MemberContactIdColumnName])) : 0;
                memberAndOrgAccountDetails.OrgAccountNumber = Convert.ToString(row[OrgAccountNumberColumnName]);

                allMemberAndOrgAccountDetails.Add(memberAndOrgAccountDetails);
            }

            return allMemberAndOrgAccountDetails;
        }

        public static DataTable LoadMemberAndOrganizationAccountDetailFromExelAsDatatable()
        {
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0; data source={FileName}; Extended Properties=Excel 12.0;";
            DataTable dt = null;
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbDataAdapter objDA = new OleDbDataAdapter
                ("select * from [Sheet1$]", conn);
                DataSet excelDataSet = new DataSet();
                objDA.Fill(excelDataSet);
                dt = excelDataSet.Tables[0];
            }

            return dt;
        }
    }
}
