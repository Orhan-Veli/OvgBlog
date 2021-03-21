using System;
using System.Collections.Generic;

#nullable disable

namespace OvgBlog.DAL.Data
{
    public partial class Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public Guid Id { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
