using System.Net.Http;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tests {
public class Testo
{
    private readonly TestServer _server;
    private readonly HttpClient _client;
    public Testo()
    {
        // Arrange
        _server = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>());
        _client = _server.CreateClient();
    }

    [Fact]
    public async void ReturnHelloWorld()
    {
        // Act
        var response = await _client.GetAsync("/");
        //response.StatusCode = 400;

        var responseString = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal("",
            responseString);
    }

    [Fact]
    public async void IncorrectPassword() 
    {
	var response = await _client.GetAsync("/users/login/phone/killme");
        Assert.False(response.IsSuccessStatusCode);


    }
    [Fact]
    public async void BadNewUser() 
    {
	var response = await _client.GetAsync("/users/login");
        Assert.False(response.IsSuccessStatusCode);
    }
    [Fact]
    public async void BadNewAct() 
    {
	var response = await _client.GetAsync("/activities/55/50/search");
        Assert.False(response.IsSuccessStatusCode);

    }
    [Fact]
    public async void BadUserActs() 
    {
	var response = await _client.GetAsync("/activities/55/getactivities");
        Assert.False(response.IsSuccessStatusCode);
    }
}
}
