using System;
using System.Collections.Generic;
using OvgBlog.DAL.Data.Base;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
