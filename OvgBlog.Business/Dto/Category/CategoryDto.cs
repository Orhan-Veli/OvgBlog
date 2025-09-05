using System;
using System.Collections.Generic;
using OvgBlog.Business.Dto.Base;

namespace OvgBlog.Business.Dto;

public class CategoryDto : BaseResponseDto
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string SeoUrl { get; set; }

    public List<ArticleCategoryRelationDto> ArticleCategoryRelations { get; set; } = [];
}