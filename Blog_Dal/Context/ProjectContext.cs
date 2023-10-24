using Blog_model.Models.Concrete;
using Blog_model.TypeConfigurations.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_Dal.Context
{
    public class ProjectContext : IdentityDbContext<AppUser>

    {

        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserFollowedCategory> FollowedCategories { get; set; }
        public DbSet<OldPasswordHistory> oldPasswordHistories { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserMap());
            modelBuilder.ApplyConfiguration(new ArticleMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new IdentityRoleMap());
            modelBuilder.ApplyConfiguration(new LikeMap());
            modelBuilder.ApplyConfiguration(new UserFollowedCategoryMap());
            modelBuilder.ApplyConfiguration(new ArticleCategoryMap());
            base.OnModelCreating(modelBuilder);

        }
    }
}
