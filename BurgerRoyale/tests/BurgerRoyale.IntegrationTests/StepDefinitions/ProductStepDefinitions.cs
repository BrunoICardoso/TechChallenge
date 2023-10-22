using BurgerRoyale.Domain.DTO;
using BurgerRoyale.IntegrationTests.Helpers;
using System.Text.Json;
using TechTalk.SpecFlow.Assist;

namespace BurgerRoyale.IntegrationTests.StepDefinitions
{
    [Binding]
    public class ProductStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly HttpClient _httpClient;

        public ProductStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            _scenarioContext = scenarioContext;
            _httpClient = httpClient;
        }

        [Given(@"I want to add a product with the following data")]
        public void GivenIWantToAddAProductWithTheFollowingData(Table productData)
        {
            var productRequest = productData.CreateInstance<RequestProductDTO>();
            _scenarioContext["productRequest"] = productRequest;
        }

        [When(@"I request to add the product")]
        public async Task WhenIRequestToAddTheProduct()
        {
            RequestProductDTO productRequest = _scenarioContext.Get<RequestProductDTO>("productRequest");

            string productRequestJson = JsonSerializer.Serialize(productRequest);

            var httpResponse = await _httpClient.PostAsync(HttpClientRequest.Path, new StringContent(productRequestJson));

            _scenarioContext["httpResponse"] = httpResponse;
        }

        [Then(@"The product should be added")]
        public void ThenTheProductShouldBeAdded()
        {
            var httpResponse = _scenarioContext.Get<HttpResponseMessage>("httpResponse");

            httpResponse.EnsureSuccessStatusCode();
        }
    }
}