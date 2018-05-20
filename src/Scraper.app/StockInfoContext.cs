using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Scraper.app
{

    public class StockInfoContext : DbContext
    {
        public DbSet<StockInfo> Stocks { get; set;}
        public DbSet<PortfolioInfo> Portfolio { get; set;}

        //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     //optionsBuilder.UseSqlServer(@"Server=(localhost)\mssqllocaldb;Database=TutorialDB;Trusted_Connection=True;");
        //    optionsBuilder.UseSqlServer(@"Server=localhost\default;Database=StockInfo;User Id=sa;Password=myPassw0rd");
        //    //optionsBuilder.UseSqlServer(@"Server=localhost;Database=StockInfo;Trusted_Connection=True");

        // }
    }
}