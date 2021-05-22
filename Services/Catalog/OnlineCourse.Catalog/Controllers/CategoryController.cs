using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Catalog.Dtos;
using OnlineCourse.Catalog.Repositories;
using OnlineCourse.Extensions;
using OnlineCourse.Shared.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineCourse.Catalog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPut]
        [Route("update-category")]
        public async Task<ActionResult> UpdateAsync(CategoryDto category)
        {
            var response = await _categoryService.UpdateAsync(category);
            return this.CreateResponse<IResult>(response);
        }
        [HttpGet]
        [Route("get-category-byid/{id}")]
        public async Task<ActionResult> GetByIdAsync(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return this.CreateResponse<IDataResult<CategoryDto>>(response);
        }
        [HttpDelete]
        [Route("delete-category/{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var response = await _categoryService.DeleteAsync(id);
            return this.CreateResponse<IResult>(response);
        }
        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult> GetAllAsync()
        {
            var response = await _categoryService.GetAsync();
            return this.CreateResponse<IDataResult<IEnumerable<CategoryDto>>>(response);
        }
        [HttpPost]
        [Route("create-category")]
        public async Task<ActionResult> CreateAsync(CategoryDto category)
        {
            var response = await _categoryService.CreateAsync(category);
            return this.CreateResponse<IDataResult<CategoryDto>>(response);
        }

    }
}
