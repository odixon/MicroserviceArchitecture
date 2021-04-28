using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Shared.Result
{
    public interface IDataResult<T>
    {
         T Entity { get; set; }
    }
}
