using System;

namespace OvgBlog.Business.Dto.Comment;

public class CreateCommentDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Body { get; set; }
    public Guid ArticleId { get; set; }
}