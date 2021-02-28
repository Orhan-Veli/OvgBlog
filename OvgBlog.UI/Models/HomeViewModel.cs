using System.Collections.Generic;

namespace OvgBlog.UI.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Articles = new List<ArticleListViewModel>();
            Categories = new List<CategoryListViewModel>();
        }
        public List<ArticleListViewModel> Articles { get; set; }
        public List<CategoryListViewModel> Categories { get; set; }
    }
}
