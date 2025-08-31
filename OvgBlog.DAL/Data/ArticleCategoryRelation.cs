using System;
using System.Collections.Generic;
using OvgBlog.DAL.Data.Base;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public class ArticleCategoryRelation : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public virtual Category Category { get; set; }
    }
}
