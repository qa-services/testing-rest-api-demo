using System.Collections.Generic;

namespace TestingRestApiDemo
{
    public class DataRepository
    {
        public static ResponseModel GetExpectedResponse(string currency, string date)
        {
            return currency switch
            {
                "GBP" => new ResponseModel
                {
                    Table = "A",
                    Currency = "funt szterling",
                    Code = currency,
                    Rates = new List<Rate> { new Rate()
                        {
                            No = "225/A/NBP/2019",
                            EffectiveDate = date,
                            Mid = 5.0144
                        }
                    }
                },
                "USD" => new ResponseModel
                {
                    Table = "A",
                    Currency = "dolar amerykański",
                    Code = currency,
                    Rates = new List<Rate> { new Rate()
                        {
                            No = "069/A/NBP/2019",
                            EffectiveDate = date,
                            Mid = 3.8188
                        }
                    }
                },
                _ => null,
            };
        }
    }
}
