using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace oFormsWeb.Controllers.WebAPI
{
    [Produces("application/json")]
    [Route("api/forms")]
    public class FormsController : Controller
    {
        // GET: api/Form
        [HttpGet]
        public IEnumerable<string> Get()
        {
            // Get all User's forms
            return new string[] { "value1", "value2" };
        }

        // GET: api/Form/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Form
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Form/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
