using System;
using System.Collections.Generic;

namespace OvgBlog.Business.Dto.Comment;

public class CommentFilterDto
{
    public List<Guid> Ids { get; set; } = new();
    public List<Guid> ArticleIds { get; set; } = new();
}