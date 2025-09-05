using System;

namespace OvgBlog.Business.Dto.Base;

public class BaseResponseDto
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

}