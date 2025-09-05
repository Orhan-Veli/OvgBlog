using System;
using System.Collections.Generic;
using OvgBlog.Business.Dto.Base;

namespace OvgBlog.Business.Dto.User;

public class UserDto : BaseResponseDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }

    public List<ArticleDto> Articles { get; set; } = [];
}