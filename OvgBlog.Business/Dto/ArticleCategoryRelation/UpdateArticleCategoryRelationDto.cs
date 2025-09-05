using System;

namespace OvgBlog.Business.Dto;

public class UpdateArticleCategoryRelationDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid ArticleId { get; set; }
}