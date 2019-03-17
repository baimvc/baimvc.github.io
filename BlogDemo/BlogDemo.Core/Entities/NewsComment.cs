using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Entities
{
    public class NewsComment:EntityBase
    {
        public int NewsId { get; set; }
        public string Contents { get; set; }
        public DateTime? AddTime { get; set; }
        public string Remark { get; set; }
        public News News { get; set; }

    }
}
