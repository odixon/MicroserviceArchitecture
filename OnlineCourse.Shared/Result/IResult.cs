using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Shared.Result
{
    public interface IResult
    {
        bool IsSuccessfull { get; set; }
        string Message { get; set; }
    }
}
