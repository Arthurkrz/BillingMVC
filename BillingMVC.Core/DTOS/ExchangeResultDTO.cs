﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BillingMVC.Core.DTOS
{
    public class ExchangeResultDTO
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; }
        
        [JsonPropertyName("rates")]
        public Dictionary<string, double> Rates { get; set; }
    }
}
