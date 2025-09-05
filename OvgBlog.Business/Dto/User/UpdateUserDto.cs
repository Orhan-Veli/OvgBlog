using System;
using System.Collections.Generic;

namespace OvgBlog.Business.Dto.User;

public class UpdateUserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
}