using FluentAssertions;
using MRHE.Datasources.Users;
using MRHE.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRHE.Helpers
{
    public static class EnvironmentHelper
    {
        public static User CurrentUser = new User();

        public static string WebsiteRootPageUrl => _stage switch
        {

            Stage.Test => "https://www.saucedemo.com/",
            Stage.Production => "https://www.saucedemo.com/",

            _ => "UNKNOWN",
        };       

        private static Stage _stage = Stage.Unknown;
        public static Stage CurrentStage => _stage;
        public static void SetStage(Stage stage) => _stage = stage;

        public static string GetSeleniumDriver()
            => Environment.GetEnvironmentVariable("SELENIUM_DRIVER");

        public static TimeSpan? GetTestWaitingTimeout()
        {
            var secondsSrt = Environment.GetEnvironmentVariable("TEST_RENDER_TIMEOUT");
            if (string.IsNullOrEmpty(secondsSrt)) return null;
            else return TimeSpan.FromSeconds(Convert.ToInt32(secondsSrt));
        }

        public static void NavigateAndAssertUrl(IWebDriver _webDriver, string url)
        {
            _webDriver.Navigate().GoToUrl(url);
            AssertUrl(_webDriver, url);
        }

        public static void AssertUrl(IWebDriver _webDriver, string url)
        {
            _webDriver.WaitUntilPageLoaded(url);
            _webDriver.Url.Should().StartWith(url);
        }

        public static void SetUserCreditioanls(IWebDriver _webDriver)
        {

            IWebElement Username_element = _webDriver.FindElement(CustomerLoginPage.login_credentials_Div);
            string Username = Username_element.Text.Split('\n')[1];
            IWebElement Password_element = _webDriver.FindElement(CustomerLoginPage.password_credentials_Div);
            string password = Password_element.Text.Split('\n')[1];
            CurrentUser.Username = Username;
            CurrentUser.Password =password;
        }

        public static void Scroll(IWebDriver _webDriver)
        {
            WebDriverWait _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);
            _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("container")));
            var js = (IJavaScriptExecutor)_webDriver;
           
            js.ExecuteScript("document.querySelector('#container').scrollIntoView();window.scrollTo(0, 500)");
        }

  

    }

    public enum Stage
    {
        Unknown,
        Test = 1,
        Staging = 2,
        Production = 3,

    }

}
