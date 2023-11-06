using OpenQA.Selenium;
namespace MRHE.Pages
{
     class CheckoutPage
    {
        public static By CheckoutButton = By.Id("checkout");
        public static By FirstNameInput = By.Id("first-name");
        public static By LastNameInput = By.Id("last-name");
        public static By PostalCodeInput = By.Id("postal-code");
        public static By CheckoutContinueButton = By.Id("continue");
        public static By CheckoutFinishButton = By.Id("finish");
        public static By SuccessMessage = By.XPath("//h2[@class='complete-header']");
    }
}
