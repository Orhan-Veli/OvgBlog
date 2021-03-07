using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OvgBlog.Business.Abstract;
using OvgBlog.DAL.Data.Entities;
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
        public IActionResult Index()
        {
            return View();
            
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
           var key = User.Claims.FirstOrDefault(x=> x.Type== ClaimTypes.Name).Value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryAddViewModel categoryAddViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(categoryAddViewModel);
            }
            categoryAddViewModel.SeoUrl = categoryAddViewModel.SeoUrl.ReplaceSeoUrl();
            var result = await _categoryService.CategoryBySeoUrl(categoryAddViewModel.SeoUrl);
            if (result.Success)
            {
                ModelState.AddModelError("SeoUrl", "SeoUrl is already taken");
                return View(categoryAddViewModel);
            }
            var category =  categoryAddViewModel.Adapt<Category>();
            await _categoryService.Create(category);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty,"Id is not valid.");
                return View("Index");
            }
            var result = await _categoryService.GetById(id);
            if (result.Data == null)
            {
                ModelState.AddModelError(string.Empty, "You dont have this Category.");
                return View("Index");
            }
            var categoryResult = result.Data.Adapt<CategoryListViewModel>();
            return View(categoryResult);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryListViewModel categoryListViewModel)
        {
            var result = await _categoryService.GetById(categoryListViewModel.Id);
            if (result.Data == null)
            {
                ModelState.AddModelError(string.Empty, "Category is not found.");
                return RedirectToAction("Index");
            }
            result.Data.ImageUrl = categoryListViewModel.ImageUrl;
            result.Data.SeoUrl = categoryListViewModel.SeoUrl;
            result.Data.Name = categoryListViewModel.Name;
            await _categoryService.Update(result.Data);
            return RedirectToAction("CategoryListView");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            if(id == Guid.Empty)
            {
                return Json(new JsonResultModel(false, "Geçersiz kategori id"));
            }
            var deleteResult = await _categoryService.Delete(id);
            return Json(new JsonResultModel(deleteResult.Success, deleteResult.Message));
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
                if(getTag != null)
                {
                    tagRelation.TagId = getTag.Data.Id;
                }
                else
                {
                    tagRelation.Tag = new Tag
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now
                    };
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
        public async Task<IActionResult> DeleteArticle(Guid id)
        {
            if (id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Id is not found.");
                return RedirectToAction("ArticleListView");
            }
            var result = await _articleService.GetById(id);
            if (result.Data == null)
            {
                return RedirectToAction("ArticleListView");
            }
            return View("DeleteArticle");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteArticle(ArticleListViewModel articleListViewModel)
        {
            if (articleListViewModel.Id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Id is not valid");
                return RedirectToAction("ArticleListView");
            }
            var result = await _articleService.GetById(articleListViewModel.Id);
            if (result.Data == null)
            {
                ModelState.AddModelError(string.Empty, "There is no category with that id.");
                return RedirectToAction("ArticleListView");
            }
            await _articleService.Delete(articleListViewModel.Id);
            return View("Index");
        }
           
        [HttpGet]
        public async Task<IActionResult> TagList()
        {
            var list = await _tagService.GetAll();
            var tagList = list.Data.Adapt<List<TagViewModel>>();
            return View(tagList);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTag(TagViewModel tagViewModel)
        {
            if (tagViewModel.Id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Id is not valid");
                return RedirectToAction("ArticleListView");
            }
            var result = await _tagService.GetById(tagViewModel.Id);
            if (result.Data == null)
            {
                ModelState.AddModelError(string.Empty, "There is no category with that id.");
                return RedirectToAction("ArticleListView");
            }
            await _tagService.Delete(tagViewModel.Id);
            return View("Index");
        }
        [HttpGet]
        public async Task<IActionResult> CommentList()
        {
            var list = await _commentService.GetAll();
            var commentList = list.Data.Adapt<List<CommentViewModel>>();
            return View(commentList);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteComment(CommentViewModel commentViewModel)
        {
            if (commentViewModel.Id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Id is not valid");
                return RedirectToAction("ArticleListView");
            }
            var result = await _commentService.GetById(commentViewModel.Id);
            if (result.Data == null)
            {
                ModelState.AddModelError(string.Empty, "There is no category with that id.");
                return RedirectToAction("ArticleListView");
            }
            await _commentService.Delete(commentViewModel.Id);
            return View("Index");
        }
    }
}
