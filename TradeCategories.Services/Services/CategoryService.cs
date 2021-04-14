using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeCategories.Core.Exceptions;
using TradeCategories.Domain;
using TradeCategories.Domain.Enums;
using TradeCategories.Infra.Interfaces;
using TradeCategories.Services.DTO;
using TradeCategories.Services.Interfaces;

namespace TradeCategories.Services.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly IMapper _mapper;

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Create(CategoryDTO categoryDTO)
        {
            var userExists = await _categoryRepository.SearchByName(categoryDTO.Name);

            if (userExists.Count > 0)
                throw new DomainException("Já existe uma categoria com esse nome");

            var category = _mapper.Map<Category>(categoryDTO);
            category.Validate();

            var categoryCreated = await _categoryRepository.Create(category);

            return _mapper.Map<CategoryDTO>(categoryCreated);

        }

        public async Task<CategoryDTO> Update(CategoryDTO categoryDTO)
        {
            var userExists = await _categoryRepository.GetById(categoryDTO.Id);

            if (userExists == null)
                throw new DomainException("Não existe nenhum usuario com o id informado");

            var category = _mapper.Map<Category>(categoryDTO);
            category.Validate();

            var categoryUpdated = await _categoryRepository.Update(category);

            return _mapper.Map<CategoryDTO>(categoryUpdated);
        }

        public async Task Remove(long id)
        {
            await _categoryRepository.Remove(id);
        }

        public async Task<List<CategoryDTO>> Get()
        {
            var allCategories = await _categoryRepository.Get();

            return _mapper.Map<List<CategoryDTO>>(allCategories);
        }

        public async Task<CategoryDTO> GetById(long id)
        {
            var category = await _categoryRepository.GetById(id);

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<List<CategoryDTO>> GetBySector(ESectorClient sectorClient)
        {
            var allCategories = await _categoryRepository.GetBySector(sectorClient);

            return _mapper.Map<List<CategoryDTO>>(allCategories);
        }
               

        public async Task<List<CategoryDTO>> SearchByName(string name)
        {
            var allCategories = await _categoryRepository.SearchByName(name);

            return _mapper.Map<List<CategoryDTO>>(allCategories);
        }

        
    }
}
