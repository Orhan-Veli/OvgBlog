using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Models
{
    public class ContactListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public string Email { get; set; }

        public DateTime SendDate { get; set; }
    }
}
