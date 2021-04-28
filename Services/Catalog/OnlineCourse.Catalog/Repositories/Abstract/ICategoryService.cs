using OnlineCourse.Catalog.Dtos;
using OnlineCourse.Catalog.Models;
using OnlineCourse.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Catalog.Repositories
{
    public interface ICategoryService
    {
        Task<IDataResult<IEnumerable<CategoryDto>>> GetAsync();
        Task<IDataResult<CategoryDto>> GetByIdAsync(string id);
        Task<IDataResult<CategoryDto>> GetByNameAsync(string name);
        Task<IDataResult<CategoryDto>> CreateAsync(CategoryDto entity);
        Task<IResult> UpdateAsync(CategoryDto entity);
        Task<IResult> DeleteAsync(string id);
    }
}
