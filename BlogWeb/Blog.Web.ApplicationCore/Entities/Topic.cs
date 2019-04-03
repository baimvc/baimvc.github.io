using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Web.ApplicationCore.Entities
{
    /// <summary>
    /// 主题
    /// </summary>
    public class Topic: BaseEntity
    {
        public int NodeId { get; set; }
        /// <summary>
        /// 主题节点
        /// </summary>
        public TopicNode Node { get; set; }
        public string UserId { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 置顶权重
        /// </summary>
        public int Top { get; set; }
        /// <summary>
        /// 主题类型
        /// </summary>
        public TopicType Type { get; set; }
        /// <summary>
        /// 浏览数
        /// </summary>
        public int ViewCount { get; set; }
        /// <summary>
        /// 回复数
        /// </summary>
        public int ReplyCount { get; set; }
        /// <summary>
        /// 最后回复的用户ID
        /// </summary>
        public string LastReplyUserId { get; set; }
        /// <summary>
        /// 最后回复的用户
        /// </summary>
        public User LastReplyUser { get; set; }
        public DateTime? LastReplyTime { get; set; }
        public DateTime? CreateOn { get; set; }
        /// <summary>
        /// 回复主题表
        /// </summary>
        public virtual List<TopicReply> Replys { get; set; }







    }
    public enum TopicType
    {
        Delete = 0,
        Normal = 1,
        Top = 2,
        Good = 3,
        Hot = 4
    }
}
