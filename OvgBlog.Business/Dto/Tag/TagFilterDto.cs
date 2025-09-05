using System;
using System.Collections.Generic;

namespace OvgBlog.Business.Dto;

public class TagFilterDto
{
    public List<Guid> Ids { get; set; } = new();
}