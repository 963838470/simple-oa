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
    /// 用户接口
    /// </summary>
    public class userController : ApiController
    {
        private AuthorityUserDal _AuthorityUserDal = new AuthorityUserDal();

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get(string name = "", int pageSize = 10, int pageIndex = 1, int ouId = 0)
        {
            ListData data = new ListData();
            List<AuthorityUser> models = _AuthorityUserDal.GetUser(name, ouId);
            data.data = models.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            data.count = models.Count();

            return Json(data);
        }

        /// <summary>
        /// 获取指定用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            var product = _AuthorityUserDal.Get().FirstOrDefault(o => o.id == id);
            return Json(product);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post([FromBody]AuthorityUser model)
        {
            model.createTime = DateTime.Now;
            return Json(_AuthorityUserDal.Add(model));
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Put([FromBody]AuthorityUser model)
        {
            return Json(_AuthorityUserDal.Update(model));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">用户ID,多个使用,分割</param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(string ids)
        {
            try
            {
                int id = 0;
                string[] arrayId = ids.Split(',');
                foreach (string item in arrayId)
                {
                    if (int.TryParse(item, out id))
                    {
                        AuthorityUser model = _AuthorityUserDal.Get().FirstOrDefault(o => o.id == id);
                        _AuthorityUserDal.Delete(model);
                    }
                }
                return Ok("删除成功");
            }
            catch (Exception ex)
            {
                return Ok("删除失败!" + ex.Message);
            }
        }
    }
}
