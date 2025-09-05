using System;

namespace OvgBlog.Business.Dto;

public class CreateArticleTagRelationDto
{
    public Guid TagId { get; set; }
    public Guid ArticleId { get; set; }
}