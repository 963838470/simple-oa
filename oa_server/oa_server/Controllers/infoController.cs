using Newtonsoft.Json.Linq;
using oa_server.Common;
using oa_server.DAL;
using oa_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace oa_server.Controllers
{
    /// <summary>
    /// 登陆
    /// </summary>
    [AllowAnonymous]
    public class infoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            SecurityHelper _SecurityHelper = new Common.SecurityHelper();
            var result = new
            {
                user = _SecurityHelper.GetUser(),
                ou = _SecurityHelper.GetOU()
            };

            return Json(result);
        }
    }
}
