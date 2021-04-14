using System;
using System.Collections.Generic;
using System.Text;
using TradeCategories.Domain.Enums;

namespace TradeCategories.Services.DTO
{
    public class TraderDTO
    {

        public decimal Value { get; set; }
        public ESectorClient SectorClient { get; set; }

        public TraderDTO()
        {

        }

        public TraderDTO(decimal value, ESectorClient sectorClient)
        {
            Value = value;
            SectorClient = sectorClient;
        }


    }
}
