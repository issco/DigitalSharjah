using System;
using OpenQA.Selenium;

namespace MRHE.Pages
{
      class CustomerLoginPage
    {
        
        
        public static By UsernameInput = By.Id("user-name");
        public static By PasswordInput = By.Id("password");
        public static By LoginButton = By.Id("login-button");
        public static By login_credentials_Div = By.Id("login_credentials");
        public static By password_credentials_Div = By.XPath("//div[@class='login_password']");

       

    }
}

