using Newtonsoft.Json;

namespace Rainfall.Api.Models.Response
{
    public class RainfallPublicReadingItem
    {
        [JsonProperty(PropertyName = "@id")]
        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Measure { get; set; }
        public decimal Value { get; set; }
    }
}
