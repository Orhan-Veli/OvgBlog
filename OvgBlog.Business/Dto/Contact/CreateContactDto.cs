using System;

namespace OvgBlog.Business.Dto.Contact;

public class CreateContactDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Body { get; set; }
    public DateTime SendDate { get; set; }
}