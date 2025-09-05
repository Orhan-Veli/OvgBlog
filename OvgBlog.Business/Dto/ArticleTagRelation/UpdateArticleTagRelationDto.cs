using System;

namespace OvgBlog.Business.Dto;

public class UpdateArticleTagRelationDto
{
    public Guid Id { get; set; }
    public Guid TagId { get; set; }
    public Guid ArticleId { get; set; }
}