using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Models
{
    public class AdminListViewModel
    {
        public int CategoryCount { get; set; }
        public int ArticleCount { get; set; }

        public int TagCount { get; set; }
        public int CommentCount { get; set; }
    }
}
