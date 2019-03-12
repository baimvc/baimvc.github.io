using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Entities
{
    public class NewsClassify:EntityBase
    {
        public NewsClassify()
        {
            this.News = new HashSet<News>();
        }
        public ICollection<News> News { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
       
    }
}
