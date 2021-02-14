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

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        public async Task<IActionResult> Index()
        {
            var models = new List<ArticleListViewModel>();

            var response = await _articleService.GetAll();
            if (response.Success)
            {
                /*
                models = response.Result.Data.Select(x => new ArticleListViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Body = x.Body?.Substring(0, 100) + " ...",
                    ImageUrl = x.ImageUrl,
                    SeoUrl = x.SeoUrl
                }).ToList();
                */

                models = response.Data.Adapt<List<ArticleListViewModel>>();
                models.ForEach(item => item.Body = item.Body?.Substring(0, 100) + " ...");
            }

            return View(models.Take(10).ToList());
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
    }
}
