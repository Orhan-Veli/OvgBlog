using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Models
{
    public class CategoryListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SeoUrl { get; set; }

        public string ImageUrl { get; set; }
    }
}
