using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OvgBlog.Business.Abstract;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryService categoryService,ILogger<CategoryController> logger, IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
            _logger = logger;
        }
        
        public async Task<IActionResult> Index()
        {
            var categoryResult = await _categoryService.GetAll();
            if (categoryResult==null || !categoryResult.Success || categoryResult.Data == null)
            {
                return RedirectToAction("Index","Home");
            }
            var map = categoryResult.Data.Adapt<List<CategoryListViewModel>>();
            map.Where(x => x.ImageUrl == null).ToList().ForEach(item => item.ImageUrl = "uploads/DefaultCategory.png");
            return View(map);
        }

        //  ovgblog.com/category/music
        [HttpGet("Category/{seoUrl}")]
        public async Task<IActionResult> Detail(string seoUrl)
        {
            ViewData["Title"] = "Kategori";
            if (string.IsNullOrEmpty(seoUrl))
            {
                return RedirectToAction("Index");
            }
            var categoryResult = await _categoryService.CategoryBySeoUrl(seoUrl);
            if (categoryResult == null || categoryResult.Data == null || !categoryResult.Success)
            {
              return  RedirectToAction("Index");
            }
            ViewData["Title"] = categoryResult.Data.Name;
            var articleResult =await _articleService.GetByCategoryId(categoryResult.Data.Id);
            if(articleResult == null ||articleResult.Data==null || !articleResult.Success)
            {
                return View(new List<ArticleListViewModel>());
            }
            var map = articleResult.Data.Adapt<List<ArticleListViewModel>>();
            return View(map);
        }
    }
}
