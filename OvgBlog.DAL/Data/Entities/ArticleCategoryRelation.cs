using OvgBlog.DAL.Abstract;
using System;
using System.Collections.Generic;

#nullable disable

namespace OvgBlog.DAL.Data.Entities
{
    public partial class ArticleCategoryRelation:BaseEntity
    {
        

        public Guid CategoryId { get; set; }
        public Guid ArticleId { get; set; }

        public virtual Article Article { get; set; }
        public virtual Category Category { get; set; }
       
    }
}
