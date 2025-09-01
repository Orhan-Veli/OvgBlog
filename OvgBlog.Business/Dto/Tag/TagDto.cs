using System;
using System.Collections.Generic;

namespace OvgBlog.Business.Dto;

public class TagDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SeoUrl { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public List<ArticleTagRelationDto> ArticleTagRelations { get; set; }
}