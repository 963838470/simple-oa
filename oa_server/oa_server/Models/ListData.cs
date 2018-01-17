using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oa_server.Models
{
    public class ListData
    {
        /// <summary>
        /// 数据总条数
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public object data { get; set; }
    }
}