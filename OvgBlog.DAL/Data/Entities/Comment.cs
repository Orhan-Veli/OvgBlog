using OvgBlog.DAL.Abstract;
using System;
using System.Collections.Generic;

#nullable disable

namespace OvgBlog.DAL.Data.Entities
{
    public partial class Comment: BaseEntity
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public Guid ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
