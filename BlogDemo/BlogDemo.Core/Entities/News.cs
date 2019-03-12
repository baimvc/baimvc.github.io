using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Entities
{
    public class News: EntityBase
    {
        public News()
        {
            this.NewsComment = new HashSet<NewsComment>();
        }
      
        public NewsClassify NewsClassify { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Contents { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Remark { get; set; }
        public ICollection<NewsComment> NewsComment { get; set; }


    }
}
