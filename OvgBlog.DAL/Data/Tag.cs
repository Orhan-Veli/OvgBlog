using System;
using System.Collections.Generic;
using OvgBlog.DAL.Data.Base;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public class Tag : BaseEntity
    {
        public Tag()
        {
            ArticleTagRelations = new HashSet<ArticleTagRelation>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SeoUrl { get; set; }

        public virtual ICollection<ArticleTagRelation> ArticleTagRelations { get; set; }
    }
}
