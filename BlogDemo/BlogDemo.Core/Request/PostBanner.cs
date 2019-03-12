using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Request
{
    public class PostBanner
    {
        public string Image { get; set; }
        public string Url { get; set; }
        public DateTime? AddTime { get; set; }
        public string Remark { get; set; }
    }
}
