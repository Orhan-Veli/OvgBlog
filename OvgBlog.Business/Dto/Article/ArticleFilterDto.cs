using System;
using System.Collections.Generic;

namespace OvgBlog.Business.Dto;

public class ArticleFilterDto
{
    public List<Guid> Ids { get; set; } = new();
    public List<Guid> UserIds { get; set; } = new();
}