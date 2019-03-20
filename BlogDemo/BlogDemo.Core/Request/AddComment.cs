using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Request
{
    public class AddComment
    {
        public int NewsId { get; set; }
        public string Contents { get; set; }
        public string Remark { get; set; }
    }
}
