using System;
using System.Collections.Generic;

namespace OvgBlog.UI.Models
{
    public class ArticleListViewModel
    {
       
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SeoUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Body { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
