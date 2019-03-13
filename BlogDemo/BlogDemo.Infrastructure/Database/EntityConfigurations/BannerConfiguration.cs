using BlogDemo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Infrastructure.Database.EntityConfigurations
{
    public class BannerConfiguration : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            builder.Property(x => x.Image).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Url).IsRequired().HasMaxLength(200);
        

        }
    }
}
