using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Shared.Result
{
    public class Result : IResult
    {
        public bool IsSuccessfull { get ; set ; }
        public string Message { get; set ; }
    }
}
