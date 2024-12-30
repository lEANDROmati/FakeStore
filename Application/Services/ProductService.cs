using System;
using System.Threading;
using Application.Models.Categories;
using Application.Models.Products;
using Application.Services.Intefaces;
using Domain.Entities;
using Domain.Infrastructure.Data;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Application.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
       
        private readonly DataContext _Context;

        public ProductService(IProductRepository productRepository ,DataContext context )
        {
            _productRepository = productRepository;
            
            _Context = context;
        }

        public async Task<IEnumerable<ProductResponseModel>> GetAll()
        {
            
          

            var Producto = await (from t in _Context.Products
                                  select new ProductResponseModel
                                  {
                                      Id = t.Id,
                                      Name = t.Name,
                                      Price = t.Price,
                                      Description = t.Description,
                                      Stock = t.Stock,
                                      CategoryId = (from c in _Context.Category
                                                    where c.Id == t.CategoryId
                                                    select new CategoryResponseModel
                                                    {
                                                        Id = c.Id,
                                                        Name = c.Name,
                                                        Description = c.Description
                                                    }).ToList(),

                                  }).ToListAsync();
            return Producto;
        }


        public async Task<ProductResponseModel> GetById(int id)
        {
            var Product = await (from t in _Context.Products
                               where t.Id == id
                               select new ProductResponseModel
                               {
                                   Id = t.Id,
                                   Name = t.Name,
                                   Price = t.Price,
                                   Description = t.Description,
                                   Stock = t.Stock,
                                   CategoryId = (from c in _Context.Category
                                                 where t.CategoryId == c.Id
                                                select new CategoryResponseModel
                                                {
                                                    Id = c.Id,
                                                    Name = c.Name,
                                                    Description = c.Description
                                                }).ToList(),

                                }).FirstOrDefaultAsync();

            return Product;
        }

        public async Task Add(ProductRequestModel entity)
        {
            var product = new ProductEntity()
            {
                
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                Stock = entity.Stock,
                CategoryId = entity.CategoryId

            };
            await _productRepository.AddAsync(product);
            _Context.SaveChanges();
        }


        public async Task Update(ProductRequestModel entity, int id )
        {
            var product = new ProductEntity()
            {
                Id = id,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                Stock = entity.Stock,
                CategoryId = entity.CategoryId

            };
          await  _productRepository.UpdateAsync(product);
            await _Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _productRepository.DeleteAsync(id);
            await _Context.SaveChangesAsync();

        }
    }
}
