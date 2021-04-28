using MongoDB.Driver;
using OnlineCourse.Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Catalog.Context
{
    public interface ICatalogContext
    {
        IMongoCollection<Category> Catagories { get; set; }
        IMongoCollection<Course> Courses { get; set; }
    }
}
