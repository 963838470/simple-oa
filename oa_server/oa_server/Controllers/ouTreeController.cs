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
    /// 机构树
    /// </summary>
    public class ouTreeController : ApiController
    {
        private AuthorityOuDal _AuthorityOuDal = new AuthorityOuDal();

        /// <summary>
        /// 获取机构-树形结构
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
            foreach (AuthorityOu item in ous.FindAll(o => o.pid == pid).OrderBy(o => o.createTime))
            {
                TreeData tree = new TreeData();
                tree.id = item.id;
                tree.label = item.name;
                tree.address = item.address;
                tree.createTime = item.createTime;
                tree.description = item.description;
                tree.name = item.name;
                tree.path = item.path;
                tree.pid = item.pid;

                trees.Add(tree);

                List<AuthorityOu> childOu = ous.FindAll(n => n.pid == item.id);
                if (childOu.Count > 0)
                {
                    List<TreeData> childrenTree = new List<TreeData>();
                    BuildTree(childrenTree, ous, item.id);
                    tree.children = childrenTree;
                }
            }
            return trees;
        }
    }
}
