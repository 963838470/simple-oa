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
    /// 用户接口
    /// </summary>
    public class UserController : ApiController
    {
        private OAEntities db = new Models.OAEntities();

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            return Json(db.AuthorityUser.ToList());
        }

        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            var product = db.AuthorityUser.FirstOrDefault(o => o.Id == id);
            return Json(product);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post([FromBody]AuthorityUser user)
        {
            user = db.AuthorityUser.Add(user);
            db.SaveChanges();
            return Json(user);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Put([FromBody]AuthorityUser user)
        {
            user = db.AuthorityUser.Attach(user);
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json(user);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            AuthorityUser user = db.AuthorityUser.FirstOrDefault(o => o.Id == id);
            db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok();
        }
    }
}
