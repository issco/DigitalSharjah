using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;


namespace MRHE.Helpers
{
    public static class DriverExtensions
    {
        public static readonly TimeSpan _defaultTimeout = EnvironmentHelper.GetTestWaitingTimeout() ?? TimeSpan.FromSeconds(80);

        public static WebDriverWait Wait(this IWebDriver driver, TimeSpan? timeout = null)
            => new WebDriverWait(driver, timeout ?? _defaultTimeout);

        public static void WaitForComponents(this IWebDriver driver, TimeSpan? timeout = null)
        {
            driver
                .Wait(timeout)
                .Until(d => ((IJavaScriptExecutor)driver).ExecuteScript("return window.document.hasHomeMounted"));
        }

        public static bool PageLoadingComplete(this IWebDriver driver)
        {
            var jsExecuter = driver.JavaScriptExecutor();
            var keys = new[] { "loaded", "complete" };
            return keys.Contains(jsExecuter.ExecuteScript("return document.readyState"));
        }

        public static void WaitUntilPageLoaded(this IWebDriver driver, string pageUrl)
        {
            driver
                .Wait()
                .Until(d => d.PageLoadingComplete() && d.Url.StartsWith(pageUrl));
        }
    }
}