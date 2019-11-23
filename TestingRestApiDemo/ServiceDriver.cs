using Newtonsoft.Json;
using RestSharp;

namespace TestingRestApiDemo
{
    public class ServiceDriver
    {
        private string _uri;
        private RestClient _restclient;

        public ServiceDriver(string uri)
        {
            _uri = uri;
            _restclient = new RestClient(_uri);
        }

        public ResponseModel GetExchangeRate(string currency, string date)
        {
            var request = new RestRequest($"/api/exchangerates/rates/A/{currency}/{date}", Method.GET);
            var response = _restclient.Execute(request);

            return JsonConvert.DeserializeObject<ResponseModel>(response.Content);
        }
    }
}
