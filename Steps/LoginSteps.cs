using FluentAssertions;
using OpenQA.Selenium;
using MRHE.Pages;
using TechTalk.SpecFlow;
using MRHE.Helpers;
using OpenQA.Selenium.Support.UI;
using MRHE.Helpers.Enums;


namespace MRHE.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _webDriver;
        public string[] tags;
        private static WebDriverWait _webDriverWait;


        public LoginSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _webDriver = DriverManager.WebDriver;
            tags = _scenarioContext.ScenarioInfo.ScenarioAndFeatureTags;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);

        }


        [Then(@"the (.*) will be redirected to Login page")]
        public void ThenTheUserWillBeRedirectedToLoginPage(int Role)
        {
            _webDriver.Url.Should().StartWith("");
        }




        public static void Login(IWebDriver driver)
        {
            _webDriverWait = new WebDriverWait(driver, DriverExtensions._defaultTimeout);


            EnvironmentHelper.NavigateAndAssertUrl(driver, PageUrls.CustomerLoginPage);
            WaitUtils.elementState(_webDriverWait, CustomerLoginPage.UsernameInput, ElementState.VISIBLE);
            EnvironmentHelper.SetUserCreditioanls(driver);
            driver.FindElement(CustomerLoginPage.UsernameInput).SendKeys(EnvironmentHelper.CurrentUser.Username);
            driver.FindElement(CustomerLoginPage.PasswordInput).SendKeys(EnvironmentHelper.CurrentUser.Password);
            driver.FindElement(CustomerLoginPage.LoginButton).Click();
            EnvironmentHelper.AssertUrl(driver, PageUrls.InventoryPage);

        }

    }
}
