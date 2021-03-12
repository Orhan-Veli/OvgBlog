using OvgBlog.DAL.Abstract;
using System;
using System.Collections.Generic;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public partial class Category : IEntity
    {
        public Category()
        {
            ArticleCategoryRelations = new HashSet<ArticleCategoryRelation>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string SeoUrl { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<ArticleCategoryRelation> ArticleCategoryRelations { get; set; }
    }
}
