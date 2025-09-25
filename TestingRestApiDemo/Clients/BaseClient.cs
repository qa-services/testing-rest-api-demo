using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;

namespace TestingRestApiDemo.Clients;

public class BaseClient
{
    protected RestClient Restclient { get; set; }

    public BaseClient(string baseUrl)
    {
        Restclient = new RestClient(baseUrl); 
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        var client = new RestClient("https://api.example.com", configureSerialization: s => s.UseSystemTextJson(options));
    }

    protected void AddDefaultHeaders(RestRequest request)
    {
        request.AddHeader("Accept", "application/json");
    }
}
