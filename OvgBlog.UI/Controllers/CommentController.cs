using Mapster;
using Microsoft.AspNetCore.Mvc;
using OvgBlog.Business.Abstract;
using OvgBlog.DAL.Data.Entities;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvgBlog.UI.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

       [HttpPost]
        public async Task<IActionResult> AddComment(CommentAddViewModel commentAddViewModel)
         {
            if (!ModelState.IsValid)
            {
                return  Json(new JsonResultModel<CommentAddViewModel>(false, "Model is not valid."));
            }
            var model = commentAddViewModel.Adapt<Comment>();
            var result = await _commentService.Create(model);
            if (!result.Success || result.Data == null)
            {
                return Json(new JsonResultModel<CommentAddViewModel>(false, "Model is not valid."));
            }
            return Json(new JsonResultModel<CommentAddViewModel>(true, "Model is valid."));
        }
    }
}
