using Newtonsoft.Json;
using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoIFoodAuthentication : Entity
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("expiresIn")]
        public int ExpiresIn { get; set; }
        public DateTime DataRequest { get; set; }
        public DateTime DataExpires { get; set; }
    }
}