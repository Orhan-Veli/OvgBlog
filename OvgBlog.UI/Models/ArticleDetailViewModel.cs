using System;
using System.Collections.Generic;

namespace OvgBlog.UI.Models
{
    public class ArticleDetailViewModel
    {
        public ArticleDetailViewModel()
        {
            Comments = new List<CommentViewModel>();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string SeoUrl { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
