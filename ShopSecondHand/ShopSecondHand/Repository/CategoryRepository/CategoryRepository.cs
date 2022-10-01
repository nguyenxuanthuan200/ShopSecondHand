﻿using AutoMapper;
using ShopSecondHand.Data.RequestModels.CategoryRequest;
using ShopSecondHand.Data.ResponseModels.CategoryResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopSecondHand.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ShopSecondHand.Repository.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopSecondHandContext dbContext;
        private readonly IMapper _mapper;

        public CategoryRepository(ShopSecondHandContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest categoryRequest)
        {

            var category = await dbContext.Categories
                 .SingleOrDefaultAsync(p => p.Name.Equals(categoryRequest.Name));
            if (category != null)
                return null;

            Category categoryy = new Category();
            {
                categoryy.Id = new Guid();
                categoryy.Name = categoryRequest.Name;
            };
            var result = await dbContext.Categories.AddAsync(categoryy);
            await dbContext.SaveChangesAsync();
            var re = _mapper.Map<CreateCategoryResponse>(categoryy);
            return re;
        }

        public async void DeleteCategory(Guid id)
        {
            var deCate = dbContext.Categories
               .SingleOrDefault(p => p.Id == id);
            if (deCate != null)
            {
                dbContext.Categories.Remove(deCate);
                dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<GetCategoryResponse>> GetCategory()
        {
            var category = await dbContext.Categories.ToListAsync();
            IEnumerable<GetCategoryResponse> result = category.Select(
                x =>
                {
                    return new GetCategoryResponse()
                    {
                        Name = x.Name
                    };
                }
                ).ToList();
            return result;
        }

        public async Task<GetCategoryResponse> GetCategoryById(Guid id)
        {
            var getById = await dbContext.Categories

               .FirstOrDefaultAsync(p => p.Id == id);
            if (getById != null)
            {
                var re = new GetCategoryResponse()
                {
                    Name = getById.Name
                };
                return re;
            }
            return null;
        }

        public async Task<GetCategoryResponse> GetCategoryByName(string name)
        {
            var getById = await dbContext.Categories

             .FirstOrDefaultAsync(p => p.Equals(name));
            if (getById != null)
            {
                var re = new GetCategoryResponse()
                {
                    Name = getById.Name
                };
                return re;
            }
            return null;
        }

        public async Task<UpdateCategoryResponse> UpdateCategory(Guid id, UpdateCategoryRequest categoryRequest)
        {
            var upCategory = await dbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
            if (id != categoryRequest.Id) return null;
            if (upCategory == null) return null;

            upCategory.Name = categoryRequest.Name;
            dbContext.Categories.Update(upCategory);
            await dbContext.SaveChangesAsync();

            var up = _mapper.Map<UpdateCategoryResponse>(upCategory);
            return up;
        }
    }
}
