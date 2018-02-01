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
    /// 机构人员关系
    /// </summary>
    public class ouUserController : ApiController
    {
        private AuthorityOuUserDal _AuthorityOuUserDal = new AuthorityOuUserDal();

        /// <summary>
        /// 获取人员-机构关系
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            List<AuthorityOuUser> datas = _AuthorityOuUserDal.Get().ToList();
            return Json(datas);
        }

        /// <summary>
        /// 新增人员-机构关系
        /// </summary>
        /// <param name="model">机构实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post([FromBody]AuthorityOuUser model)
        {
            var ous = _AuthorityOuUserDal.Get().Where(o => o.userId == model.userId).ToList();
            foreach (var ou in ous)
            {
                _AuthorityOuUserDal.Delete(ou);
            }

            return Json(_AuthorityOuUserDal.Add(model));
        }

        /// <summary>
        /// 删除人员-机构关系
        /// </summary>
        /// <param name="userId">删除用户Id</param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int userId)
        {
            var ous = _AuthorityOuUserDal.Get().Where(o => o.userId == userId).ToList();
            foreach (var ou in ous)
            {
                _AuthorityOuUserDal.Delete(ou);
            }

            return Ok("删除成功！");
        }
    }
}
