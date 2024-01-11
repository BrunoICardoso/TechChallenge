using Microsoft.AspNetCore.Mvc.Testing;

namespace BurgerRoyale.IntegrationTests.Helpers;

public class HttpClientInject
{
    private readonly WebApplicationFactory<Program> progam;

    internal HttpClientInject(WebApplicationFactory<Program> progam)
    {
        this.progam = progam;
    }

    internal WebApplicationFactory<Program> GetProgram()
    {
        return progam;
    }
}