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
    /// 机构
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
            return Json(_AuthorityOuDal.Get());
        }

        /// <summary>
        /// 获取指定机构
        /// </summary>
        /// <param name="id">机构ID</param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            var product = _AuthorityOuDal.Get().FirstOrDefault(o => o.id == id);
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
            model.createTime = DateTime.Now;
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
        /// <param name="ids">机构ID,多个使用,分割</param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(string ids)
        {
            var ous = _AuthorityOuDal.Get();
            int id = 0;
            string[] arrayId = ids.Split(',');
            foreach (string item in arrayId)
            {
                if (int.TryParse(item, out id))
                {
                    // 将直接子机构关系删除
                    foreach (AuthorityOu ou in ous.Where(o => o.pid == id).ToList())
                    {
                        ou.pid = 0;
                        ou.path = ou.path.Replace("," + id + ",", "").Replace("," + id, "").Replace(id + ",", "");
                        _AuthorityOuDal.Update(ou);
                    }
                    // 逻辑删除该机构
                    AuthorityOu model = ous.FirstOrDefault(o => o.id == id);
                    model.isDelete = true;
                    _AuthorityOuDal.Update(model);
                }
            }
            return Ok("删除成功！");
        }
    }
}
