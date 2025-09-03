using System;

namespace OvgBlog.DAL.Helpers;

public class OvgBlogException : Exception
{
    public int Code { get; set; }
    
    public OvgBlogException(string message) : base(message)
    {
        
    }

    public OvgBlogException(string message, Exception innerException) : base(message, innerException)
    {
        
    }
    
    public OvgBlogException(string message, string exceptionMessage) : base(message)
    {
        
    }
    
    //ToDo exception code is required for finding the code
    public OvgBlogException(int code, string message, Exception innerException) : base(message, innerException)
    {
        Code = code;
    }
}