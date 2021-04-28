using AutoMapper;
using OnlineCourse.Catalog.Dtos;
using OnlineCourse.Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Catalog.Mapping
{
    public class CatalogMapping : Profile
    {
        public CatalogMapping()
        {
            CreateMap<Course, CourseUpdateDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
            CreateMap<Course, CourseCreateDto>().ReverseMap();
        }
    }
}
