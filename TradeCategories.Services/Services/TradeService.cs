using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeCategories.Infra.Interfaces;
using TradeCategories.Services.DTO;
using TradeCategories.Services.Interfaces;

namespace TradeCategories.Services.Services
{
    public class TradeService : ITradeService
    {
        private readonly IMapper _mapper;

        private readonly ICategoryRepository _categoryRepository;

        public TradeService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        public async Task<List<string>> Categorize(List<TraderDTO> trades)
        {

            List<string> list = new List<string>();
            var allCategories = await _categoryRepository.Get();
            var mapperCategories = _mapper.Map<List<CategoryDTO>>(allCategories);

            foreach (TraderDTO trade in trades)
            {

                foreach (CategoryDTO category in mapperCategories)
                {
                                    if (trade.Value >= category.ValueInitial && trade.Value <= category.ValueFinal && trade.SectorClient == category.SectorClient)
                                    {
                                        list.Add(category.Name);
                                    }
                }
           }


            return list;
        }
    }
}
