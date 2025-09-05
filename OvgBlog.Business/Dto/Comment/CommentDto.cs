using System;
using OvgBlog.Business.Dto.Base;

namespace OvgBlog.Business.Dto.Comment;

public class CommentDto : BaseResponseDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Body { get; set; }
    public Guid ArticleId { get; set; }

    public virtual ArticleDto Article { get; set; }
}