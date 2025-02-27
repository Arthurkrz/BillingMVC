using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BillingMVC.Core.DTOS
{
    public class ExchangeResult
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; }
        
        [JsonPropertyName("rates")]
        public Dictionary<string, double> Rates { get; set; }
    }

    public class Rate
    {
        public double BRL { get; set; }
        public double USD { get; set; }
        public double EUR { get; set; }
    }
}
