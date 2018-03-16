using oa_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace oa_server.Common
{
    public class SecurityHelper : ApiController
    {
        /// <summary>
        /// 获取登陆人员信息
        /// </summary>
        /// <returns></returns>
        public AuthorityUser GetUser()
        {
            string json = GetProp("AuthorityUser");
            return JsonHelper.JsonDeserialize<AuthorityUser>(json);
        }

        /// <summary>
        /// 获取登陆人员机构信息
        /// </summary>
        /// <returns></returns>
        public List<AuthorityOu> GetOU()
        {
            string json = GetProp("AuthorityOu");
            return JsonHelper.JsonDeserialize<List<AuthorityOu>>(json);
        }

        public string GetProp(string prop)
        {
            string result = string.Empty;
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            Claim claim = identity.Claims.Where(o => o.Type == prop).FirstOrDefault();
            if (claim != null)
                result = claim.Value;

            return result;
        }
    }
}