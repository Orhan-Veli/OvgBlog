using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OvgBlog.Business.Abstract;
using OvgBlog.DAL.Data;
using OvgBlog.UI.Extentions;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OvgBlog.UI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //TODO(proje sonunda yapılacak): Parola encripyt  1234 --> as54dfgfdgd65dfgd778_'r34dfgfd 
        private readonly ILogger<AdminController> _logger;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly ITagService _tagService;
        private readonly ICommentService _commentService;

        public AdminController(ILogger<AdminController> logger, IUserService userService, ICategoryService categoryService, IArticleService articleService, ITagService tagService, ICommentService commentService)
        {
            _logger = logger;
            _userService = userService;
            _categoryService = categoryService;
            _articleService = articleService;
            _tagService = tagService;
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {                 
            var adminViewModelCounts = new AdminListViewModel
            {
                ArticleCount = _articleService.GetAll().Result.Data.Count(),
                CategoryCount = _categoryService.GetAll().Result.Data.Count(),
                TagCount = _tagService.GetAll().Result.Data.Count(),
                CommentCount = _commentService.GetAll().Result.Data.Count()            
            };
            return View(adminViewModelCounts);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var entity = await _categoryService.GetAll();
            var categoryList = entity.Data.Adapt<List<CategoryListViewModel>>();
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            var key = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryAddViewModel categoryAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonResultModel<Category>(false, "Tüm alanları doldurun."));
            }
            categoryAddViewModel.SeoUrl = categoryAddViewModel.SeoUrl.ReplaceSeoUrl();
            var result = await _categoryService.CategoryBySeoUrl(categoryAddViewModel.SeoUrl);
            if (result.Success)
            {
                return Json(new JsonResultModel<Category>(false, "SeoUrl zaten bulunuyor."));
            }
            var category = categoryAddViewModel.Adapt<Category>();
            var createResult = await _categoryService.Create(category);
            if (!createResult.Success || createResult.Data == null)
            {
                return Json(new JsonResultModel<Category>(false, "Kayıt Eklenemedi"));
            }
            return Json(new JsonResultModel<Category>(true, createResult.Data, "Kayıt eklendi"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryListViewModel categoryListViewModel)
        {
            var result = await _categoryService.GetById(categoryListViewModel.Id);
            if (result.Data == null)
            {
                return Json(new JsonResultModel<Category>(false, "Category is not found."));
            }
            result.Data.ImageUrl = categoryListViewModel.ImageUrl;
            result.Data.SeoUrl = categoryListViewModel.SeoUrl;
            result.Data.Name = categoryListViewModel.Name;
            await _categoryService.Update(result.Data);
            return Json(new JsonResultModel<Category>(true, result.Data, "Güncellendi."));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Json(new JsonResultModel<Category>(false, "Geçersiz kategori id"));
            }
            var deleteResult = await _categoryService.Delete(id);
            return Json(new JsonResultModel<Category>(deleteResult.Success, deleteResult.Message));
        }
        [HttpGet]
        public async Task<IActionResult> AddArticle()
        {
            var model = new ArticleAddViewModel();
            var result = await _categoryService.GetAll();
            model.CategoryList = result.Data.ToList().Adapt<List<CategoryListViewModel>>();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(ArticleAddViewModel model)

        {
            //Needs to move business
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.SeoUrl = model.SeoUrl.ReplaceSeoUrl();
            var result = await _articleService.GetBySeoUrl(model.SeoUrl);
            if (result.Success && result.Data != null)
            {
                ModelState.AddModelError("SeoUrl", "SeoUrl is already taken");
                return View(model);
            }

            var tags = model.TagName.Split(",");
            var article = model.Adapt<Article>();
            for (int i = 0; i < tags.Length; i++)
            {
                var tagRelation = new ArticleTagRelation
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now
                };

                var getTag = await _tagService.FindIdByName(tags[i]);
                
                if (getTag.Data != null)
                {
                    tagRelation.TagId = getTag.Data.Id;
                }
                else
                {
                    var tagModel = new Tag { Name = tags[i],SeoUrl=tags[i].ReplaceSeoUrl(), CreatedDate = DateTime.Now, Id = Guid.NewGuid() };
                    await _tagService.Create(tagModel);
                    tagRelation.Tag = new Tag
                    {
                        Id = tagModel.Id,
                        CreatedDate = DateTime.Now,
                        Name=tagModel.Name,
                        SeoUrl=tagModel.SeoUrl
                    };
                    tagRelation.TagId = tagModel.Id;
                }                
                
                article.ArticleTagRelations.Add(tagRelation);
            }
            article.ArticleCategoryRelations.Add(new ArticleCategoryRelation
            {
                Id = Guid.NewGuid(),
                CategoryId = model.CategoryId,
                CreatedDate = DateTime.Now
            });
            await _articleService.Create(article);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ArticleList()
        {
            var list = await _articleService.GetAll();
            var articleList = list.Data.Adapt<List<ArticleListViewModel>>();
            return View(articleList);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateArticle(Guid id)
        {
            if (id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Id is not valid.");
                return RedirectToAction("ArticleListView");
            }
            var result = await _articleService.GetById(id);
            if (result.Data == null)
            {
                ModelState.AddModelError(string.Empty, "You dont have this Category.");
                return RedirectToAction("ArticleListView");
            }
            var categoryResult = await _categoryService.GetAll();
            var categoryList = categoryResult.Data.ToList().Adapt<List<CategoryListViewModel>>();
            var articleResult = result.Data.Adapt<ArticleUpdateViewModel>();
            var categoryId = result.Data.ArticleCategoryRelations.FirstOrDefault().CategoryId;
            articleResult.SelectedCategoryId = categoryId;
            articleResult.CategoryList = categoryList;
            return View(articleResult);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateArticle(ArticleUpdateViewModel articleUpdateViewModel)
        {
            //Need to move business
            var result = await _articleService.GetById(articleUpdateViewModel.Id);
            if (result.Data == null)
            {
                ModelState.AddModelError(string.Empty, "Category is not found.");
                return RedirectToAction("ArticleListView");
            }
            result.Data.ImageUrl = articleUpdateViewModel.ImageUrl;
            result.Data.SeoUrl = articleUpdateViewModel.SeoUrl;
            result.Data.Body = articleUpdateViewModel.Body;
            result.Data.Title = articleUpdateViewModel.Title;
            foreach (var item in result.Data.ArticleCategoryRelations)
            {
                item.IsDeleted = true;
                item.DeletedDate = DateTime.Now;
            }
            result.Data.ArticleCategoryRelations.Add(new ArticleCategoryRelation
            {
                CategoryId = articleUpdateViewModel.SelectedCategoryId,
                CreatedDate = DateTime.Now
            });
            await _articleService.Update(result.Data);
            return RedirectToAction("ArticleList");
        }

        [HttpGet]
        public async Task<IActionResult> TagList()
        {
            var list = await _tagService.GetAll();
            var tagList = list.Data.Adapt<List<TagViewModel>>();
            return View(tagList);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Json(new JsonResultModel<Tag>(false, "Geçersiz tag id"));
            }
            var deleteResult = await _tagService.Delete(id);
            return Json(new JsonResultModel<Tag>(deleteResult.Success, deleteResult.Message));
        }
        [HttpGet]
        public async Task<IActionResult> CommentList()
        {
            var list = await _commentService.GetAll();
            var commentList = list.Data.Adapt<List<CommentViewModel>>();
            return View(commentList);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Json(new JsonResultModel<Comment>(false, "Geçersiz article id"));
            }
            var deleteResult = await _commentService.Delete(id);
            return Json(new JsonResultModel<Comment>(deleteResult.Success, deleteResult.Message));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteArticle(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Json(new JsonResultModel<Article>(false, "Geçersiz article id"));
            }
            var deleteResult = await _articleService.Delete(id);
            return Json(new JsonResultModel<Article>(deleteResult.Success, deleteResult.Message));
        }
    }
}
