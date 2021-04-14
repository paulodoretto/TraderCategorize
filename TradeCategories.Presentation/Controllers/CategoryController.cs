using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeCategories.Core.Exceptions;
using TradeCategories.Services.DTO;
using TradeCategories.Services.Interfaces;

namespace TradeCategories.Presentation.Categories
{
    public class CategoryController 
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoryDTO> Create(CategoryDTO category)
        {

            try
            {
                var userCreated = await _categoryService.Create(category);


                return userCreated;
            }
            catch (DomainException EX)
            {

                return category;
            }
            catch (Exception EX)
            {
                return category;
            }

            
        }

        public async Task<CategoryDTO> Update(CategoryDTO category)
        {

            try
            {
                var userUpdate = await _categoryService.Update(category);


                return userUpdate;
            }
            catch (DomainException EX)
            {

                return category;
            }
            catch (Exception EX)
            {
                return category;
            }


        }

        public async Task Remove(long id)
        {

            try
            {
                await _categoryService.Remove(id);

                
            }
            catch (DomainException EX)
            {

                
            }
            catch (Exception EX)
            {
                
            }


        }

        public async Task<List<CategoryDTO>> Get()
        {
            try
            {
                var allcategories = await _categoryService.Get();

                return allcategories;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CategoryDTO> GetById(long id)
        {
            try
            {
                var category = await _categoryService.GetById(id);

                return category;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
