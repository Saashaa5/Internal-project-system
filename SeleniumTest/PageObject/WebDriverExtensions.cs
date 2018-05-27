using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest.PageObject
{
    public static class WebDriverExtensions
    {
        public static IWebElement WaitElement(this IWebDriver driver, By by, int timeoutInSeconds = 30)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(x => driver.FindElement(by));
        }

        public static List<IWebElement> WaitElements(this IWebDriver driver, By by, int timeoutInSeconds = 30)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(x => driver.FindElements(by)).ToList();
        }

        public static bool WaitElementVisible(this IWebDriver driver, IWebElement webElement, int timeoutInSeconds = 30)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(x => webElement.Displayed);
        }
    }
}
