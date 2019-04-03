using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Web.ApplicationCore.Entities
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Profile { get; set; }
        public string Url { get; set; }
        public string GitHub { get; set; }
        /// <summary>
        /// 主题数量
        /// </summary>
        public int TopicCount{ get; set; }
        /// <summary>
        /// 主题回复
        /// </summary>
        public int TopicReplyCount { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        public int Score { get; set; }
        public DateTime? CreateOn { get; set; }
        public DateTime? LastTime { get; set; }

    }
}
