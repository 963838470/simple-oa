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
    public class loginController : ApiController
    {
        [HttpGet]
        public void Get(string loginName, string password)
        {
            AuthorityUserDal _AuthorityUserDal = new AuthorityUserDal();
            bool isUserExist = _AuthorityUserDal.IsUserExist(loginName.Trim(), password.Trim());
        }
    }
}
