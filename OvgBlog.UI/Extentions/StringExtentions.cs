using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace OvgBlog.UI.Extentions
{
    public static class StringExtentions
    {
        public static string ReplaceSeoUrl(this string seoUrl)
        {
           return string.Join("",seoUrl.Normalize(NormalizationForm.FormD).Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).ToLower().Replace(" ","-").Replace("ı","i");
        }
        public static bool EmailValidation(this string email)
        {           
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }
    }
    
}
