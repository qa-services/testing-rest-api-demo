using FluentAssertions;
using RestSharp;
using System.Net;
using TestingRestApiDemo.Clients;
using TestingRestApiDemo.Models;
using Xunit;

namespace TestingRestApiDemo.Tests;

public class PetStoreTests
{
    [Fact]
    public void TestAddingPet_Basic()
    {
        string url = "https://petstore.swagger.io";

        var restclient = new RestClient(url);
        var request = new RestRequest("/v2/pet", Method.Post);
        request.AddHeader("Accept", "application/json");
        request.AddJsonBody("{\"category\":{\"id\":0,\"name\":\"MeetupAnimal\"},\"name\":\"KamilM\",\"photoUrls\":[\"http://myphotos.pl/1\"],\"tags\":[{\"id\":0,\"name\":\"EQTEK\"}],\"status\":\"available\"}");

        var response = restclient.Execute(request);
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
    }

    [Fact]
    public void TestAddingPet()
    {
        var restclient = new PetStoreApiClient(Settings.PetStoreApiBaseUrl);
        var request = CreatePetRequest.Random();

        var response = restclient.SendCreatePetRequest(request);
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);

        response.Data.Should().BeEquivalentTo(request, options => options
            .ExcludingMissingMembers());

        response.Data.Id.Should().BeGreaterThan(0);
    }
}
