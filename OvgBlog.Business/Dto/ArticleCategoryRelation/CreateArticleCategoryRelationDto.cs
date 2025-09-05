using System;

namespace OvgBlog.Business.Dto;

public class CreateArticleCategoryRelationDto
{
    public Guid CategoryId { get; set; }
    public Guid ArticleId { get; set; }
}