using System;
using OvgBlog.Business.Dto.Base;
using OvgBlog.DAL.Data;

namespace OvgBlog.Business.Dto;

public class ArticleTagRelationDto : BaseResponseDto
{
    public Guid TagId { get; set; }
    public Guid ArticleId { get; set; }

    public Article Article { get; set; }
    public Tag Tag { get; set; }
}