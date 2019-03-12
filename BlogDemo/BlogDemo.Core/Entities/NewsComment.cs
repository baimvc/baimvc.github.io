using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Entities
{
    public class NewsComment:EntityBase
    {
        public News News { get; set; }
        public string Contents { get; set; }
        public DateTime? AddTime { get; set; }
        public string Remark { get; set; }
      
    }
}
