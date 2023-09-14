using Blog_model.Models.Concrete;
using Blog_model.TypeConfigurations.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_model.TypeConfigurations.Concrete
{
    public class CommentMap : BaseMap<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(a => a.Text).IsRequired(true);

            builder.HasKey(a => new { a.AppUserID, a.ArticleID });

            builder.HasOne(a => a.AppUser).WithMany(a => a.Comments).HasForeignKey(a => a.AppUserID).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Article).WithMany(a => a.Comments).HasForeignKey(a => a.ArticleID).OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}

