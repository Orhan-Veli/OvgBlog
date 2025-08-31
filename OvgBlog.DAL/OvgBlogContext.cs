using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using OvgBlog.DAL.Constant;
using OvgBlog.DAL.Data;

#nullable disable

namespace OvgBlog.DAL
{
    public partial class OvgBlogContext : DbContext
    {     

        public OvgBlogContext(DbContextOptions<OvgBlogContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; }
        
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleCategoryRelation> ArticleCategoryRelations { get; set; }
        public virtual DbSet<ArticleTagRelation> ArticleTagRelations { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate);

                entity.Property(e => e.DeletedDate);

                entity.Property(e => e.ImageUrl).HasMaxLength(500);

                entity.Property(e => e.SeoUrl)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedDate);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Article_User_Id_UserId");
            });

            modelBuilder.Entity<ArticleCategoryRelation>(entity =>
            {
                entity.ToTable("ArticleCategoryRelation");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate);

                entity.Property(e => e.DeletedDate);

                entity.Property(e => e.UpdatedDate);

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleCategoryRelations)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleCategoryRelation_Article_Id_ArticleId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ArticleCategoryRelations)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleCategoryRelation_Category_Id_CategoryId");
            });

            modelBuilder.Entity<ArticleTagRelation>(entity =>
            {
                entity.ToTable("ArticleTagRelation");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate);

                entity.Property(e => e.DeletedDate);

                entity.Property(e => e.UpdatedDate);

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleTagRelations)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleTagRelation_Article_Id_ArticleId");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.ArticleTagRelations)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleTagRelation_Tag_Id_TagId");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate);

                entity.Property(e => e.DeletedDate);

                entity.Property(e => e.ImageUrl).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SeoUrl)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.CreatedDate);

                entity.Property(e => e.DeletedDate);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate);

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Article_Id_ArticleId");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SendDate);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate);

                entity.Property(e => e.DeletedDate);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.SeoUrl)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UpdatedDate);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate);

                entity.Property(e => e.DeletedDate);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate);
            });
        }
        
        public void Seed(IConfiguration configuration)
        {
            if (Users.Any(u => u.Email == configuration[UserConstants.AdminEmail])) return;
            
            Users.Add(new User
            {
                Id = Guid.NewGuid(),
                Name = configuration[UserConstants.AdminUserName],
                Email = configuration[UserConstants.AdminEmail],
                Password = configuration[UserConstants.AdminPassword],
                IsDeleted = false,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            });
            SaveChanges();
        }

    }
}