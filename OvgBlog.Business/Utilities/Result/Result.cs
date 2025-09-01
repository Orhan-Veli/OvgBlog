using System;
using System.Collections.Generic;
using System.Text;

namespace OvgBlog.Business.Constants
{
    public class Result<T> : IResult<T>
    {
        public Result(bool success,string message,T data):this(success)
        {
            Message = message;
            Data = data;
        }
        public Result(bool success)
        {
            IsSuccess = success;
        }
        public Result(bool success,T data)
        {
            IsSuccess = success;
            Data = data;
        }
        public Result(bool success, string message) : this(success)
        {
            Message = message;            
        }

        public bool IsSuccess { get; }
        public string Message { get; }
        public T Data { get; }
    }
}
