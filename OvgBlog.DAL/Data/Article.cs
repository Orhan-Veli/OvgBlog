using System;
using System.Collections.Generic;
using OvgBlog.DAL.Data.Base;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public string SeoUrl { get; set; }
        public string ImageUrl { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ArticleCategoryRelation> ArticleCategoryRelations { get; set; }
        public virtual ICollection<ArticleTagRelation> ArticleTagRelations { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
