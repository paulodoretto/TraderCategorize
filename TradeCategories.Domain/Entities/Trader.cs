using System;
using System.Collections.Generic;
using System.Text;
using TradeCategories.Domain.Enums;

namespace TradeCategories.Domain
{
    public class Trader
    {
        public Trader(decimal value, ESectorClient sectorClient)
        {
            Value = value;
            SectorClient = sectorClient;
        }

        public decimal Value { get; set; }
        public ESectorClient SectorClient { get; private set; }

    }
}
