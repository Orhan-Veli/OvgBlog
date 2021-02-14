using OvgBlog.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace OvgBlog.DAL.Data.Entities
{
    public class BaseEntity:IEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime UpdatedDate { get; set; }


    }
}
