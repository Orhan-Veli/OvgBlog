using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OvgBlog.Business.Abstract;
using OvgBlog.UI.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

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

        [HttpGet("article/{seoUrl}")]
        public async Task<IActionResult> Index(string seoUrl, CancellationToken cancellationToken)
        
         {
            if (string.IsNullOrEmpty(seoUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            var response = await _articleService.GetBySeoUrlAsync(seoUrl, cancellationToken);           
            if (!response.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            response.Data.Comments = response.Data.Comments.Where(x => !x.IsDeleted).ToList();
            var articleTags = await _tagService.GetAllAsync(cancellationToken);
            if (articleTags.Data == null || !articleTags.IsSuccess || response.Data.ArticleTagRelations.Count == 0)
            {
                var model = response.Data.Adapt<ArticleDetailViewModel>();               
                var userResponse = await _userService.GetByIdAsync(response.Data.UserId, cancellationToken);
                if (userResponse.IsSuccess)
                {
                    model.UserName = userResponse.Data.Name;
                }               
                return View(model);
            }
            else
            {
                var articleTagIds = response.Data.ArticleTagRelations.Select(x => x.TagId).ToList();
                var tagListResponse = await _tagService.GetByIdsAsync(articleTagIds, cancellationToken);
                
                var model = response.Data.Adapt<ArticleDetailViewModel>();                
                foreach (var item in tagListResponse.Data)
                {
                   if(item.ArticleTagRelations.Where(x => !x.Tag.IsDeleted).Any())
                    {
                        model.Tags.Add(item.Adapt<TagViewModel>());
                    }                   
                }
                var userResponse = await _userService.GetByIdAsync(response.Data.UserId, cancellationToken);
                if (userResponse.IsSuccess)
                {
                    model.UserName = userResponse.Data.Name;
                }
                return View(model);
            }

        }
        
    }
}
