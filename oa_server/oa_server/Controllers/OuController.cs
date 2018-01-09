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
    /// 机构接口
    /// </summary>
    public class ouController : ApiController
    {
        private AuthorityOuDal _AuthorityOuDal = new AuthorityOuDal();

        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            return Json(_AuthorityOuDal.Get().ToList());
        }

        /// <summary>
        /// 获取指定机构
        /// </summary>
        /// <param name="id">机构ID</param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            var product = _AuthorityOuDal.Get().FirstOrDefault(o => o.Id == id);
            return Json(product);
        }

        /// <summary>
        /// 新增机构
        /// </summary>
        /// <param name="model">机构实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post([FromBody]AuthorityOu model)
        {
            return Json(_AuthorityOuDal.Add(model));
        }

        /// <summary>
        /// 修改机构
        /// </summary>
        /// <param name="model">机构实体</param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Put([FromBody]AuthorityOu model)
        {
            return Json(_AuthorityOuDal.Update(model));
        }

        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="id">机构ID</param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                AuthorityOu model = _AuthorityOuDal.Get().FirstOrDefault(o => o.Id == id);
                _AuthorityOuDal.Delete(model);
                return Ok("删除成功");
            }
            catch (Exception ex)
            {
                return Ok("删除失败!" + ex.Message);
            }
        }
    }
}
