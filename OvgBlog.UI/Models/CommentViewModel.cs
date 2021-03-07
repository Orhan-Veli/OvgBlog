using System;

namespace OvgBlog.UI.Models
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
    }
}
