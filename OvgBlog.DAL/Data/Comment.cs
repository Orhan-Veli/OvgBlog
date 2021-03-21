using System;
using System.Collections.Generic;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public partial class Comment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public Guid ArticleId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Article Article { get; set; }
    }
}
