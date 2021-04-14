using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeCategories.Services.DTO;
using TradeCategories.Services.Interfaces;

namespace TradeCategories.Presentation.Controllers
{
    public class TraderController
    {
        private readonly ITradeService _tradeService;

        public TraderController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        public async Task<List<string>> Categorize(List<TraderDTO> trades)
        {
            
            var allcategories = await _tradeService.Categorize(trades);

            return allcategories;
            
        }
    }
}
