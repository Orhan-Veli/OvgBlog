using OvgBlog.DAL.Abstract;
using System;
using System.Collections.Generic;

#nullable disable

namespace OvgBlog.DAL.Data.Entities
{
    public partial class Tag: BaseEntity
    {
        public Tag()
        {
            ArticleTagRelations = new HashSet<ArticleTagRelation>();
        }

        public string Name { get; set; }
        public string SeoUrl { get; set; }

        public virtual ICollection<ArticleTagRelation> ArticleTagRelations { get; set; }
    }
}
