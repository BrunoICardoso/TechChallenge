using BoDi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BurgerRoyale.IntegrationTests.Hooks;

[Binding]
public sealed class EnvironmentSetupHooks
{
    [BeforeTestRun]
    public static void BeforeTestRun(IObjectContainer testThreadContainer)
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Development");
            });

        HttpClient httpClient = application.CreateClient();

        testThreadContainer.RegisterInstanceAs(httpClient);
    }
}