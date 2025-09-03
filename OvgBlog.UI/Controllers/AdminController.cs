using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OvgBlog.Business.Abstract;
using OvgBlog.DAL.Data;
using OvgBlog.UI.Extentions;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using OvgBlog.Business.Dto;

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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IContactService _contactService;
        public AdminController(ILogger<AdminController> logger,
            IUserService userService,
            ICategoryService categoryService,
            IArticleService articleService,
            ITagService tagService,
            ICommentService commentService,
            IWebHostEnvironment webHostEnvironment,
            IContactService contactService
            )
        {
            _logger = logger;
            _userService = userService;
            _categoryService = categoryService;
            _articleService = articleService;
            _tagService = tagService;
            _commentService = commentService;
            _webHostEnvironment = webHostEnvironment;
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {                 
            var adminViewModelCounts = new AdminListViewModel
            {
                ArticleCount = _articleService.GetAllAsync(cancellationToken).Result.Data.Count(),
                CategoryCount = _categoryService.GetAllAsync(cancellationToken).Result.Data.Count(),
                TagCount = _tagService.GetAllAsync(cancellationToken).Result.Data.Count(),
                CommentCount =  _commentService.GetAllAsync(cancellationToken).Result.Data.Count(),    
                ContactCount =  _contactService.GetAllAsync(cancellationToken).Result.Data.Count()
            };
            return View(adminViewModelCounts);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList(CancellationToken cancellationToken)
        {
            var entity = await _categoryService.GetAllAsync(cancellationToken);
            var categoryList = entity.Data.Adapt<List<CategoryListViewModel>>();
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryAddViewModel categoryAddViewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonResultModel<Category>(false, "Tüm alanları doldurun."));
            }
            categoryAddViewModel.SeoUrl = categoryAddViewModel.SeoUrl.ReplaceSeoUrl();
            var result = await _categoryService.CategoryBySeoUrlAsync(categoryAddViewModel.SeoUrl, cancellationToken);
            if (result.IsSuccess)
            {
                return Json(new JsonResultModel<Category>(false, "SeoUrl zaten bulunuyor."));
            }
            var category = categoryAddViewModel.Adapt<Category>();
            var createResult = await _categoryService.CreateAsync(category, cancellationToken);
            if (!createResult.IsSuccess || createResult.Data == null)
            {
                return Json(new JsonResultModel<Category>(false, "Kayıt Eklenemedi"));
            }
            return Json(new JsonResultModel<Category>(true, createResult.Data, "Kayıt eklendi"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryListViewModel categoryListViewModel, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetByIdAsync(categoryListViewModel.Id, cancellationToken);
            if (result.Data == null)
            {
                return Json(new JsonResultModel<Category>(false, "Category is not found."));
            }
            result.Data.ImageUrl = categoryListViewModel.ImageUrl;
            result.Data.SeoUrl = categoryListViewModel.SeoUrl;
            result.Data.Name = categoryListViewModel.Name;
            await _categoryService.UpdateAsync(result.Data, cancellationToken);
            return Json(new JsonResultModel<Category>(true, result.Data, "Güncellendi."));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return Json(new JsonResultModel<Category>(false, "Geçersiz kategori id"));
            }
            var deleteResult = await _categoryService.DeleteAsync(id, cancellationToken);
            return Json(new JsonResultModel<Category>(deleteResult.IsSuccess, deleteResult.Message));
        }
        [HttpGet]
        public async Task<IActionResult> AddArticle(CancellationToken cancellationToken)
        {
            var model = new ArticleAddViewModel();
            var result = await _categoryService.GetAllAsync(cancellationToken);
            model.CategoryList = result.Data.ToList().Adapt<List<CategoryListViewModel>>();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(ArticleAddViewModel model, CancellationToken cancellationToken)

        {            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var dto = model.Adapt<CreateArticleDto>();
            var userKey = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            if (userKey != null)
            {
                dto.UserId = Guid.TryParse(userKey, out var parsedId) ? parsedId : Guid.Empty;
            }
            
            dto.RootPath = _webHostEnvironment.WebRootPath;
            await _articleService.CreateAsync(dto, cancellationToken);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ArticleList(CancellationToken cancellationToken)
        {
            var list = await _articleService.GetAllAsync(cancellationToken);
            var articleList = list.Data.Adapt<List<ArticleListViewModel>>();
            return View(articleList);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateArticle(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Id is not valid.");
                return RedirectToAction("ArticleListView");
            }
            var result = await _articleService.GetByIdAsync(id, cancellationToken);
            if (result.Data == null)
            {
                ModelState.AddModelError(string.Empty, "You dont have this Category.");
                return RedirectToAction("ArticleListView");
            }
            var categoryResult = await _categoryService.GetAllAsync(cancellationToken);
            var categoryList = categoryResult.Data.ToList().Adapt<List<CategoryListViewModel>>();
            var articleResult = result.Data.Adapt<ArticleUpdateViewModel>();
            var categoryId = result.Data.ArticleCategoryRelations.FirstOrDefault().CategoryId;
            articleResult.SelectedCategoryId = categoryId;
            articleResult.CategoryList = categoryList;
            return View(articleResult);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateArticle(ArticleUpdateViewModel articleUpdateViewModel, CancellationToken cancellationToken)
        {
            //Need to move business
            var result = await _articleService.GetByIdAsync(articleUpdateViewModel.Id, cancellationToken);
            if (result.Data == null)
            {
                ModelState.AddModelError(string.Empty, "Category is not found.");
                return RedirectToAction("ArticleListView");
            }
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", result.Data.ImageUrl);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(articleUpdateViewModel.FileImageUrl.FileName);
            string extension = Path.GetExtension(articleUpdateViewModel.FileImageUrl.FileName);
            string path = Path.Combine(wwwRootPath + "/uploads/", fileName + extension);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await articleUpdateViewModel.FileImageUrl.CopyToAsync(fileStream);
            }
            result.Data.ImageUrl = articleUpdateViewModel.FileImageUrl.FileName;
            //result.Data.ImageUrl = articleUpdateViewModel.ImageUrl;
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
                CreatedDate = DateTime.Now,
                Id = Guid.NewGuid(),
                ArticleId = result.Data.Id
            });
            //await _articleService.UpdateAsync(result.Data, cancellationToken);
            return RedirectToAction("ArticleList");
        }

        [HttpGet]
        public async Task<IActionResult> TagList(CancellationToken cancellationToken)
        {
            var list = await _tagService.GetAllAsync(cancellationToken);
            var tagList = list.Data.Where(x=> !x.IsDeleted).Adapt<List<TagViewModel>>();
            return View(tagList);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTag(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return Json(new JsonResultModel<Tag>(false, "Geçersiz tag id"));
            }
            var deleteResult = await _tagService.DeleteAsync(id, cancellationToken);
            return Json(new JsonResultModel<Tag>(deleteResult.IsSuccess, deleteResult.Message));
        }
        [HttpGet]
        public async Task<IActionResult> CommentList(CancellationToken cancellationToken)
        {
            var list = await _commentService.GetAllAsync(cancellationToken);
            var commentList = list.Data.Adapt<List<CommentViewModel>>();
            return View(commentList);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return Json(new JsonResultModel<Comment>(false, "Geçersiz article id"));
            }
            var deleteResult = await _commentService.DeleteAsync(id, cancellationToken);
            return Json(new JsonResultModel<Comment>(deleteResult.IsSuccess, deleteResult.Message));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteArticle(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return Json(new JsonResultModel<Article>(false, "Geçersiz article id"));
            }
            var imageModel = await _articleService.GetByIdAsync(id, cancellationToken);
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", imageModel.Data.ImageUrl);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            var deleteResult = await _articleService.DeleteAsync(id, cancellationToken);            
            return Json(new JsonResultModel<Article>(deleteResult.IsSuccess, deleteResult.Message));
        }
        [HttpGet]
        public async Task<IActionResult> ContactList(CancellationToken cancellationToken)
        {
            var result = await _contactService.GetAllAsync(cancellationToken);
            var list = result.Data.Adapt<List<ContactListViewModel>>();
            return View(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("ContactList");
            }
            var result = await _contactService.GetAsync(id, cancellationToken);
            if (result == null || result.Data == null || !result.IsSuccess)
            {
                return RedirectToAction("ContactList");
            }
            var model = result.Data.Adapt<ContactListViewModel>();
            return View("ContactView", model);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteContact(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return Json(new JsonResultModel<ContactListViewModel>(false, "Id is not valid."));
            }
            await _contactService.DeleteAsync(id, cancellationToken);
            return Json(new JsonResultModel<ContactListViewModel>(true, "Kayıt silinmiştir."));
        }
    }
}
