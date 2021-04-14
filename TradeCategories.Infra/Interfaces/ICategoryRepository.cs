using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeCategories.Domain;
using TradeCategories.Domain.Enums;

namespace TradeCategories.Infra.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<List<Category>> SearchByName(string name);
        Task<List<Category>> GetBySector(ESectorClient sectorClient);
       
    }
}
