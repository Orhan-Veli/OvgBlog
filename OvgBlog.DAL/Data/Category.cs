using System;
using System.Collections.Generic;
using OvgBlog.DAL.Data.Base;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public class Category : BaseEntity
    {
        public Category()
        {
            ArticleCategoryRelations = new HashSet<ArticleCategoryRelation>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string SeoUrl { get; set; }

        public virtual ICollection<ArticleCategoryRelation> ArticleCategoryRelations { get; set; }
    }
}
