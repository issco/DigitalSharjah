using MRHE.Datasources;
using MRHE.Helpers;
using MRHE.Steps;
using MRHE.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using MRHE.Steps.Helpers;

namespace MRHE.Hooks
{
    [Binding]
    public class LoginHooks
    {


        [BeforeScenario("Authorized_Customer")]
        public static void LoginCustomer(ScenarioContext scenarioContext)
        {
            //var customer = UsersData
            //    .SearchUsers(EnvironmentHelper.CurrentStage, UserRole.Customer, 1)
            //    .First();
            LoginHelper.Login(DriverManager.WebDriver);
        }

    }
}