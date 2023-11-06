using MRHE.Datasources;
using MRHE.Helpers;
using System;
using System.IO;

namespace MRHE.Pages
{
    public class PageUrls
    {
        public static string StartPage => Combine("");
        public static string CustomerLoginPage => Combine("");
        public static string InventoryPage => Combine("inventory.html");
        public static string CartPage => Combine("cart.html");
        public static string CheckoutStepOne => Combine("checkout-step-one.html");
        public static string CheckoutStepTwo => Combine("checkout-step-two.html");


        private static string Combine(string url)
            => Path.Combine(EnvironmentHelper.WebsiteRootPageUrl, url);
    }
}
