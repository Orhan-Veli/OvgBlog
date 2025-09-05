using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OvgBlog.Business.Abstract;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OvgBlog.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger, IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var categoryResult = await _categoryService.GetAllAsync(cancellationToken);
            if (categoryResult == null || !categoryResult.IsSuccess || categoryResult.Data == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var map = categoryResult.Data.Adapt<List<CategoryListViewModel>>();
            map.Where(x => x.ImageUrl == null).ToList().ForEach(item => item.ImageUrl = "uploads/DefaultCategory.png");
            return View(map);
        }

        [HttpGet("Category/{seoUrl}")]
        public async Task<IActionResult> Detail(string seoUrl, CancellationToken cancellationToken)
        {
            ViewData["Title"] = "Kategori";
            if (string.IsNullOrEmpty(seoUrl))
            {
                return RedirectToAction("Index");
            }
            var categoryResult = await _categoryService.GetCategoryBySeoUrlAsync(seoUrl, cancellationToken);
            if (categoryResult == null || categoryResult.Data == null || !categoryResult.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            ViewData["Title"] = categoryResult.Data.Name;
            var articleResult = await _articleService.GetByCategoryIdAsync(categoryResult.Data.Id, cancellationToken);
            if (articleResult == null || articleResult.Data == null || !articleResult.IsSuccess)
            {
                return View(new List<ArticleListViewModel>());
            }
            var map = articleResult.Data.OrderByDescending(x=> x.CreatedDate).Adapt<List<ArticleListViewModel>>();
            if (map.Count!=0)
            {               
                foreach (var item in map)
                {
                    if (item.Body?.Length>100)
                    {
                        item.Body = (item.Body?.Substring(0, 100)?.ToString() ?? "") + " ...";
                    }
                    item.CategorySeoUrl = seoUrl;
                }              
            }           
            return View(map);
        }
    }
}
