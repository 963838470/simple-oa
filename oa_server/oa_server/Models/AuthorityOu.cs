//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace oa_server.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuthorityOu
    {
      
        public int id { get; set; }
      
        public int pid { get; set; }
      
        public string name { get; set; }
      
        public string address { get; set; }
      
        public string description { get; set; }
      
        public string path { get; set; }
      
        public System.DateTime createTime { get; set; }
      
        public bool isDelete { get; set; }
    }
}
