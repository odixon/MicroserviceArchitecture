using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Catalog.Settings
{
    public class CatalogDatabaseSettings : ICatalogDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
