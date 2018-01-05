using oa_server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace oa_server.DAL
{
    public class BaseDal<T> where T : class
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DbSet<T> Get()
        {
            OAEntities db = new OAEntities();
            return db.Set<T>();
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public T Add(T model)
        {
            using (OAEntities db = new OAEntities())
            {
                T newModel = db.Set<T>().Add(model);
                db.SaveChanges();
                return newModel;
            }
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public T Update(T model)
        {
            using (OAEntities db = new OAEntities())
            {
                model = db.Set<T>().Attach(model);
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

                return model;
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public int Delete(T model)
        {
            using (OAEntities db = new OAEntities())
            {
                db.Entry(model).State = EntityState.Deleted;
                return db.SaveChanges();
            }
        }
    }
}