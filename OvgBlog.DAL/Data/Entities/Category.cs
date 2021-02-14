using OvgBlog.DAL.Abstract;
using System;
using System.Collections.Generic;

#nullable disable

namespace OvgBlog.DAL.Data.Entities
{
    public partial class Category: BaseEntity
    {
        public Category()
        {
            ArticleCategoryRelations = new HashSet<ArticleCategoryRelation>();
        }


        public string Name { get; set; }
        public string SeoUrl { get; set; }

        public virtual ICollection<ArticleCategoryRelation> ArticleCategoryRelations { get; set; }
    }
}
