using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OvgBlog.Business.Abstract;
using OvgBlog.Business.Services;

namespace OvgBlog.UI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        public string Message { get; set; }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                Message = "T�m alanlar� eksiksiz doldurun!";
                return Page();
            }
            var result = await _userService.CheckUser(UserName, Password);
            if (!result.IsSuccess)
            {
                Message = "Ad�n�z ve ya �ifreniz yanl��t�r.";
                return Page();
            }
            var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, UserName),
            new Claim(ClaimTypes.Role, "Admin")            
           };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties() { IsPersistent = true });
            return Redirect("/Admin");
       }
    }
}
