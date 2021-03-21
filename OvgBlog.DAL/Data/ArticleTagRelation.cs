using System;
using System.Collections.Generic;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public partial class ArticleTagRelation
    {
        public Guid Id { get; set; }
        public Guid TagId { get; set; }
        public Guid ArticleId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Article Article { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
