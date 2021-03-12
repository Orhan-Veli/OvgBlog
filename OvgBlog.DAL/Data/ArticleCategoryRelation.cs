using OvgBlog.DAL.Abstract;
using System;
using System.Collections.Generic;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public partial class ArticleCategoryRelation : IEntity
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ArticleId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Article Article { get; set; }
        public virtual Category Category { get; set; }
    }
}
