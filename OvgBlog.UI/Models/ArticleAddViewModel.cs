using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Models
{
    public class ArticleAddViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string SeoUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}
