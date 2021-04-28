using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Catalog.Dtos;
using OnlineCourse.Catalog.Extensions;
using OnlineCourse.Catalog.Models;
using OnlineCourse.Catalog.Repositories;
using OnlineCourse.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Catalog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        ICourseService _courseRepository;
        public CourseController(ICourseService courseRepository)
        {
            _courseRepository = courseRepository;
        }
        [HttpGet]
        [Route("get-course-byid/{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var response = await _courseRepository.GetByIdAsync(id);
            return this.CreateResponse<IDataResult<CourseDto>>(response);
        }
        [HttpGet]
        [Route("getall-users-by-id/{userId}")]
        public async Task<ActionResult> GetByUserId(string userId)
        {
            var response = await _courseRepository.GetByUserIdAsync(userId);
            return this.CreateResponse<IDataResult<CourseDto>>(response);
        }
        [HttpPost]
        [Route("create-course")]
        public async Task<ActionResult> Create(CourseCreateDto course)
        {
            var response = await _courseRepository.CreateAsync(course);
            return this.CreateResponse<IDataResult<CourseCreateDto>>(response);
        }
        [HttpDelete]
        [Route("delete-course/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var response = await _courseRepository.DeleteAsync(id);
            return this.CreateResponse<IResult>(response);

        }
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseRepository.GetAsync();
            return this.CreateResponse<IDataResult<IEnumerable<CourseDto>>>(response);
        }
        [HttpPut]
        [Route("update-course")]
        public async Task<ActionResult> Update(CourseUpdateDto course)
        {
            var response = await _courseRepository.UpdateAsync(course);
            return this.CreateResponse<IResult>(response);
        }
    }
}
