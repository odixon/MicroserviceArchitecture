using OnlineCourse.Catalog.Dtos;
using OnlineCourse.Catalog.Models;
using OnlineCourse.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Catalog.Repositories
{
    public interface ICourseService
    {
        Task<IDataResult<IEnumerable<CourseDto>>> GetAsync();
        Task<IDataResult<CourseDto>> GetByIdAsync(string id);
        Task<IDataResult<CourseDto>> GetByNameAsync(string name);
        Task<IDataResult<CourseCreateDto>> CreateAsync(CourseCreateDto entity);
        Task<IResult> UpdateAsync(CourseUpdateDto entity);
        Task<IResult> DeleteAsync(string id);
        Task<IDataResult<CourseDto>> GetByUserIdAsync(string userId);
    }
}
