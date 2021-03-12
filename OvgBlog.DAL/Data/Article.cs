﻿using OvgBlog.DAL.Abstract;
using System;
using System.Collections.Generic;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public partial class Article:IEntity
    {
        public Article()
        {
            ArticleCategoryRelations = new HashSet<ArticleCategoryRelation>();
            ArticleTagRelations = new HashSet<ArticleTagRelation>();
            Comments = new HashSet<Comment>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public string SeoUrl { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ArticleCategoryRelation> ArticleCategoryRelations { get; set; }
        public virtual ICollection<ArticleTagRelation> ArticleTagRelations { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
