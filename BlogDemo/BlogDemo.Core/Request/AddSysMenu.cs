using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Request
{
    public class AddSysMenu
    {
        public int? ParentID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int Sort { get; set; }
        public string Picname { get; set; }
        public int? Level { get; set; }
    }
}
