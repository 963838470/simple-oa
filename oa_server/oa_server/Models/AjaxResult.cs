using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oa_server.Models
{
    public class AjaxResult
    {
        /// <summary>
        /// 返回状态
        /// </summary>
        public bool state { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }
    }
}