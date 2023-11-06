using OpenQA.Selenium;
using System;

namespace MRHE.Helpers
{
    public static class DriverManager
    {
        private static IWebDriver _driver;
        public static IWebDriver WebDriver
        {
            get
            {
                if(_driver is null)
                {
                    _driver = DriverSelector.SelectDriverFromEnvironment();
                    _driver.Manage().Window.Maximize();
                }

                return _driver;
            }
        }

        public static IJavaScriptExecutor JavaScriptExecutor(this IWebDriver webDriver)
            => (IJavaScriptExecutor)webDriver;

        public static void Cleanup()
        {
            try
            {
                if (_driver is null)
                    return;

                //driver.Quit();// quit does not close the window
                _driver.Close();
                _driver.Dispose();
                _driver = null;
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
    }
}
