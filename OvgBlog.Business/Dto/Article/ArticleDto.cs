using System;
using System.Collections.Generic;
using OvgBlog.Business.Dto.Comment;
using OvgBlog.Business.Dto.User;

namespace OvgBlog.Business.Dto;

public class ArticleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public Guid UserId { get; set; }
    public string SeoUrl { get; set; }
    public string ImageUrl { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public virtual UserDto User { get; set; }
    public List<ArticleCategoryRelationDto> ArticleCategoryRelations { get; set; } = [];
    public List<ArticleTagRelationDto> ArticleTagRelations { get; set; } = [];
    public List<CommentDto> Comments { get; set; } = [];
}