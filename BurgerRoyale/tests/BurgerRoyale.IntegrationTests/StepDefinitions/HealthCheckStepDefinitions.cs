using BurgerRoyale.IntegrationTests.Helpers;

namespace BurgerRoyale.IntegrationTests.StepDefinitions;

[Binding]
public class HealthCheckStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;
    
    private readonly HttpClient _httpClient;

    public HealthCheckStepDefinitions(ScenarioContext scenarioContext, HttpClientInject httpClientInject)
    {
        _scenarioContext = scenarioContext;
        _httpClient = httpClientInject.GetProgram().CreateClient();
    }

    [Given(@"I want to check the status of my application")]
    public void GivenIWantToCheckTheStatusOfMyApplication()
    {
    }

    [When(@"I check the application status")]
    public async Task WhenICheckTheApplicationStatus()
    {
        var httpResponse = await _httpClient.GetAsync($"{HttpClientRequest.Path}/health");
        _scenarioContext["healthCheckHttpResponse"] = httpResponse;
    }

    [Then(@"I should see the status as ok")]
    public void ThenIShouldSeeTheStatusAsOk()
    {
        var httpResponse = _scenarioContext.Get<HttpResponseMessage>("healthCheckHttpResponse");
        httpResponse.EnsureSuccessStatusCode();
    }
}