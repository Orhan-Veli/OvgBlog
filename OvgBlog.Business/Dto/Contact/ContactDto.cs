using System;
using OvgBlog.Business.Dto.Base;

namespace OvgBlog.Business.Dto.Contact;

public class ContactDto : BaseResponseDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Body { get; set; }
    public DateTime SendDate { get; set; }
}