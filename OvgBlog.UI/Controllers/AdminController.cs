using Mapster;
using Microsoft.AspNetCore.Authorization;
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
    public class AdminController : Controller
    {
        //TODO: Authorize attribute eklenecek ve isLogin silinecek
        //TODO(proje sonunda yapılacak): Parola encripyt  1234 --> as54dfgfdgd65dfgd778_'r34dfgfd 
        private static bool isLogin = false;
        private readonly ILogger<AdminController> _logger;
        private readonly IUserService _userService;
        public AdminController(ILogger<AdminController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["ErrorMessage"] = null;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewController model)
        {
            //TODO: BLL'de kullanıcı adı ve parolaya göre success dönen bir metod eklenebilir
            if (!ModelState.IsValid || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                ViewData["ErrorMessage"] = "Tüm alanları eksiksiz doldurun!";
                return View(model);
            }
            var result = await _userService.GetUser(model.UserName,model.Password);
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

    }
}
