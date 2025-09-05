using System;
using System.Collections.Generic;

namespace OvgBlog.Business.Dto;

public class UpdateTagDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SeoUrl { get; set; }
    
}