using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Catalog.Extensions
{
    public static class ResultBaseExtension
    {
        public static ActionResult CreateResponse<T>(this ControllerBase controller, T model)
        {
            return new ObjectResult(model);
        }
    }
}
