using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Shared.Result
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public T Entity { get; set;}
    }
}
