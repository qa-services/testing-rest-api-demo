using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Xunit;

namespace TestingRestApiDemo
{
    public class Tests
    {
        private ServiceDriver _serviceDriver;

        public Tests()
        {
            _serviceDriver = new ServiceDriver(Settings.NbpEndpoint);
        }

        [Fact]
        public void TestGet1_StatusCode()
        {
            string url = "http://api.nbp.pl";

            var restclient = new RestClient(url);
            var request = new RestRequest("/api/exchangerates/rates/A/GBP/2019-11-21", Method.GET);
            var response = restclient.Execute(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void TestGet2_ResponseData()
        {
            string url = "http://api.nbp.pl";

            var restclient = new RestClient(url);
            var request = new RestRequest("/api/exchangerates/rates/A/GBP/2019-11-21", Method.GET);
            var response = restclient.Execute(request);

            Assert.Contains("funt szterling", response.Content);
        }

        [Fact]
        public void TestGet3_FullResponseData()
        {
            string url = "http://api.nbp.pl";
            const string RefFile = "C:\\temp\\GBP_2019-11-21.json";

            var restclient = new RestClient(url);
            var request = new RestRequest("/api/exchangerates/rates/A/GBP/2019-11-21", Method.GET);
            var response = restclient.Execute(request);

            if (!File.Exists(RefFile))
                File.WriteAllText(RefFile, response.Content);

            Assert.Equal(File.ReadAllText(RefFile), response.Content);
        }

        [Theory]
        [InlineData("GBP", "2019-10-22")]
        [InlineData("USD", "2019-04-08")]
        public void TestGet4_FullResponseData(string currency, string date)
        {
            string url = "http://api.nbp.pl";
            string RefFile = $"C:\\temp\\{currency}_{date}.json";

            var restclient = new RestClient(url);
            var request = new RestRequest($"/api/exchangerates/rates/A/{currency}/{date}", Method.GET);
            var response = restclient.Execute(request);

            if (!File.Exists(RefFile))
                File.WriteAllText(RefFile, response.Content);

            Assert.Equal(File.ReadAllText(RefFile), response.Content);
        }

        [Fact]
        public void TestGet5_ResponseDataAsObject()
        {
            string url = "http://api.nbp.pl";

            var restclient = new RestClient(url);
            var request = new RestRequest("/api/exchangerates/rates/A/GBP/2019-11-21", Method.GET);
            var response = restclient.Execute(request);
            var responseObject = JsonConvert.DeserializeObject<ResponseModel>(response.Content);

            Assert.Equal("funt szterling", responseObject.Currency);
        }

        [Fact]
        public void TestGet6_ResponseDataAsObject_FluentAssertions()
        {
            string url = "http://api.nbp.pl";

            var restclient = new RestClient(url);
            var request = new RestRequest("/api/exchangerates/rates/A/GBP/2019-11-21", Method.GET);
            var response = restclient.Execute(request);
            var responseObject = JsonConvert.DeserializeObject<ResponseModel>(response.Content);

            responseObject.Currency
                .Should()
                .Be("funt szterling");
        }

        [Fact]
        public void TestGet6_FullResponseDataAsObject_FluentAssertions()
        {
            string url = "http://api.nbp.pl";

            var expectedResponse = new ResponseModel
            {
                Table = "A",
                Currency = "funt szterling",
                Code = "GBP",
                Rates = new List<Rate> { new Rate()
                    {
                        No = "225/A/NBP/2019",
                        EffectiveDate = "2019-11-21",
                        Mid = 5.0144
                    }
                }
            };    

            var restclient = new RestClient(url);
            var request = new RestRequest("/api/exchangerates/rates/A/GBP/2019-11-21", Method.GET);
            var response = restclient.Execute(request);
            var responseObject = JsonConvert.DeserializeObject<ResponseModel>(response.Content);

            expectedResponse
                .Should()
                .BeEquivalentTo(responseObject);
        }

        [Theory]
        [InlineData("GBP", "2019-11-21")]
        [InlineData("USD", "2019-04-08")]
        public void TestGet7_Refactor(string currency, string date)
        {
            _serviceDriver
                    .GetExchangeRate(currency, date)
                    .Should()
                    .BeEquivalentTo(DataRepository.GetExpectedResponse(currency, date));
        }

        [Fact]
        public void TestPost1_WithHeaderAndBody()
        {
            string url = "https://petstore.swagger.io";

            var restclient = new RestClient(url);
            var request = new RestRequest("/v2/pet", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody("{\"category\":{\"id\":0,\"name\":\"MeetupAnimal\"},\"name\":\"KamilM\",\"photoUrls\":[\"http://myphotos.pl/1\"],\"tags\":[{\"id\":0,\"name\":\"EQTEK\"}],\"status\":\"available\"}");

            var response = restclient.Execute(request);
            response.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);
        }
    }
}
