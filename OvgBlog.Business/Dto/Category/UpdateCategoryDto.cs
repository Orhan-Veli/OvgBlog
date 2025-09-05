using System;

namespace OvgBlog.Business.Dto;

public class UpdateCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string SeoUrl { get; set; }
}