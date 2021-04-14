using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeCategories.Domain.Enums;
using TradeCategories.Services.DTO;

namespace TradeCategories.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDTO> Create(CategoryDTO categoryDTO);

        Task<CategoryDTO> Update(CategoryDTO categoryDTO);

        Task Remove(long id);

        Task<CategoryDTO> GetById(long id);

        Task<List<CategoryDTO>> Get();

        Task<List<CategoryDTO>> SearchByName(string name);

        Task<List<CategoryDTO>> GetBySector(ESectorClient sectorClient);
    }
}
