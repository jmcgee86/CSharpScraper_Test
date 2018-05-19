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
            //options.AddArguments("--headless");

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
                
                var result = driver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody[2]/tr/td[1]/span/span/a").Text;

                File.WriteAllText("/Users/jmcgee/Documents/CSharpScraper/result.txt", result);
            }
        }
    }
}
