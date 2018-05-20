using System;
using System.Collections.Generic;
namespace Scraper.app
{
    public class PortfolioInfo
    {
        public DateTime DatePulled { get; set; }
        public Double NetWorth { get; set; }
        public Double DayGain{ get; set; }
        public Double DayGainPercentage{ get; set; }
        public Double TotalGain{ get; set; }
        public Double TotalGainPercentage{ get; set; }

        public virtual List<StockInfo> Stocks { get; set; }
        public int Id { get; set; }

        
    }
}