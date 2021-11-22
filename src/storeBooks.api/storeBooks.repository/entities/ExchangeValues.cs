using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace storeBooks.repository.entities
{
    public class ExchangeValues
    {
        [JsonProperty("success")]
        public bool Status { get; set; }
        [JsonProperty("timestamp")]
        public long TimeStamp { get; set; }
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("date")]
        public DateTime DateExchange { get; set; }
        [JsonProperty("rates")]
        public Rates Rates { get; set; }
    }
}
