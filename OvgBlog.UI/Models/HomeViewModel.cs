using System.Collections.Generic;

namespace OvgBlog.UI.Models
{
    public class HomeViewModel
    {
        public List<ArticleListViewModel> Articles { get; set; } = [];
        public List<CategoryListViewModel> Categories { get; set; } = [];
    }
}
