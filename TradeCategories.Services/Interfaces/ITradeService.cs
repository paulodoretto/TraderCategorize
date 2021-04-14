using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeCategories.Services.DTO;

namespace TradeCategories.Services.Interfaces
{
    public interface ITradeService
    {       
        Task<List<string>> Categorize(List<TraderDTO> trades);
    }
}
