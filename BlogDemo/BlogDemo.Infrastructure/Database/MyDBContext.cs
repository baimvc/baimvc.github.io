using BlogDemo.Core.Entities;
using BlogDemo.Infrastructure.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Infrastructure.Database
{
    public class MyDBContext:DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BannerConfiguration());
        }

        public DbSet<Banner> Banner { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsClassify> NewsClassify { get; set; }
        public DbSet<NewsComment> NewsComment { get; set; }
    }
}
