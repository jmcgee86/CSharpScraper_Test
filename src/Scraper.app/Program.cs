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
            //returns day gain and day gain percentage in string, need to split and parse before adding data 
                //ex: -10,029.00 (-0.64%)
            string[] dayGain = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/header/div/div[1]/div/div[2]/p[2]/span")).Text.Split(" ");
            // returns total gain and totoal gain percentage in string, need to split and parse before adding data
                //ex: -10,029.00 (-0.64%)
            string [] totalGain = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/header/div/div[1]/div/div[2]/p[3]/span")).Text.Split(" ");
           
            snapshot.NetWorth = double.Parse(netWorth, NumberStyles.Currency);
            snapshot.DatePulled = DateTime.Now;
            snapshot.DayGain = double.Parse(dayGain[0]);
            snapshot.DayGainPercentage = double.Parse(dayGain[1].TrimStart(new char []{' ', '('}).TrimEnd(new char [] {'%', ' ', ')'}))/100;
            snapshot.TotalGain = double.Parse(totalGain[0]);
            snapshot.TotalGainPercentage = double.Parse(totalGain[1].TrimStart(new char []{' ', '('}).TrimEnd(new char [] {'%', ' ', ')'}))/100;

            //List<StockInfo> stockDataList = new List<StockInfo>();

            //var stockInfo = new StockInfo();

            //stockDataList.Add(stockInfo); // at end of each iteration through the data table, add info for individual stock to list

            //snapshot.Stocks = stockDataList; //after all iterations, add data to snapshot before adding to db

                			// xpath of html table
			var elemTable =	driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table"));

			// Fetch all Row of the table
			List<IWebElement> lstTrElem = new List<IWebElement>(elemTable.FindElements(By.TagName("tr")));
			String strRowData = "";
            List<string> rowData = new List<string>();

			// Traverse each row
			foreach (var elemTr in lstTrElem)
			{
				// Fetch the columns from a particuler row
				List<IWebElement> lstTdElem = new List<IWebElement>(elemTr.FindElements(By.TagName("td")));
				if (lstTdElem.Count > 0)
				{
					// Traverse each column
					foreach (var elemTd in lstTdElem)
					{
						// "\t\t" is used for Tab Space between two Text
						//strRowData = strRowData + elemTd.Text + "\t\t";
                        rowData.Add(elemTd.Text);
                        //File.WriteAllText("/Users/jmcgee/Documents/CSharpScraper/result.txt", strRowData + "\n");
                        // File.AppendAllText("/Users/jmcgee/Documents/CSharpScraper/result.txt", strRowData);

					}
				}
				// else
				// {
				// 	// To print the data into the console
				// 	Console.WriteLine("This is the end");
				// 	//Console.WriteLine(lstTrElem[0].Text.Replace(" ", "\t\t"));
				// }
				//Console.WriteLine(strRowData);
				strRowData = String.Empty;
                foreach (var col in rowData)
                {
                File.AppendAllText("/Users/jmcgee/Documents/CSharpScraper/result.txt", col + "\n");
                }
                File.AppendAllText("/Users/jmcgee/Documents/CSharpScraper/result.txt", "\n"+"\n");
                rowData.Clear();
			}
			// Console.WriteLine("");
			driver.Quit();


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

