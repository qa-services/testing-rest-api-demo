using RestSharp;
using TestingRestApiDemo.Models;

namespace TestingRestApiDemo.Clients
{
    public class NbpApiClient
    {
        private string _uri;
        private RestClient _restclient;

        public NbpApiClient(string uri)
        {
            _uri = uri;
            _restclient = new RestClient(_uri);
        }

        public ResponseModel GetExchangeRate(string currency, string date)
        {
            var request = new RestRequest($"/api/exchangerates/rates/A/{currency}/{date}", Method.Get);
            var response = _restclient.Execute<ResponseModel>(request);

            return response.Data;
        }
    }
}
