using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Models
{
    public class LoginViewController
    {
        [Required(ErrorMessage ="Kullanıcı adı gereklidir.")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Parola alanı gereklidir.")]
        public string Password { get; set; }
    }
}
