using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oFormsWeb.Models
{
    public class User
    {
        public string ClientId { get; set; }
        public string Email { get; set; }
        public List<Form> Forms;
    }
}
