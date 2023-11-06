using OpenQA.Selenium;


namespace MRHE.Steps.Helpers
{
    public static class LoginHelper
    {
        
        public static void Login(IWebDriver driver)
        {
            LoginSteps.Login( driver);

        }

        }
    }