using System;
using System.Collections.Generic;
using System.Text;
using TradeCategories.Domain.Enums;

namespace TradeCategories.Services.DTO
{
    public class CategoryDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal ValueInitial { get; set; }

        public decimal ValueFinal { get; set; }

        public ESectorClient SectorClient { get; set; }

        public CategoryDTO()
        {

        }

        public CategoryDTO(long id, string name, decimal valueInitial, decimal valueFinal, ESectorClient sectorClient)
        {
            Id = id;
            Name = name;
            ValueInitial = valueInitial;
            ValueFinal = valueFinal;
            SectorClient = sectorClient;
        }
    }
}
