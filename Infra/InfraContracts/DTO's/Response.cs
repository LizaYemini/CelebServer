using Newtonsoft.Json;

namespace InfraContracts
{
    public class Response
    {
        [JsonProperty(PropertyName = "responseType")]
        public string ResponseType { get; set; }
        public Response()
        {
            ResponseType = this.GetType().Name;
        }
    }
}