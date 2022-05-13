using MemberAndOrganizationDataCorrectionInEBS.Interface;
using MemberAndOrganizationDataCorrectionInEBS.Model;
using MemberAndOrganizationDataCorrectionInEBS.Utility;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace MemberAndOrganizationDataCorrectionInEBS.Implementation
{
    public class ExternalService : IExternalService
    {
        private const int FatalStatusCode = 503;
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly IMemberSystemLoggerService _memberSystemLogger;
        private readonly HttpClient accountManagementHttpClient = null;
        private readonly HttpClient orderManagementHttpClient = null;

        public ExternalService(IHttpClientWrapper httpClientWrapper, IMemberSystemLoggerService memberSystemLogger)
        {
            this._httpClientWrapper = httpClientWrapper;
            this._memberSystemLogger = memberSystemLogger;

            this.accountManagementHttpClient = this._httpClientWrapper.GetAccountManagementHttpClient;
            this.orderManagementHttpClient = this._httpClientWrapper.GetOrderManagementHttpClient;
        }

        public string GetAccountManagementBaseUrl()
        {
            return string.Concat(ConfigurationManager.AppSettings[Constants.ACCOUNTMANAGEMENTAPIURL], Constants.DEMOGRAPHICURL)?.Trim();
        }

        public string GetOrderManagementBaseUrl()
        {
            return ConfigurationManager.AppSettings[Constants.ORDERMANAGEMENTCORAPIURL];
        }

        public MemberOrOrganizationDemographicInfoDto DemoGraphicInformationCore(string orgOrmemberAccountNumber)
        {
            _memberSystemLogger.LogTrace($"%%%%%%%%%%%%%%DemoGraphicInformationCore Mule API Process Start%%%%%%%%%%%%%%");
            MemberOrOrganizationDemographicInfoDto demographicInfo = null;
            string error = string.Empty;
            try
            {
                string apiBaseUrl = this.GetAccountManagementBaseUrl();
                string primaryAddressType = ConfigurationManager.AppSettings[Constants.PRIMARYADDRESSTYPE];
                var demographiInfoCoreApiUrl = string.Concat(apiBaseUrl, "?accountNumber=", orgOrmemberAccountNumber);
                this._memberSystemLogger.LogTrace(demographiInfoCoreApiUrl);
                var getTask = this._httpClientWrapper.GetAsync(this.accountManagementHttpClient, demographiInfoCoreApiUrl);
                getTask.Wait();
                dynamic jObject = this.GetParsedResponseData(getTask.Result);
                if (getTask.Result.IsSuccessStatusCode)
                {
                    var serializedData = JsonConvert.SerializeObject(jObject);
                    demographicInfo = JsonConvert.DeserializeObject<MemberOrOrganizationDemographicInfoDto>(serializedData);

                    DateTime dt;
                    demographicInfo.Accounts.Relationships =
                        demographicInfo?.Accounts?.Relationships.Select(item =>
                        {
                            DateTime.TryParseExact(item.StartDate.Substring(0, 9), "dd-MMM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                            item.Start_Date = dt;

                            DateTime.TryParseExact(item.EndDate.Substring(0, 9), "dd-MMM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                            item.End_Date = dt;

                            return item;
                        }).ToList();
                }
                else
                {
                    error = $"{Constants.EbsApiErrorGetDemoGraphicInformation}{Constants.SingleSpace}{JsonConvert.SerializeObject(jObject)}";
                    this._memberSystemLogger.LogFatal(error, FatalStatusCode);
                }
            }
            catch (Exception ex)
            {
                error = string.IsNullOrEmpty(ex.Message) ? ex.InnerException?.Message.Trim() : ex.Message;
                error = $"{Constants.EbsApiErrorGetDemoGraphicInformation}{Constants.SingleSpace}{error}";
                this._memberSystemLogger.LogException(ex, Constants.EbsApiErrorGetDemoGraphicInformation);
            }

            _memberSystemLogger.LogTrace($"%%%%%%%%%%%%%%DemoGraphicInformationCore Mule API Process End%%%%%%%%%%%%%%");

            return demographicInfo;
        }

        public(bool result, string error) EBSOrganizationMemberRelation(string organizationAccountNumber, string memberAccountNumber)
        {
            _memberSystemLogger.LogTrace($"$$$$$$$$$$$$$$$$EBSOrganizationMemberRelation Mule API Process Start$$$$$$$$$$$$$$$$");
            bool isRelation = false;
            string error = string.Empty;
            try
            {
                string apiBaseUrl = this.GetAccountManagementBaseUrl();
                var url = string.Concat(apiBaseUrl, "/", organizationAccountNumber, Constants.ACCOUNTDETAILURL);
                this._memberSystemLogger.LogTrace(url);
                string json = JsonConvert.SerializeObject(new EBSOrganizationMemberInputDto { CreateByModule = Constants.HZCPUI, MemberAccountNumber = memberAccountNumber, UserType = Constants.ADMIN });
                this._memberSystemLogger.LogTrace($"EBSOrganizationMemberRelation Request Payload: {json}");
                var putTask = this._httpClientWrapper.PutAsync(this.accountManagementHttpClient, url, new StringContent(json, Encoding.UTF8, Constants.JSONMEDIATYPE));
                putTask.Wait();
                dynamic jObject = this.GetParsedResponseData(putTask.Result);
                this._memberSystemLogger.LogTrace($"EBSOrganizationMemberRelation Response Payload: {JsonConvert.SerializeObject(jObject)}");
                if (putTask.Result.IsSuccessStatusCode)
                {
                    int? responseStatus = Convert.ToInt32(jObject?.returnstatus);
                    if (responseStatus != null && responseStatus.Equals((int)System.Net.HttpStatusCode.OK))
                    {
                        isRelation = true;
                    }
                    else
                    {
                        error = $"{Constants.EbsApiErrorOrganizationMemberAccountRelationship}{Constants.SingleSpace}{JsonConvert.SerializeObject(jObject)}";
                        this._memberSystemLogger.LogFatal(error, FatalStatusCode);
                    }
                }
                else
                {
                    error = $"{Constants.EbsApiErrorOrganizationMemberAccountRelationship}{Constants.SingleSpace}{JsonConvert.SerializeObject(jObject)}";
                    this._memberSystemLogger.LogFatal(error, FatalStatusCode);
                }
            }
            catch (Exception ex)
            {
                error = string.IsNullOrEmpty(ex.Message) ? ex.InnerException?.Message.Trim() : ex.Message;
                error = $"{Constants.EbsApiErrorOrganizationMemberAccountRelationship}{Constants.SingleSpace}{error}";
                this._memberSystemLogger.LogException(ex, Constants.EbsApiErrorOrganizationMemberAccountRelationship);
            }

            _memberSystemLogger.LogTrace($"$$$$$$$$$$$$$$$$EBSOrganizationMemberRelation Mule API Process End$$$$$$$$$$$$$$$$");
            return (isRelation, error);
        }

        public (bool result, string error) RemoveMemberAndOrganizationFacilityRelationshipInEBS(
            string organizationAccountNumber,
            InactiveContactsInputDto[] allMemberContactId)
        {
            _memberSystemLogger.LogTrace($"#################RemoveMemberAndOrganizationFacilityRelationshipInEBS Mule API Process Start#################");
            bool isMemberOrgFacilityRelationshipInactivated = false;
            string error = string.Empty;
            try
            {
                string apiBaseUrl = this.GetAccountManagementBaseUrl();
                var url = string.Concat(apiBaseUrl, "/", organizationAccountNumber, "/", Constants.AccountContacts);
                this._memberSystemLogger.LogTrace(url);
                RemoveMemberOrganizationInputDto removeMemberOrganizationInput =
                    this.GetRemoveMemberOrganizationRelationshipRequestPayload(organizationAccountNumber, allMemberContactId);

                string json = JsonConvert.SerializeObject(removeMemberOrganizationInput);
                this._memberSystemLogger.LogTrace($"RemoveMemberAndOrganizationFacilityRelationshipInEBS Request Payload: {json}");
                var putTask = this._httpClientWrapper.PutAsync(this.accountManagementHttpClient, url, new StringContent(json, Encoding.UTF8, Constants.JSONMEDIATYPE));
                putTask.Wait();
                dynamic jObject = this.GetParsedResponseData(putTask.Result);
                this._memberSystemLogger.LogTrace($"RemoveMemberAndOrganizationFacilityRelationshipInEBS Response Payload: {JsonConvert.SerializeObject(jObject)}");
                if (putTask.Result.IsSuccessStatusCode)
                {
                    int? responseStatus = Convert.ToInt32(jObject?.returnstatus);
                    if (responseStatus != null && responseStatus.Equals((int)System.Net.HttpStatusCode.OK))
                    {
                        isMemberOrgFacilityRelationshipInactivated = true;
                    }
                    else
                    {
                        error = $"{Constants.EbsApiErrorRemoveMemberAndOrganizationFacilityRelationshipInEBS}{Constants.SingleSpace}{JsonConvert.SerializeObject(jObject)}";
                        this._memberSystemLogger.LogFatal(error, FatalStatusCode);
                    }
                }
                else
                {
                    error = $"{Constants.EbsApiErrorRemoveMemberAndOrganizationFacilityRelationshipInEBS}{Constants.SingleSpace}{JsonConvert.SerializeObject(jObject)}";
                    this._memberSystemLogger.LogFatal(error, FatalStatusCode);
                }
            }
            catch (Exception ex)
            {
                error = string.IsNullOrEmpty(ex.Message) ? ex.InnerException?.Message.Trim() : ex.Message;
                error = $"{Constants.EbsApiErrorRemoveMemberAndOrganizationFacilityRelationshipInEBS}{Constants.SingleSpace}{error}";
                this._memberSystemLogger.LogException(ex, Constants.EbsApiErrorRemoveMemberAndOrganizationFacilityRelationshipInEBS);
            }

            _memberSystemLogger.LogTrace($"#################RemoveMemberAndOrganizationFacilityRelationshipInEBS Mule API Process End#################");

            return (isMemberOrgFacilityRelationshipInactivated, error);
        }

        private dynamic GetParsedResponseData(HttpResponseMessage httpResponseMessage)
        {
            var responseStream = httpResponseMessage.Content.ReadAsStreamAsync().Result;
            var serializer = new JsonSerializer();
            var sr = new StreamReader(responseStream);
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                try
                {
                    return serializer.Deserialize<dynamic>(jsonTextReader);
                }
                catch
                {
                    return null;
                }
            }
        }

        private long GetPartyId(string accountNumber)
        {
            return Convert.ToInt64(this.DemoGraphicInformationCore(accountNumber)?.Accounts?.Account?.PartyId);
        }

        private RemoveMemberOrganizationInputDto GetRemoveMemberOrganizationRelationshipRequestPayload(
            string organizationAccountNumber,
            InactiveContactsInputDto[] allMemberContactId)
        {
            return new RemoveMemberOrganizationInputDto
            {
                OrganizationAccountNumber = organizationAccountNumber,
                UserType = Constants.ADMIN,
                CreatedByModule = Constants.HZCPUI,
                AttachedTo = Constants.AttachedTo,
                InactiveContacts = allMemberContactId
            };
        }
    }
}
