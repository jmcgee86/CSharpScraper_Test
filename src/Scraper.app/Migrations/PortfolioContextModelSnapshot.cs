﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Scraper.app;

namespace Scraper.app.Migrations
{
    [DbContext(typeof(PortfolioContext))]
    partial class PortfolioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rc1-32029");

            modelBuilder.Entity("Scraper.app.PortfolioInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatePulled");

                    b.Property<double>("DayGain");

                    b.Property<double>("DayGainPercentage");

                    b.Property<double>("NetWorth");

                    b.Property<double>("TotalGain");

                    b.Property<double>("TotalGainPercentage");

                    b.HasKey("Id");

                    b.ToTable("Portfolio");
                });

            modelBuilder.Entity("Scraper.app.StockInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CostBasis");

                    b.Property<double>("CurrentPrice");

                    b.Property<double>("DayGain");

                    b.Property<double>("DayGainPercentage");

                    b.Property<int>("Lots");

                    b.Property<double>("MarketValue");

                    b.Property<string>("Notes");

                    b.Property<int?>("PortfolioInfoId");

                    b.Property<double>("PriceChange");

                    b.Property<double>("PriceChangePercentage");

                    b.Property<double>("Shares");

                    b.Property<string>("StockSymbol");

                    b.Property<int>("StocksId");

                    b.Property<double>("TotalGain");

                    b.Property<double>("TotalGainPercentage");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioInfoId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("Scraper.app.StockInfo", b =>
                {
                    b.HasOne("Scraper.app.PortfolioInfo", "PortfolioInfo")
                        .WithMany("Stocks")
                        .HasForeignKey("PortfolioInfoId");
                });
#pragma warning restore 612, 618
        }
    }
}
