using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
