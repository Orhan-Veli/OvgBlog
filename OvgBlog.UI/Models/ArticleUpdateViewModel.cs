using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Models
{
    public class ArticleUpdateViewModel
    {
        public ArticleUpdateViewModel()
        {
            CategoryList = new List<CategoryListViewModel>();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string SeoUrl { get; set; }
        public string ImageUrl { get; set; }

        public List<CategoryListViewModel> CategoryList { get; set; }

        public Guid SelectedCategoryId { get; set; }
    }
}
