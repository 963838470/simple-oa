using oa_server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace oa_server.DAL
{
    public class AuthorityOuDal : BaseDal<AuthorityOu>
    {
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <returns></returns>
        public List<AuthorityOu> Get()
        {
            DbContext db = GetDbContext();

            return db.Set<AuthorityOu>().Where(o => o.isDelete == false).ToList();
        }
    }
}