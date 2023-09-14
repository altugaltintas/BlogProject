using Blog_model.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_model.TypeConfigurations.Concrete
{
    public class LikeMap : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(a => new { a.AppUserID, a.ArticleID });

            builder.HasOne(a=> a.AppUser).WithMany(a=> a.Likes).HasForeignKey(a=>a.AppUserID).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Article).WithMany(a => a.Likes).HasForeignKey(a => a.ArticleID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
