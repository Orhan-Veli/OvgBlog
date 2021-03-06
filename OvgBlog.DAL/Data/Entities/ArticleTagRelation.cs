using OvgBlog.DAL.Abstract;
using System;
using System.Collections.Generic;

#nullable disable

namespace OvgBlog.DAL.Data.Entities
{
    public partial class ArticleTagRelation:BaseEntity
    {
        public Guid TagId { get; set; }
        public Guid ArticleId { get; set; }

        public virtual Article Article { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
