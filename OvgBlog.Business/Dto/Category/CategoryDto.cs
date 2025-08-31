using System;
using System.Collections.Generic;

namespace OvgBlog.Business.Dto;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string SeoUrl { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public List<ArticleCategoryRelationDto> ArticleCategoryRelations { get; set; } = [];
}