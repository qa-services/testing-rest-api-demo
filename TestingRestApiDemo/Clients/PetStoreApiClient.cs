using RestSharp;
using TestingRestApiDemo.Models;

namespace TestingRestApiDemo.Clients;

public class PetStoreApiClient : BaseClient
{
    public PetStoreApiClient(string baseUrl) : base(baseUrl)
    {
    }

    public RestResponse<PetResponse> SendCreatePetRequest(CreatePetRequest createPetRequest)
    {        
        var request = new RestRequest("/v2/pet", Method.Post);
        AddDefaultHeaders(request);
        request.AddJsonBody(createPetRequest);

        var response = Restclient.Execute<PetResponse>(request);
        return response;
    }
}
