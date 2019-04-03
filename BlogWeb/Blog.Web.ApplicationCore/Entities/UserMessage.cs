using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Web.ApplicationCore.Entities
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class UserMessage:BaseEntity
    {
        public string SendUserId { get; set; }
        /// <summary>
        /// 发送者
        /// </summary>
        public User SendUser { get; set; }
        public string ReceiveUserId { get; set; }
        /// <summary>
        /// 接受者
        /// </summary>
        public User ReceiveUser { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public int State { get; set; }
        public DateTime CreateOn { get; set; }

    }
}
