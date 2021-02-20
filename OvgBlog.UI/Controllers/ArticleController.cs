using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OvgBlog.Business.Abstract;
using OvgBlog.UI.Models;
using System.Threading.Tasks;

namespace OvgBlog.UI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;

        private readonly ILogger<ArticleController> _logger;

        public ArticleController(ILogger<ArticleController> logger, IArticleService articleService, IUserService userService)
        {
            _logger = logger;
            _articleService = articleService;
            _userService = userService;
        }

        // ovgblog.com/article/yazinin-basligi
        //[Route("{seoUrl}")]
        public async Task<IActionResult> Index(string seoUrl)
        {
            if (string.IsNullOrEmpty(seoUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            var response = await _articleService.GetBySeoUrl(seoUrl);
            if (!response.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = response.Data.Adapt<ArticleDetailViewModel>();

            var userResponse = await _userService.GetById(response.Data.UserId);
            if (userResponse.Success)
            {
                model.UserName = userResponse.Data.Name;
            }

            return View(model);
        }
    }
}
