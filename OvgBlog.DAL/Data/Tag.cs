using System;
using System.Collections.Generic;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public partial class Tag
    {
        public Tag()
        {
            ArticleTagRelations = new HashSet<ArticleTagRelation>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SeoUrl { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<ArticleTagRelation> ArticleTagRelations { get; set; }
    }
}
