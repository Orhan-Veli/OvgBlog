using System;
using System.Collections.Generic;

namespace OvgBlog.Business.Dto.User;

public class UserFilterDto
{
    public List<Guid> Ids { get; set; } = new();
}