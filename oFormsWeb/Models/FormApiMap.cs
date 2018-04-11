using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace oFormsWeb.Models
{
    public class FormApiMap
    {
        [Required]
        [JsonProperty(PropertyName = "id")]
        public string ApiKey { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string FormId { get; set; }
        public FormTemplate FormTemplate { get; set; }
        public int NumberOfRequests { get; set; }
    }
}
