using BurgerRoyale.Domain.DTO;
using BurgerRoyale.IntegrationTests.Extensions;
using BurgerRoyale.IntegrationTests.Helpers;
using TechTalk.SpecFlow.Assist;

namespace BurgerRoyale.IntegrationTests.StepDefinitions
{
    [Binding]
    public class ProductStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly HttpClient _httpClient;

        public ProductStepDefinitions(ScenarioContext scenarioContext, HttpClientInject httpClientInject)
        {
            _scenarioContext = scenarioContext;
            _httpClient = httpClientInject.GetProgram().CreateClient();
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

            string productRequestJson = System.Text.Json.JsonSerializer.Serialize(productRequest);

            var productStringContent = StringContentHelper.Create(productRequestJson);

            var httpResponse = await _httpClient.PostAsync($"{HttpClientRequest.Path}/api/Product", productStringContent);

            _scenarioContext["httpResponse"] = httpResponse;
        }

        [Then(@"The product should be added")]
        public async Task ThenTheProductShouldBeAdded()
        {
            var httpResponse = _scenarioContext.Get<HttpResponseMessage>("httpResponse");

            httpResponse.EnsureSuccessStatusCode();

            var request = _scenarioContext.Get<RequestProductDTO>("productRequest");

            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            var response = httpResponse.DeserializeTo<ProductDTO>(responseContent);

            response.Should().NotBeNull();

            response!.Id.Should().NotBeEmpty();
            response.Name.Should().Be(request.Name);
            response.Category.Should().Be(request.Category);
            response.Description.Should().Be(request.Description);
            response.Price.Should().Be(request.Price);
        }
    }
}