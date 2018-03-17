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
    public class loginController : ApiController
    {
        public class User
        {
            public string loginName { get; set; }
            public string password { get; set; }
        }

        [HttpPost]
        public IHttpActionResult Get([FromBody]User model)
        {
            AjaxResult result = new AjaxResult();
            AuthorityUserDal _AuthorityUserDal = new AuthorityUserDal();
            AuthorityUser user = _AuthorityUserDal.GetUser(model.loginName.Trim(), model.password.Trim());
            if (user == null)
            {
                result.state = false;
            }
            result.msg = user;

            return Json(result);
        }
    }
}
