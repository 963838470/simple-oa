using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oa_server.Models
{
    public class TreeData
    {

        public int id { get; set; }

        public string label { get; set; }

        public List<TreeData> children { get; set; }
    }
}