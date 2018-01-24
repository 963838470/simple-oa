using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oa_server.Models
{
    public class TreeData : AuthorityOu
    {
        public List<TreeData> children { get; set; }
    }
}