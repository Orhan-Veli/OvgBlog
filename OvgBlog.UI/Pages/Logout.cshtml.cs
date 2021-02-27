using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OvgBlog.UI.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            foreach (var item in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(item);
            }
            LocalRedirect("/Login");
        }
    }
}
