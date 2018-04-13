using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace oFormsWeb.Models
{
    public class FormApiMap
    {
        [Required]
        [JsonProperty(PropertyName = "id")]
        public string ApiKey { get; set; }
        //[Required]
        //public string ClientId { get; set; }
        //[Required]
        //public string FormId { get; set; }
        public FormTemplate EmailInfo { get; set; }
    }
}
