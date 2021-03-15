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

        public string DisplayImage { get; set; } = "https://i.pinimg.com/originals/47/cc/85/47cc8554d4f385375555d0467a2c7ce7.jpg";
    }
}
