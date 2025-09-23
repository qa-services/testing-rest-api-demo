using Newtonsoft.Json;
using RestSharp;

namespace TestingRestApiDemo
{
    public class ApiClient
    {
        private string _uri;
        private RestClient _restclient;

        public ApiClient(string uri)
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
