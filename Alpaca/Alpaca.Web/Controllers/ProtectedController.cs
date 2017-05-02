using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Alpaca.Web.Controllers {

    [Route("api/protected")]
    public class ProtectedController : Controller {

        // GET api/protected
        [Authorize]
        public string Get() {
            return "Protected Data!";
        }

    }
}
