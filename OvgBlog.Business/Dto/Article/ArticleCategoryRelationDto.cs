using System;

namespace OvgBlog.Business.Dto;

public class ArticleCategoryRelationDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid ArticleId { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public ArticleDto Article { get; set; }
    public CategoryDto Category { get; set; }
}