﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OvgBlog.Business.Constants
{
    public interface IResult<T>
    {
        public bool Success { get; }

        public string Message { get; }

        public T Data { get; }
    }
}
