using oa_server.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace oa_server.DAL
{
    public class AuthorityUserDal : BaseDal<AuthorityUser>
    {
        public List<AuthorityUser> GetUser(string name, int ouId)
        {
            DbContext db = GetDbContext();
            string sql = @" SELECT u.id, u.name, u.loginName, u.password, u.email, u.phone, u.isDisable, u.address, u.remark, u.picture, u.isDelete, u.createTime
                                FROM AuthorityUser u
                            LEFT JOIN AuthorityOuUser ou ON ou.userId=u.id
                            WHERE ou.ouId=@ouId and name like @name
                            ORDER BY u.name";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("ouId", ouId),
                new SqlParameter("name", "%" + name + "%")
            };

            return db.Database.SqlQuery<AuthorityUser>(sql, param).ToList();
        }
    }
}