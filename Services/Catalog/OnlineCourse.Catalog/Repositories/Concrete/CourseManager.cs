using AutoMapper;
using MongoDB.Driver;
using OnlineCourse.Catalog.Context;
using OnlineCourse.Catalog.Dtos;
using OnlineCourse.Catalog.Models;
using OnlineCourse.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Catalog.Repositories
{
    public class CourseManager : ICourseService
    {
        private readonly ICatalogContext _context;
        private readonly IMapper _mapper;
        public CourseManager(ICatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IDataResult<CourseCreateDto>> CreateAsync(CourseCreateDto entity)
        {
            var response = new DataResult<CourseCreateDto>();
            var model = _mapper.Map<Course>(entity);
            model.CreatedTime = DateTime.Now;
            await _context.Courses.InsertOneAsync(model);
            response.Entity = _mapper.Map<CourseCreateDto>(entity);
            response.IsSuccessfull = true;
            response.Message = ResultConstant.RecordCreateSuccessfully;
            return response;
        }

        public async Task<IResult> DeleteAsync(string id)
        {
            var response = new Result();
            FilterDefinition<Course> filter = Builders<Course>.Filter.Eq(x => x.Id, id);
            DeleteResult result = await _context.Courses.DeleteOneAsync(filter);
            if (result.IsAcknowledged && result.DeletedCount > 0)
            {
                response.Message = ResultConstant.RecordRemoveSuccessfully;
                response.IsSuccessfull = true;
            }
            else
            {
                response.Message = ResultConstant.RecordRemoveNotSuccessfully;
                response.IsSuccessfull = false;
            }
            return response;

        }

        public async Task<IDataResult<IEnumerable<CourseDto>>> GetAsync()
        {

            var response = new DataResult<IEnumerable<CourseDto>>();
            var result = await _context.Courses.Find(x => true).ToListAsync();
            if (result.Any())
            {
                foreach (var item in result)
                {
                    item.Category = await _context.Catagories.Find<Category>(c => c.Id == item.CategoryId).FirstOrDefaultAsync();
                }
                response.Entity = _mapper.Map<List<CourseDto>>(result);
            }
            else
            {
                response.Entity = null;
            }
            return response;
        }

        public async Task<IDataResult<CourseDto>> GetByIdAsync(string id)
        {
            var response = new DataResult<CourseDto>();
            var result = await _context.Courses.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (result == null)
            {
                response.IsSuccessfull = false;
                response.Entity = null;
                response.Message = ResultConstant.RecordNotFound;
            }
            else
            {
                response.IsSuccessfull = true;
                response.Entity = _mapper.Map<CourseDto>(result);
                response.Message = ResultConstant.RecordFound;
            }
            return response;
        }

        public async Task<IDataResult<CourseDto>> GetByUserIdAsync(string userId)
        {
            var response = new DataResult<CourseDto>();
            var result = await _context.Courses.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            if (result == null)
            {
                response.IsSuccessfull = false;
                response.Entity = null;
                response.Message = ResultConstant.RecordNotFound;
            }
            else
            {
                response.IsSuccessfull = true;
                response.Entity = _mapper.Map<CourseDto>(result);
                response.Message = ResultConstant.RecordFound;
            }
            return response;
        }

        public async Task<IDataResult<CourseDto>> GetByNameAsync(string name)
        {
            var response = new DataResult<CourseDto>();
            FilterDefinition<Course> filter = Builders<Course>.Filter.ElemMatch(x => x.Name, name);
            var result = await _context.Courses.Find(filter).FirstOrDefaultAsync();
            if (result == null)
            {
                response.Message = ResultConstant.RecordNotFound;
                response.Entity = null;
                response.IsSuccessfull = false;
            }
            else
            {
                response.Message = ResultConstant.RecordFound;
                response.Entity = _mapper.Map<CourseDto>(result);
                response.IsSuccessfull = true;
            }
            return response;

        }
        public async Task<IResult> UpdateAsync(CourseUpdateDto entity)
        {
            var response = new Result();
            var result = await _context.Courses.ReplaceOneAsync(x => x.Id.Equals(entity.Id), _mapper.Map<Course>(entity));
            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                response.IsSuccessfull = true;
                response.Message = ResultConstant.RecordUpdateSuccessfully;
            }
            else
            {
                response.IsSuccessfull = false;
                response.Message = ResultConstant.RecordUpdateNotSuccessfully;
            }
            return response;
        }
    }
}
