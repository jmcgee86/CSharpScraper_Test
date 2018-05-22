using Microsoft.EntityFrameworkCore;

namespace Scraper.app
{
    public class PortfolioContext : DbContext
    {
        public DbSet<PortfolioInfo> Portfolio { get; set;}
        public DbSet<StockInfo> Stocks {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlite("Data Source=Finance.db");
        }

    }
}