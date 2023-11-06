using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using MRHE.Helpers.Enums;



namespace MRHE.Helpers
{
	internal static class WaitUtils
	{
		public static void waitForPage(IWait<IWebDriver> wait)
		{
			
			wait.Until(d => ((IJavaScriptExecutor) d).ExecuteScript("return document.readyState").Equals("complete"));
		}

		public static void waitForPage(IWait<IWebDriver> wait, string url)
		{
			wait.Until(d => d.Url == url);
			waitForPage(wait);
		}

		
		public static bool elementState(IWait<IWebDriver> wait, By elementPath, ElementState status) =>
			waitForElementRunner(wait, null, elementPath, status);

		
		public static bool elementState(IWait<IWebDriver> wait, IWebElement parentElement, By elementPath, ElementState status) =>
			waitForElementRunner(wait, parentElement, elementPath, status);

		private static bool waitForElementRunner(IWait<IWebDriver> wait, IWebElement parentElement, By elementPath,
			ElementState status)
		{
			try
			{
				waitForElement(wait, parentElement, elementPath, status);
				return true;
			}
			catch (NoSuchElementException)
			{
				return false;
			}
		}

		private static void waitForElement(IWait<IWebDriver> wait, IWebElement parentElement, By elementPath, ElementState status)
		{
			switch (status)
			{
				case ElementState.VISIBLE:
					wait.Until(driver =>
					{
						try
						{
							return parentElement != null
								? parentElement.FindElement(elementPath).Displayed
								: driver.FindElement(elementPath).Displayed;
						}
						catch (NoSuchElementException)
						{
							return false;
						}
					});
					break;
		
				case ElementState.NOT_VISIBLE:
					wait.Until(driver =>
					{
						try
						{
							return parentElement != null
								? !parentElement.FindElement(elementPath).Displayed
								: !driver.FindElement(elementPath).Displayed;
						}
						catch (NoSuchElementException)
						{
							return true;
						}
					});
					break;
			
				case ElementState.EXISTS:
					wait.Until(driver =>
					{
						try
						{
							var element = parentElement != null
								? parentElement.FindElement(elementPath)
								: driver.FindElement(elementPath);

							return true;
						}
						catch (NoSuchElementException)
						{
							return false;
						}
					});
					break;
		
				case ElementState.NOT_EXIST:
					wait.Until(driver =>
					{
						var originalImplicitWait = driver.Manage().Timeouts().ImplicitWait;
						driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
						try
						{
							var element = parentElement != null
								? parentElement.FindElement(elementPath)
								: driver.FindElement(elementPath);

							return false;
						}
						catch (NoSuchElementException)
						{
							return true;
						}
						finally
						{
							driver.Manage().Timeouts().ImplicitWait = originalImplicitWait;
						}
					});
					break;

				default:
					throw new Exception($" '{status}' is not a valid status to wait for element");
			}
		}

		public static void removeIFrameFromCode(IWebDriver driver, string iframeId)
		{
			try
			{
				// Locate the iframe element
				IWebElement iframeElement = driver.FindElement(By.Id(iframeId));

				// Execute JavaScript to remove the iframe from the DOM
				IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
				js.ExecuteScript("arguments[0].remove();", iframeElement);
			}
            catch
            {
				// already removed,
            }
		}

	}
}