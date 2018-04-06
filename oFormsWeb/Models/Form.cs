using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oFormsWeb.Models
{
    public class Form
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string ApiKey { get; set; }
        public string Cors { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public FormTemplate FormTemplate { get; set; }
    }
}
