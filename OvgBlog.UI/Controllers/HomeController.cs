using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OvgBlog.Business.Abstract;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IArticleService articleService, ICategoryService categoryService)
        {
            _logger = logger;
            _articleService = articleService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel();

            var articleList = new List<ArticleListViewModel>();
            var response = await _articleService.GetAll();
            if (response.Success)
            {
                articleList = response.Data.Adapt<List<ArticleListViewModel>>();
                articleList.ForEach(item => item.Body = item.Body?.Length > 100
                                    ? (item.Body?.Substring(0, 100)?.ToString() ?? "") + " ..."
                                    : item.Body);
            }
            model.Articles = articleList.OrderByDescending(x=> x.CreatedDate).Take(10).ToList();
            var categoryList = new List<CategoryListViewModel>();
            var categoryResult = await _categoryService.GetAll();
            if (categoryResult.Success && categoryResult.Data != null)
            {
                categoryList = categoryResult.Data.Adapt<List<CategoryListViewModel>>();
                categoryList.Where(x => x.ImageUrl == null).ToList().ForEach(item => item.ImageUrl = "uploads/DefaultCategory.png");
            }
            model.Categories = categoryList;

            return View(model);
        }

        public IActionResult Categories()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var model = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(model);
        }
        [HttpGet]
        public IActionResult AboutMe()
        {

            return View();
        }
    }
}
