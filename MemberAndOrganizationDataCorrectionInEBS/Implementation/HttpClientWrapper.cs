namespace MemberAndOrganizationDataCorrectionInEBS.Implementation
{
    using MemberAndOrganizationDataCorrectionInEBS.Interface;
    using MemberAndOrganizationDataCorrectionInEBS.Utility;
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class HttpClientWrapper : IHttpClientWrapper
    {
        private static readonly HttpClient AccountManagementHttpClient = new HttpClient();
        private static readonly HttpClient OrderManagementHttpClient = new HttpClient();

        static HttpClientWrapper()
        {
            AddHttpClientRequestHeadersForAccountManagement();
            AddHttpClientRequestHeadersForOrderManagement();
        }

        public HttpClient GetAccountManagementHttpClient
        {
            get
            {
                return AccountManagementHttpClient;
            }
        }

        public HttpClient GetOrderManagementHttpClient
        {
            get
            {
                return OrderManagementHttpClient;
            }
        }

        public static(string clientId, string clientSecret) GetAccountManagementConfigKeyValues()
        {
            return (ConfigurationManager.AppSettings[Constants.ACCOUNTMANAGEMENTCORCLIENTID]?.Trim(),
                ConfigurationManager.AppSettings[Constants.ACCOUNTMANAGEMENTCORCLIENTSECRET]?.Trim());
        }

        public static(string clientId, string clientSecret) GetOrderManagementConfigKeyValues()
        {
            return (ConfigurationManager.AppSettings[Constants.ORDERMANAGEMENTCORCLIENTID]?.Trim(),
                ConfigurationManager.AppSettings[Constants.ORDERMANAGEMENTCORCLIENTSECRET]?.Trim());
        }

        public Task<HttpResponseMessage> GetAsync(HttpClient httpClient, string requestUri)
        {
            return httpClient.GetAsync(requestUri);
        }

        public Task<HttpResponseMessage> PostAsync(HttpClient httpClient, string url, HttpContent content)
        {
            return httpClient.PostAsync(new Uri(url), content);
        }

        public Task<HttpResponseMessage> PutAsync(HttpClient httpClient, string url, HttpContent content)
        {
            return httpClient.PutAsync(new Uri(url), content);
        }

        public Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpRequestMessage request)
        {
            return httpClient.SendAsync(request);
        }

        private static void AddHttpClientRequestHeadersForAccountManagement()
        {
            if (AccountManagementHttpClient.DefaultRequestHeaders.Count() == 0)
            {
                (string clientId, string clientSecret) configValues = GetAccountManagementConfigKeyValues();

                AccountManagementHttpClient.DefaultRequestHeaders.Add(Constants.CLIENTSECRET, configValues.clientSecret);
                AccountManagementHttpClient.DefaultRequestHeaders.Add(Constants.CLIENTID, configValues.clientId);
            }
        }

        private static void AddHttpClientRequestHeadersForOrderManagement()
        {
            if (OrderManagementHttpClient.DefaultRequestHeaders.Count() == 0)
            {
                (string clientId, string clientSecret) configValues = GetOrderManagementConfigKeyValues();

                OrderManagementHttpClient.DefaultRequestHeaders.Add(Constants.CLIENTSECRET, configValues.clientSecret);
                OrderManagementHttpClient.DefaultRequestHeaders.Add(Constants.CLIENTID, configValues.clientId);
            }
        }
    }
}
