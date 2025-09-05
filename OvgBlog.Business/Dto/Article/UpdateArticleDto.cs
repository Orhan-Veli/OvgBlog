using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using OvgBlog.Business.Dto.Comment;

namespace OvgBlog.Business.Dto;

public class UpdateArticleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string SeoUrl { get; set; }
    public string RootPath { get; set; }
    public IFormFile FileImageUrl { get; set; }
    public string TagName { get; set; }
    public Guid CategoryId { get; set; }
    public Guid UserId { get; set; }                                                                                                                                                                                                                                                                                                                                                           
}