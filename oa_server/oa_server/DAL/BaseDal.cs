using oa_server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace oa_server.DAL
{
    public class BaseDal<T> where T : class
    {
        /// <summary>  
        /// 获取线程内唯一的dbContext对象  
        /// </summary>  
        /// <returns></returns>  
        public DbContext GetDbContext()
        {
            // 首先先线程上下文中查看是否有已存在的DBContext  
            // 如果有那么直接返回这个，如果没有就新建   
            DbContext DB = CallContext.GetData("DBContext") as OAEntities;
            if (DB == null)
            {
                DB = new OAEntities();
                CallContext.SetData("DBContext", DB);
            }

            return DB;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <returns></returns>
        public DbSet<T> Get()
        {
            DbContext db = GetDbContext();

            return db.Set<T>();
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public T Add(T model)
        {
            DbContext db = GetDbContext();
            T newModel = db.Set<T>().Add(model);
            db.SaveChanges();

            return newModel;
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public T Update(T model)
        {
            DbContext db = GetDbContext();
            model = db.Set<T>().Attach(model);
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();

            return model;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public int Delete(T model)
        {
            DbContext db = GetDbContext();
            db.Entry(model).State = EntityState.Deleted;

            return db.SaveChanges();
        }
    }
}