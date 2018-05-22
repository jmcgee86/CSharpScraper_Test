namespace Scraper.app
{
    public class StockInfo
    {
        public int StocksId { get; set;}

        public string StockSymbol { get; set; }
        public double CurrentPrice { get; set; }
        public double PriceChange { get; set; }
        public double PriceChangePercentage { get; set; }
        public double Shares { get; set; }
        public double CostBasis { get; set; }
        public double MarketValue { get; set; }
        public double DayGain { get; set; }
        public double DayGainPercentage { get; set; }
        public double TotalGain { get; set; }
        public double TotalGainPercentage { get; set; }
        public int Lots { get; set; }
        public string Notes { get; set; }
        public int Id { get; set; }
        public virtual PortfolioInfo PortfolioInfo { get; set;}

    }
}