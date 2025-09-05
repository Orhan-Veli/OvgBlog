using System;
using System.Collections.Generic;
using OvgBlog.Business.Dto.Base;

namespace OvgBlog.Business.Dto;

public class TagDto : BaseResponseDto
{
    public string Name { get; set; }
    public string SeoUrl { get; set; }

    public List<ArticleTagRelationDto> ArticleTagRelations { get; set; }
}