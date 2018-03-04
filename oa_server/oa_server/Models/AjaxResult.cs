using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oa_server.Models
{
    public class AjaxResult
    {
        private bool _state = true;
        /// <summary>
        /// 返回状态
        /// </summary>
        public bool state
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// 返回信息
        /// </summary>
        public object msg { get; set; }
    }
}