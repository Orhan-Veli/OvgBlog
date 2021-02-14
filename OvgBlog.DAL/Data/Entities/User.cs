using OvgBlog.DAL.Abstract;
using System;
using System.Collections.Generic;

#nullable disable

namespace OvgBlog.DAL.Data.Entities
{
    public partial class User: BaseEntity
    {
        public User()
        {
            Articles = new HashSet<Article>();
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
