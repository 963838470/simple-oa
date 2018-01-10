﻿using oa_server.DAL;
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
        public IHttpActionResult Get()
        {
            return Json(_AuthorityUserDal.Get());
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
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                AuthorityUser model = _AuthorityUserDal.Get().FirstOrDefault(o => o.id == id);
                _AuthorityUserDal.Delete(model);
                return Ok("删除成功");
            }
            catch (Exception ex)
            {
                return Ok("删除失败!" + ex.Message);
            }
        }
    }
}