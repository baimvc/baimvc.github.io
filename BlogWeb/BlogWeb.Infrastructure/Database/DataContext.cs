using Blog.Web.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogWeb.Infrastructure.Database
{
    public class DataContext:IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {

        }
        /// <summary>
        /// 主题表
        /// </summary>
        public DbSet<Topic> Topics { get; set; }
        /// <summary>
        /// 主题回复表
        /// </summary>
        public DbSet<TopicReply> TopicReplys { get; set; }
        /// <summary>
        /// 主题节点表
        /// </summary>
        public DbSet<TopicNode> TopicNodes { get; set; }
        /// <summary>
        /// 用户消息表
        /// </summary>
        public DbSet<UserMessage> UserMessages { get; set; }
        /// <summary>
        /// 主题用户表
        /// </summary>
        public DbSet<UserCollection> UserTopics { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Topic>().ToTable("Topic");
            builder.Entity<TopicReply>().ToTable("TopicReply");
            builder.Entity<TopicNode>().ToTable("TopicNode");
            builder.Entity<User>().ToTable("User");
            builder.Entity<UserMessage>().ToTable("UserMessage");
            builder.Entity<UserCollection>().ToTable("UserCollection");
        }

    }
}
