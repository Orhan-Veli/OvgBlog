using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Globalization;
namespace OvgBlog.UI.Extentions
{
    public static class StringExtentions
    {
        public static string ReplaceSeoUrl(this string seoUrl)
        {
           return string.Join("",seoUrl.Normalize(NormalizationForm.FormD).Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).ToLower().Replace(" ","-");
        }
    }
}
