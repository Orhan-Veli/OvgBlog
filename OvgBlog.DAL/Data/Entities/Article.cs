using OvgBlog.DAL.Abstract;
using System;
using System.Collections.Generic;

#nullable disable

namespace OvgBlog.DAL.Data.Entities
{
    public partial class Article:BaseEntity
    {
        public Article()
        {
            ArticleCategoryRelations = new HashSet<ArticleCategoryRelation>();
            Comments = new HashSet<Comment>();
        }

       
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public string SeoUrl { get; set; }
        public string ImageUrl { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ArticleCategoryRelation> ArticleCategoryRelations { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
