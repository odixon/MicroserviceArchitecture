using MongoDB.Driver;
using OnlineCourse.Catalog.Models;
using OnlineCourse.Catalog.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Catalog.Context
{
    public class CatalogContext : ICatalogContext
    {

        public CatalogContext(ICatalogDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Catagories = database.GetCollection<Category>("Categories");
            Courses = database.GetCollection<Course>("Courses");
        }



        public IMongoCollection<Category> Catagories { get; set; }
        public IMongoCollection<Course> Courses { get; set; }
    }
}
