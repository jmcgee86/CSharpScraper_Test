using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Text;
using System.Drawing;

namespace Scraper.app
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--incognito");

            using (var driver = new ChromeDriver("/Users/jmcgee/Documents/CSharpScraper/src/Scraper.app/bin/Debug/netcoreapp2.0/", options))
            {
                var privateKeys = new PrivateKeys();
                driver.Navigate().GoToUrl("https://login.yahoo.com/config/login?.intl=us&.lang=en-US&.src=finance&.done=https%3A%2F%2Ffinance.yahoo.com%2F");
                var userNameField = driver.FindElementById("login-username");
                var userName = privateKeys.Email;
                userNameField.SendKeys(userName);

                var nextButton = driver.FindElementById("login-signin");
                nextButton.Click();

                var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("login-passwd")));

                var userPasswordField = driver.FindElementById("login-passwd");
                var password = privateKeys.Password;
                userPasswordField.SendKeys(password);

                var loginButton = driver.FindElementById("login-signin");
                loginButton.Click();

                var wait2 = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
                var element2 = wait2.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div/div/div[1]/div/div[3]/div[2]/div/div/div/div/div/div[3]/div/div/section/div/section[1]/table/tbody/tr/td[1]/a")));

               var result = driver.FindElementByXPath("/html/body/div[1]/div/div/div[1]/div/div[3]/div[2]/div/div/div/div/div/div[3]/div/div/section/div/section[1]/table/tbody/tr/td[1]/a").Text;
                File.WriteAllText("/Users/jmcgee/Documents/CSharpScraper/result.txt", result);
            }
        }
    }
}
