using Mapster;
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
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryService categoryService,ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }
        
        public async Task<IActionResult> Index()
        {
            //TODO: Kategorileri listeleyen sayfa yapılacak
            return View();
        }

        //  ovgblog.com/category/music
        [HttpGet("{seoUrl}")]
        public async Task<IActionResult> Detail(string seoUrl)
        {
            //TODO: 
            //seoUrl'ye göre kategori bul (categoryId)
            //bulduğun kategorinin yazılarını getir
            //yazı listesini list model'e bind et
            //yazi listesini döndür
            ViewData["Title"] = "Kategori ismi"; //categoryEntity.Name

            return View(); //yazı listesini dönecek
        }
    }
}
