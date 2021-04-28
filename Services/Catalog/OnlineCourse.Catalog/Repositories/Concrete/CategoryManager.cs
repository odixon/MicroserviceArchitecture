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
    public class CategoryManager : ICategoryService
    {
        private readonly ICatalogContext _context;
        private readonly IMapper _mapper;
        public CategoryManager(ICatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IDataResult<CategoryDto>> CreateAsync(CategoryDto entity)
        {
            var response = new DataResult<CategoryDto>();
            await _context.Catagories.InsertOneAsync(_mapper.Map<Category>(entity));
            response.Entity = _mapper.Map<CategoryDto>(entity);
            response.Message = ResultConstant.RecordCreateSuccessfully;
            response.IsSuccessfull = true;
            return response;
        }
        public async Task<IResult> DeleteAsync(string id)
        {
            var response = new Result();
            FilterDefinition<Category> filter = Builders<Category>.Filter.Eq(x => x.Id, id);
            DeleteResult result = await _context.Catagories.DeleteOneAsync(filter);
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
        public async Task<IDataResult<IEnumerable<CategoryDto>>> GetAsync()
        {
            var response = new DataResult<IEnumerable<CategoryDto>>();
            var result = await _context.Catagories.Find(x => true).ToListAsync();
            if (result.Any())
            {
                response.Entity = _mapper.Map<IEnumerable<CategoryDto>>(result);
                response.IsSuccessfull = true;
            }
            else
            {
                response.Entity = null;
                response.IsSuccessfull = false;
            }
            return response;
        }
        public async Task<IDataResult<CategoryDto>> GetByIdAsync(string id)
        {
            var response = new DataResult<CategoryDto>();
            var result = await _context.Catagories.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (result == null)
            {
                response.IsSuccessfull = false;
                response.Entity = null;
                response.Message = ResultConstant.RecordNotFound;
            }
            else
            {
                response.Entity = _mapper.Map<CategoryDto>(result);
                response.Message = ResultConstant.RecordFound;
                response.IsSuccessfull = true;
            }
            return response;
        }
        public async Task<IDataResult<CategoryDto>> GetByNameAsync(string name)
        {
            var response = new DataResult<CategoryDto>();
            FilterDefinition<Category> filter = Builders<Category>.Filter.ElemMatch(x => x.Name, name);
            var result = await _context.Catagories.Find(filter).FirstOrDefaultAsync();
            if (result == null)
            {
                response.Message = ResultConstant.RecordNotFound;
                response.Entity = null;
                response.IsSuccessfull = false;
            }
            else
            {
                response.Message = ResultConstant.RecordFound;
                response.Entity = _mapper.Map<CategoryDto>(result);
                response.IsSuccessfull = true;
            }
            return response;

        }
        public async Task<IResult> UpdateAsync(CategoryDto entity)
        {
            var response = new Result();
            var result = await _context.Catagories.ReplaceOneAsync(x => x.Id.Equals(entity.Id), _mapper.Map<Category>(entity));
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
