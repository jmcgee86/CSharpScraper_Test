using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Scraper.app
{
    class Program
    {
        static void Main(string[] args)
        {
            var snapshot = new PortfolioInfo();

            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--incognito");
           // options.AddArguments("--headless");

            using (var driver = new ChromeDriver("bin/Debug/netcoreapp2.0/", options))
            {
                var keys = new Keys();
                driver.Navigate().GoToUrl("https://login.yahoo.com/config/login?.intl=us&.lang=en-US&.src=finance&.done=https%3A%2F%2Ffinance.yahoo.com%2F");
                var userNameField = driver.FindElementById("login-username");
                var userName = keys.Email;
                userNameField.SendKeys(userName);

                var nextButton = driver.FindElementById("login-signin");
                nextButton.Click();

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

                var loginAvailable = wait.Until(d => d.FindElement(By.Id("login-passwd")));

                var userPasswordField = driver.FindElementById("login-passwd");
                var password = keys.Password;
                userPasswordField.SendKeys(password);

                var loginButton = driver.FindElementById("login-signin");
                loginButton.Click();

               driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v2");
                                    
                wait.Until(d => d.FindElement(By.Id("__dialog")));

                var closePopup = driver.FindElement(By.XPath("//dialog[@id = '__dialog']/section/button"));
                closePopup.Click();


            var netWorth = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/header/div/div[1]/div/div[2]/p[1]")).Text;
            string[] dayGain = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/header/div/div[1]/div/div[2]/p[2]/span")).Text.Split(" ");
            string [] totalGain = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/header/div/div[1]/div/div[2]/p[3]/span")).Text.Split(" ");
            snapshot.NetWorth = double.Parse(netWorth, NumberStyles.Currency);
            snapshot.DatePulled = DateTime.Now;
            snapshot.DayGain = double.Parse(dayGain[0]);
            snapshot.DayGainPercentage = double.Parse(dayGain[1].TrimStart(new char []{' ', '('}).TrimEnd(new char [] {'%', ' ', ')'}))/100;
            snapshot.TotalGain = double.Parse(totalGain[0]);
            snapshot.TotalGainPercentage = double.Parse(totalGain[1].TrimStart(new char []{' ', '('}).TrimEnd(new char [] {'%', ' ', ')'}))/100;

            List<StockInfo> stockDataList = new List<StockInfo>();

            // xpath of html table
			var table =	driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table"));

			// Fetch all Rows of the table
			List<IWebElement> tableRows = new List<IWebElement>(table.FindElements(By.TagName("tr")));
            List<string> rowDataList = new List<string>();
			// Traverse each row
			foreach (var row in tableRows)
			{
				// Fetch the columns from a particuler row
				List<IWebElement> colsInRow = new List<IWebElement>(row.FindElements(By.TagName("td")));
				if (colsInRow.Count > 0)
				{
					// Traverse each column and add to rowDataList
					foreach (var col in colsInRow)
					{
                        rowDataList.Add(col.Text);
					}
                
                string [] colOne = rowDataList[0].ToString().Split("\n");
                string [] colTwo = rowDataList[1].ToString().Split("\n");
                string [] colSix = rowDataList[5].ToString().Split("\n");
                string [] colSeven = rowDataList[6].ToString().Split("\n");
                string [] colEight = rowDataList[7].ToString().Split(" ");

                stockDataList.Add(new StockInfo()
                {
                    StockSymbol = colOne[0].ToString(),
                    CurrentPrice = double.Parse(colOne[1].ToString()),
                    PriceChange = double.Parse(colTwo[1]),
                    PriceChangePercentage = double.Parse((colTwo[0]).TrimEnd(new char [] {'%', ' ', ')'}))/100,
                    Shares = double.Parse(rowDataList[2].ToString()),
                    CostBasis = double.Parse(rowDataList[3].ToString()),
                    MarketValue = double.Parse(rowDataList[4].ToString()),
                    DayGain = double.Parse(colSix[1]),
                    DayGainPercentage = double.Parse((colSix[0]).TrimEnd(new char [] {'%', ' ', ')'}))/100,
                    TotalGain = double.Parse(colSeven[1]),
                    TotalGainPercentage = double.Parse((colSeven[0]).TrimEnd(new char [] {'%', ' ', ')'}))/100,
                    Lots = int.Parse(colEight[0]),
                    Notes = rowDataList[8]
                });
                rowDataList.Clear();
				}
			}
			driver.Quit();

        snapshot.Stocks = stockDataList;

            using (var db = new PortfolioContext())
            {
                db.Add(snapshot);
                var count = db.SaveChanges();
                foreach (var portfolio in db.Portfolio)
                {
                    System.Console.WriteLine(portfolio.NetWorth);
                }
            }
		}
	}
}
                






               // File.WriteAllText("/Users/jmcgee/Documents/CSharpScraper/result.txt", result);




        //         using (var db = new StockInfoContext()) 
        // { 
        //     // Create and save a new Blog 
        //     //Console.Write("Enter a name for a new Blog: "); 
        //     //var name = Console.ReadLine(); 
 
        //     var snapshot = new StockInfo(); 
        //     db.Stocks.Add(snapshot); 
        //     db.SaveChanges(); 
 
        //     // Display all Blogs from the database 
        //     var query = from b in db.Stocks 
        //                 orderby b.Id
        //                 select b; 
 
        //     Console.WriteLine("All stocks in the database:"); 
        //     foreach (var item in query) 
        //     { 
        //         Console.WriteLine(item.Id); 
        //     } 
 
        //     Console.WriteLine("Press any key to exit..."); 
        //     Console.ReadKey(); 
        //     }
}