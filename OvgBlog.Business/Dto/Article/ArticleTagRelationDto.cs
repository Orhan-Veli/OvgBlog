using System;
using OvgBlog.DAL.Data;

namespace OvgBlog.Business.Dto;

public class ArticleTagRelationDto
{
    public Guid Id { get; set; }
    public Guid TagId { get; set; }
    public Guid ArticleId { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public Article Article { get; set; }
    public Tag Tag { get; set; }
}