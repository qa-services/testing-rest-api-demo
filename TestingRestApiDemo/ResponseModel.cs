using System.Collections.Generic;

namespace TestingRestApiDemo
{
    public class ResponseModel
    {       
            public string Table { get; set; }
            public string Currency { get; set; }
            public string Code { get; set; }
            public IList<Rate> Rates { get; set; }

        public ResponseModel()
        {
            Rates = new List<Rate>();
        }
    }

    public class Rate
    {
        public string No { get; set; }
        public string EffectiveDate { get; set; }
        public double Mid { get; set; }
    }
}
