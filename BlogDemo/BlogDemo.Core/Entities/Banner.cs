using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Entities
{
    public class Banner: EntityBase
    {
      
        public string Image { get; set; }
        public string Url { get; set; }
        public DateTime? AddTime { get; set; }
        public string Remark { get; set; }
      

    }
}
