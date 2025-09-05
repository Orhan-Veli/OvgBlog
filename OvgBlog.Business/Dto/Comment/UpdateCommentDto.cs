using System;

namespace OvgBlog.Business.Dto.Comment;

public class UpdateCommentDto
{
    public Guid Id { get; set; }
    public Guid ArticleId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Body { get; set; }
}