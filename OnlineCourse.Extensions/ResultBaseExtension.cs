using Microsoft.AspNetCore.Mvc;
using System;

namespace OnlineCourse.Extensions
{
    public static class ResultBaseExtension
    {
        public static ActionResult CreateResponse<T>(this ControllerBase controller, T model)
        {
            return new ObjectResult(model);
        }
    }
}
