namespace MemberAndOrganizationDataCorrectionInEBS.Utility
{
    public static class Constants
    {
        public const string ADMIN = "ADMIN";
        public const string CLIENTID = "client_id";
        public const string CLIENTSECRET = "client_secret";
        public const string JSONMEDIATYPE = "application/json";

        public const string HZCPUI = "HZ_CPUI";
        public const string AttachedTo = "ACCOUNT";
        public const string ACCOUNTUPDATE = "N";
        public const string DEMOGRAPHICURL = "accounts";
        public const string ORDERHISTORYCORURL = "orders/history";
        public const string ACCOUNTDETAILURL = "/account-details";
        public const string AccountContacts = "contacts";
        public const string CUSTOMERCLASSCODE = "NON-MEMBER";
        public const string ACCOUNTPARTYCREATION = "ORGANIZATION";
        public const string PRIMARYADDRESSTYPE = "PrimaryAddressType";
        public const string ACCOUNTMANAGEMENTCORCLIENTID = "AccountManagementCorClientId";
        public const string ACCOUNTMANAGEMENTCORCLIENTSECRET = "AccountManagementCorClientSecret";
        public const string ACCOUNTMANAGEMENTAPIURL = "AccountManagementAPIUrl";
        public const string ORDERMANAGEMENTCORAPIURL = "OrderManagementCorAPIUrl";
        public const string ORDERMANAGEMENTCORCLIENTID = "OrderManagementCorClientId";
        public const string ORDERMANAGEMENTCORCLIENTSECRET = "OrderManagementCorClientSecret";
        public const string ORDERHISTORYRESPONSEPROPERTY = "order-history";
        public const string SHIPTOFLAG = "ShipToFlag";

        public const string TAXAPIURL = "TaxAPIUrl";
        public const string TAXCLIENTID = "TaxClientId";
        public const string TAXCLIENTSECRET = "TaxClientSecret";

        public const string EbsApiErrorGetDemoGraphicInformation = "[EBS API ERROR - Get Demo Graphic Information Core]:";
        public const string EbsApiErrorCreateNewOrgAccount = "[EBS API ERROR - Create New Organizational Account]:";
        public const string EbsApiErrorOrganizationMemberAccountRelationship = "[EBS API ERROR - Organization Member Account Relationship]:";
        public const string EbsApiErrorRemoveMemberAndOrganizationFacilityRelationshipInEBS = "[EBS API ERROR - Remove Member and Organization/Facility Relationship in EBS]:";
        public const string EbsApiErrorGetMemberOrderHistory = "[EBS API ERROR - Get Member Order History]:";
        public const string EbsApiErrorGetCalculateTax = "[EBS API ERROR - Get Calculate Tax]:";
        public const string EbsApiErrorGetOrderDetails = "[EBS API ERROR - Order Details By OrderNumber]:";
        public const string EbsApiValidationErrorOrderDetailsNotFound = "[EBS API Validation Error - Order Details By OrderNumber]: Order Details could not found for Order Number: {0}";
        public const string EbsApiValidationErrorOrderDetailsMemberAccountNotMatch = "[EBS API Validation Error - Order Details By OrderNumber]: Order Details Member Account Number: {0} is not matched with processing Member Account Number: {1} for Order Number: {2}";
        public const string EbsApiErrorGetPrepaidMembers = "[EBS API ERROR - Get Prepaid Members]:";
        public const string EbsApiErrorUpdateOrganizationAccount = "[EBS API ERROR - Update Organization Account]:";
        public const string SingleSpace = " ";
        public const string MessageSeparator = "-";
        public const string PrimaryFlag = "Y";
        public const string Email = "EMAIL";
        public const string Phone = "PHONE";
        public const string Fax = "FAX";
        public const int PageLimit = 1000;
        public const string Yes = "Y";
        public const string CanadaCountryCode = "CA";
        public static readonly string EmptyString = string.Empty;
    }
}
