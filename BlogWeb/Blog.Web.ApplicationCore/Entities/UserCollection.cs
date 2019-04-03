using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Web.ApplicationCore.Entities
{
    /// <summary>
    /// 用户集合
    /// </summary>
    public class UserCollection:BaseEntity
    {
        public string UserId { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public int State { get; set; }
        public DateTime? CreateOn { get; set; }
    }
}
