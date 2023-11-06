using TechTalk.SpecFlow;
using MRHE.Helpers;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using MRHE.Pages;
using MRHE.Helpers.Enums;
using NUnit.Framework;
using Newtonsoft.Json.Linq;


namespace MRHE.Steps.WEB.QA.Customer.HousingRequest
{

    [Binding]
    public class CheckoutSuccessSteps
    {

        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _webDriverWait;
        public JObject[] DocumentsPayload;
        public CheckoutSuccessSteps(ScenarioContext scenarioContext)
        {
 
            _webDriver = DriverManager.WebDriver;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);
        }


        [Given("I add the product \"(.*)\" to my cart")]
        public void WhenIAddTheProductToMyCart(string productName)
        {
            WaitUtils.elementState(_webDriverWait, InventoryPage.AddtoCartButton, ElementState.VISIBLE);

            _webDriver.FindElement(InventoryPage.AddtoCartButton).Click();
            Assert.AreNotEqual(_webDriver.FindElement(InventoryPage.CartCountBadge).Text, "0");
        }

        [Given("I navigate to the Cart")]
        public void WhenINavigateToTheCart()
        {
            // Implement your code to navigate to the cart
            EnvironmentHelper.NavigateAndAssertUrl(_webDriver, PageUrls.CartPage);

        }

        [Then("I click the Checkout button")]
        public void ThenIClickTheCheckoutButton()
        {
            // Implement your code to click the Checkout button
            _webDriver.FindElement(CheckoutPage.CheckoutButton).Click();
            EnvironmentHelper.AssertUrl(_webDriver, PageUrls.CheckoutStepOne);

        }

        [Then("I provide the required checkout data:")]
        public void WhenIProvideTheRequiredCheckoutData(Table table)
        {
            var firstRow = table.Rows[0];
            // Access individual values based on column names
            string firstName = firstRow["First Name"];
            string lastName = firstRow["Last Name"];
            string postalCode = firstRow["Postal Code"];

            _webDriver.FindElement(CheckoutPage.FirstNameInput).SendKeys(firstName);
            _webDriver.FindElement(CheckoutPage.LastNameInput).SendKeys(lastName);
            _webDriver.FindElement(CheckoutPage.PostalCodeInput).SendKeys(postalCode);
            _webDriver.FindElement(CheckoutPage.CheckoutContinueButton).Click();

        }

        [When("I click the Finish button")]
        public void WhenIClickTheFinishButton()
        {
            // Implement your code to click the Finish button
            EnvironmentHelper.AssertUrl(_webDriver, PageUrls.CheckoutStepTwo);
            _webDriver.FindElement(CheckoutPage.CheckoutFinishButton).Click();


        }

        [Then("I should see the message \"(.*)\"")]
        public void ThenIShouldSeeTheMessage(string expectedMessage)
        {
            // Implement your code to verify the displayed message
            IWebElement headerElement= _webDriver.FindElement(CheckoutPage.SuccessMessage);
            Assert.AreEqual(expectedMessage, headerElement.Text);

        }

    }
}
