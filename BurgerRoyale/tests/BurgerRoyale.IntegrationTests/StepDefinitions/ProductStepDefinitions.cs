using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Enumerators;
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

        [Given(@"I have a product added with category ""([^""]*)""")]
        public async Task GivenIHaveAProductAddedWithCategory(ProductCategory category)
        {
            var productRequest = new RequestProductDTO
            {
                Name = "Burger a moda da casa",
                Category = category,
                Description = "A delicious one",
                Price = 60
            };

            _scenarioContext["productRequest"] = productRequest;

            await WhenIRequestToAddTheProduct();

            var httpResponse = _scenarioContext.Get<HttpResponseMessage>("httpResponse");
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            var productAdded = httpResponse.DeserializeTo<ProductDTO>(responseContent);
            _scenarioContext["productAdded"] = productAdded;
        }

        [When(@"I get products given the category ""([^""]*)""")]
        public async Task WhenIGetProductsGivenTheCategory(ProductCategory category)
        {
            var httpResponse = await _httpClient.GetAsync($"{HttpClientRequest.Path}/api/Product?productCategory={category}");

            _scenarioContext["httpResponse"] = httpResponse;
        }

        [Then(@"I should only see the products with ""([^""]*)"" category")]
        public async Task ThenIShouldOnlySeeTheProductsWithCategory(ProductCategory category)
        {
            var httpResponse = _scenarioContext.Get<HttpResponseMessage>("httpResponse");
            httpResponse.EnsureSuccessStatusCode();

            var responseContent = await httpResponse.Content.ReadAsStringAsync();

            var products = httpResponse.DeserializeTo<IEnumerable<ProductDTO>>(responseContent);

            products.Should().HaveCountGreaterThanOrEqualTo(1);
            products.All(product => product.Category == category).Should().BeTrue();
        }

        [When(@"I get this product")]
        public async Task WhenIGetThisProduct()
        {
            var productAdded = _scenarioContext.Get<ProductDTO>("productAdded");

            var httpResponse = await _httpClient.GetAsync($"{HttpClientRequest.Path}/api/Product/{productAdded.Id}");
            _scenarioContext["httpResponse"] = httpResponse;
        }

        [Then(@"I should only see the product")]
        public async Task ThenIShouldOnlySeeTheProduct()
        {
            var productAdded = _scenarioContext.Get<ProductDTO>("productAdded");

            var httpResponse = _scenarioContext.Get<HttpResponseMessage>("httpResponse");
            httpResponse.EnsureSuccessStatusCode();

            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            var product = httpResponse.DeserializeTo<ProductDTO>(responseContent);

            product.Should().NotBeNull();
            product.Id.Should().Be(productAdded.Id);
            product.Name.Should().Be(productAdded.Name);
            product.Description.Should().Be(productAdded.Description);
            product.Category.Should().Be(productAdded.Category);
            product.Price.Should().Be(productAdded.Price);
        }
    }
}