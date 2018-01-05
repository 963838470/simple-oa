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
    public class UserController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            return Json(products);
        }

        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            return Json(product);
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
