﻿using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OvgBlog.Business.Abstract;
using OvgBlog.UI.Models;
using System.Threading.Tasks;
using System.Linq;

namespace OvgBlog.UI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        private readonly ITagService _tagService;
        private readonly ILogger<ArticleController> _logger;

        public ArticleController(ILogger<ArticleController> logger, IArticleService articleService, IUserService userService, ITagService tagService)
        {
            _logger = logger;
            _articleService = articleService;
            _userService = userService;
            _tagService = tagService;
        }

        // ovgblog.com/article/yazinin-basligi
        //[Route("{seoUrl}")]
        [HttpGet]
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
            var articleTags = await _tagService.GetAll();
            if (articleTags.Data == null || !articleTags.Success || response.Data.ArticleTagRelations.Count == 0)
            {
                var model = response.Data.Adapt<ArticleDetailViewModel>();
                var userResponse = await _userService.GetById(response.Data.UserId);
                if (userResponse.Success)
                {
                    model.UserName = userResponse.Data.Name;
                }
                return View(model);
            }
            else
            {
                var model = response.Data.Adapt<ArticleDetailViewModel>();
                var model1 = articleTags.Data.Where(x => x.Id == response.Data.ArticleTagRelations.FirstOrDefault().TagId);

                foreach (var item in model1)
                {
                    model.Tags.Add(item.Adapt<TagViewModel>());
                }
                var userResponse = await _userService.GetById(response.Data.UserId);
                if (userResponse.Success)
                {
                    model.UserName = userResponse.Data.Name;
                }
                return View(model);
            }

        }
    }
}
