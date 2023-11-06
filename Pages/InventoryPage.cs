using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRHE.Pages
{
    class InventoryPage
    {
        public static By AddtoCartButton = By.XPath("//div[@class='inventory_item']//button[@name='add-to-cart-sauce-labs-bike-light']");
        public static By CartCountBadge = By.XPath("//div[@class='shopping_cart_container']//span[@class='shopping_cart_badge']");

    }
}
