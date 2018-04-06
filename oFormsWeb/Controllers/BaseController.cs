using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace oFormsWeb.Controllers
{
    public abstract class BaseController : Controller
    {
        protected virtual string GetUserObjectId()
        {
            return User.FindFirstValue("http://schemas.microsoft.com/identity/claims/objectidentifier");
        }
    }
}
