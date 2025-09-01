using System;
using System.Collections.Generic;
using OvgBlog.DAL.Data.Base;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public class ArticleTagRelation : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid TagId { get; set; }
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
