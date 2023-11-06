using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;
using WebDriverManager.DriverConfigs.Impl;
using System.Threading.Tasks;
namespace MRHE.Helpers
{
    static class DriverSelector
    {
        private static bool _shouldUpdateDrivers = false;

        public static IWebDriver SelectDriverFromEnvironment()
        {
            return (EnvironmentHelper.GetSeleniumDriver()?.Trim()?.ToLower()) switch
            {
                "firefox_driver" => FirefoxDriver,
                "edge_driver" => EdgeDriver,
                "ie_driver" => InternetExplorerDriver,
                _ => ChromeDriver,
            };
        }

        private static string DriversDirectoryPath => Path.Combine(Directory.GetCurrentDirectory(), "Drivers");

        private readonly static PageLoadStrategy _pageLoadStrategy = PageLoadStrategy.Eager;
        private static FirefoxDriver FirefoxDriver
        {
            get
            {
                new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());

                var service = FirefoxDriverService.CreateDefaultService(DriversDirectoryPath);
                service.Host = "::1";

                var options = new FirefoxOptions
                {
                    PageLoadStrategy = _pageLoadStrategy,
                };

                return new FirefoxDriver(DriversDirectoryPath, options);
            }
        }
        private static EdgeDriver EdgeDriver
        {
            get
            {
                var options = new EdgeOptions
                {
                    PageLoadStrategy = _pageLoadStrategy,
                };

                return new EdgeDriver(DriversDirectoryPath, options);
            }
        }
        private static InternetExplorerDriver InternetExplorerDriver
        {
            get
            {
                var options = new InternetExplorerOptions
                {
                    PageLoadStrategy = _pageLoadStrategy,
                };

                return new InternetExplorerDriver(DriversDirectoryPath, options);
            }
        }
       
        private static ChromeDriver ChromeDriver
        {
            get
            {
                var options = new ChromeOptions
                {
                    PageLoadStrategy = _pageLoadStrategy,
                    
                };
                //options.AddArgument("no-sandbox");
                options.AddArguments("--start-maximized");
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                try
                {
                    return new ChromeDriver(DriversDirectoryPath, options);
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                        Console.WriteLine("Drivers need to be updated");
                        Task.Run(async () => await UpdateChromeDriversAsync()).Wait();
                }
                return new ChromeDriver(DriversDirectoryPath, options);
            }
        }

        static async Task UpdateChromeDriversAsync()
        {
            // Step 1: Create an instance of ChromeDriverUpdater
            ChromeDriverUpdater updater = new ChromeDriverUpdater();

            // Step 2: Call the UpdateChromeDriverAsync() method
            await updater.UpdateChromeDriverAsync();
        }
    }
}



