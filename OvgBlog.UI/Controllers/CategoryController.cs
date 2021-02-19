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
        private readonly ILogger _logger;
        public CategoryController(ICategoryService categoryService,ILogger logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }
        [HttpGet("Music")]
        public async Task<IActionResult> Music()
        {

            return View();
        }
        [HttpGet("Languages")]
        public async Task<IActionResult> Languages()
        {


            return View();
        }
        [HttpGet("It")]
        public async Task<IActionResult> It()
        {

            return View();
        }

        public async Task<IActionResult> AboutMe()
        {

            return View();
        }

    }
}
