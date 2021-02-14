using System;
using System.Collections.Generic;
using System.Text;

namespace OvgBlog.Business.Constants
{
    public class Result<T> : IResult<T> where T:class
    {
        public Result(bool success,string message,T data):this(success)
        {
            Message = message;
            Data = data;
        }
        public Result(bool success)
        {
            Success = success;
        }
        public Result(bool success,T data)
        {
            Success = success;
            Data = data;
        }
        public Result(bool success, string message) : this(success)
        {
            Message = message;            
        }

        public bool Success { get; }
        public string Message { get; }
        public T Data { get; }
    }
}
