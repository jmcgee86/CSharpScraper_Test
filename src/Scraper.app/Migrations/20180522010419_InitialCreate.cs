using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scraper.app.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portfolio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DatePulled = table.Column<DateTime>(nullable: false),
                    NetWorth = table.Column<double>(nullable: false),
                    DayGain = table.Column<double>(nullable: false),
                    DayGainPercentage = table.Column<double>(nullable: false),
                    TotalGain = table.Column<double>(nullable: false),
                    TotalGainPercentage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StocksId = table.Column<int>(nullable: false),
                    StockSymbol = table.Column<string>(nullable: true),
                    CurrentPrice = table.Column<double>(nullable: false),
                    PriceChange = table.Column<double>(nullable: false),
                    PriceChangePercentage = table.Column<double>(nullable: false),
                    Shares = table.Column<double>(nullable: false),
                    CostBasis = table.Column<double>(nullable: false),
                    MarketValue = table.Column<double>(nullable: false),
                    DayGain = table.Column<double>(nullable: false),
                    DayGainPercentage = table.Column<double>(nullable: false),
                    TotalGain = table.Column<double>(nullable: false),
                    TotalGainPercentage = table.Column<double>(nullable: false),
                    Lots = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PortfolioInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Portfolio_PortfolioInfoId",
                        column: x => x.PortfolioInfoId,
                        principalTable: "Portfolio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_PortfolioInfoId",
                table: "Stocks",
                column: "PortfolioInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Portfolio");
        }
    }
}
