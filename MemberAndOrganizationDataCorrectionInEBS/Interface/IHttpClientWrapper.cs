namespace MemberAndOrganizationDataCorrectionInEBS.Interface
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IHttpClientWrapper
    {
        HttpClient GetAccountManagementHttpClient { get; }

        HttpClient GetOrderManagementHttpClient { get; }

        Task<HttpResponseMessage> GetAsync(HttpClient httpClient, string requestUri);

        Task<HttpResponseMessage> PostAsync(HttpClient httpClient, string requestUri, HttpContent content);

        Task<HttpResponseMessage> PutAsync(HttpClient httpClient, string requestUri, HttpContent content);

        Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpRequestMessage request);
    }
}
