using Application.Models.Categories;
using Application.Services.Intefaces;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryResponseModel>> GetAll()
        {
            var  categories = await _categoryRepository.GetAllAsync();
            var response = categories.Select(categories => new CategoryResponseModel
            {
                Id = categories.Id,
                Name = categories.Name,
                Description = categories.Description
            });
            return response;
        }

        public async Task<CategoryResponseModel> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return (null);
            }
            var response = new CategoryResponseModel
            {
                Id = id,
                Name = category.Name,
                Description = category.Description
            };

            return response;
        }

        public async Task Add(CategoryRequestModel entity)
        {
            var category = new CategoryEntity
            {
                Name = entity.Name,
                Description = entity.Description
            };
           await _categoryRepository.AddAsync(category);
           await _categoryRepository.SaveChangesAsync();
        }

        public async Task Update(CategoryRequestModel entity, int id)
        {
            var category = new CategoryEntity()
            {
                Id = id,
                Name = entity.Name,
                Description = entity.Description
            };
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _categoryRepository.DeleteAsync(id);
           await _categoryRepository.SaveChangesAsync();
        }

    }
}
