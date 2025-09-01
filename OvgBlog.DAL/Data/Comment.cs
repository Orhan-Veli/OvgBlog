using System;
using System.Collections.Generic;
using OvgBlog.DAL.Data.Base;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public class Comment : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public Guid ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
