using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
