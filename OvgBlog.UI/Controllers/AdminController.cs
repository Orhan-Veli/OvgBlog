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
using System.Threading.Tasks;

namespace OvgBlog.UI.Controllers
{
    public class AdminController : Controller
    {
        //TODO: Authorize attribute eklenecek ve isLogin silinecek
        //TODO(proje sonunda yapılacak): Parola encripyt  1234 --> as54dfgfdgd65dfgd778_'r34dfgfd 
        private static bool isLogin = false;
        private readonly ILogger<AdminController> _logger;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;

        public AdminController(ILogger<AdminController> logger, IUserService userService, ICategoryService categoryService)
        {
            _logger = logger;
            _userService = userService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["ErrorMessage"] = null;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //TODO: BLL'de kullanıcı adı ve parolaya göre success dönen bir metod eklenebilir
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Tüm alanları eksiksiz doldurun!";
                return View(model);
            }
            var result = await _userService.CheckUser(model.UserName,model.Password);
            if (!result.Success)
            {
                ViewData["ErrorMessage"] = "Adınız ve ya şifreniz yanlıştır.";
                return View();
            }
            isLogin = true;
            return RedirectToAction("Index");
        }

        //[OvgAuthorize]
        [HttpGet]
        public IActionResult Index()
        {
            if (isLogin) return View();
            else return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> AddCategory(CategoryAddViewModel categoryAddViewModel)
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
            return RedirectToAction("Index");
        }
    }
}
