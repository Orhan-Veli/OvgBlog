using System;
using System.Collections.Generic;
using OvgBlog.DAL.Data.Base;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public Guid Id { get; set; }
        public DateTime SendDate { get; set; }
    }
}
