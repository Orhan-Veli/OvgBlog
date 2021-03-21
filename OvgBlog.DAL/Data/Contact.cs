using OvgBlog.DAL.Abstract;
using System;
using System.Collections.Generic;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public partial class Contact:IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }

        public DateTime SendDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
