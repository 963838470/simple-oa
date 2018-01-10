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
            List<AuthorityOu> datas = _AuthorityOuDal.Get().ToList();
            List<TreeData> trees = new List<TreeData>();
            trees = BuildTree(trees, datas, 0);

            return Json(trees);
        }

        private static List<TreeData> BuildTree(List<TreeData> trees, List<AuthorityOu> ous, int pid)
        {
            foreach (AuthorityOu item in ous.FindAll(o => o.pid == pid).OrderBy(o => o.name))
            {
                TreeData tree = new TreeData();
                tree.id = item.id;
                tree.label = string.IsNullOrWhiteSpace(item.name) ? "" : item.name;
                trees.Add(tree);

                List<AuthorityOu> childOu = ous.FindAll(n => n.pid == item.id);
                List<TreeData> childrenTree = new List<TreeData>();
                if (childOu.Count > 0)
                {
                    BuildTree(childrenTree, ous, item.id);
                }
                tree.children = childrenTree;
            }
            return trees;
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
                AuthorityOu model = _AuthorityOuDal.Get().FirstOrDefault(o => o.id == id);
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
