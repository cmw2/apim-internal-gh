using System;
using Xunit;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace apim_internal.Tests
{
    public class APIInternatIntegrationTests
    {
        private static readonly HttpClient client = new HttpClient();
        IConfiguration Configuration { get; set; }

        public APIInternatIntegrationTests()
        {
            // the type specified here is just so the secrets library can 
            // find the UserSecretId we added in the csproj file
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<APIInternatIntegrationTests>();

            Configuration = builder.Build();            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", GetSubscriptionKey());
        }

        [Fact]
        public async Task GetSpeakersShouldReturnSuccess()
        {            
            HttpResponseMessage response = await client.GetAsync(GetBaseUrl() + "/speakers");           
            string responseBody = await response.Content.ReadAsStringAsync();
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetSessionsShouldReturnSuccess()
        {            
            HttpResponseMessage response = await client.GetAsync(GetBaseUrl() + "/sessions");           
            string responseBody = await response.Content.ReadAsStringAsync();
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetTopicsShouldReturnSuccess()
        {            
            HttpResponseMessage response = await client.GetAsync(GetBaseUrl() + "/topics");           
            string responseBody = await response.Content.ReadAsStringAsync();
            Assert.True(response.IsSuccessStatusCode);
        }

        private string GetBaseUrl() {
            string baseUrl = Environment.GetEnvironmentVariable("DemoConfApiBaseUrl");
            if (String.IsNullOrWhiteSpace(baseUrl)) {
                baseUrl = "https://apim-internal-dev-eastus.azure-api.net/conference";
            }

            return baseUrl;
        }

        private string GetSubscriptionKey() {
            string subKey = Configuration["DemoConfApiSubscriptionKey"];
            if (String.IsNullOrWhiteSpace(subKey)) {
                subKey = Environment.GetEnvironmentVariable("DemoConfApiSubscriptionKey");
            }

            if (String.IsNullOrWhiteSpace(subKey)) {
                throw new ArgumentException("missing DemoConfApiSubscriptionKey");
            }

            return subKey;
        }
    }
}
