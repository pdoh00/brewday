using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace brewday.resource.endpoint.Controllers
{
    //[RoutePrefix("api/temp")]
    public class TempController : ApiController
    {
        public string Get()
        {
            return "SOME GET DATA";
        }
    }
}
