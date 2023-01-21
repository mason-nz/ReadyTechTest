using System.Net;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit.Abstractions;

namespace ReadyTech.BrewCoffee2023.API.Test;

public class UnitTestBrewCoffee
{
    public UnitTestBrewCoffee(ITestOutputHelper outputHelper)
    {
        Client = new HttpClient();
        Client.BaseAddress = new Uri("https://localhost:7122/");
        Output = outputHelper;
    }

    public HttpClient Client { get; }
    public ITestOutputHelper Output { get; }

    [Fact]
    public async Task Get_OK()
    {
        var response = await Client.GetAsync("/brew-coffee");
        var responseTest = await response.Content.ReadAsStringAsync();
        Output.WriteLine(responseTest);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_503()
    {
        HttpResponseMessage response;
        int i = 0;
        do
        {
            response = await Client.GetAsync("/brew-coffee");
            i++;
        } while (i < 5); // every fifth call, the endpoint should return 503 

        Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
    }

    [Fact]
    public async Task Get_418()
    {
        var response = await Client.GetAsync("/brew-coffee");
        var responseTest = await response.Content.ReadAsStringAsync();
        Output.WriteLine(responseTest);

        string result = $"{DateTime.Now.Day}{DateTime.Now.Month}-{(int)response.StatusCode}";

        Assert.Equal("14-418", result);
    }
}
