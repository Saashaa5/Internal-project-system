using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest.PageObject
{
    public class Base
    {
        public IWebDriver Driver;

        [FindsBy(How = How.ClassName, Using = "confirm")]
        public IWebElement ElementOKButton;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(), 'Сотрудники')]")]
        public IWebElement ElementEmployeeTab;

        public Base(IWebDriver _driver)
        {
            Driver = _driver;
        }

        public void WaitElement(IWebElement element, int timeout = 30)
        {
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, timeout));
            wait.Until(driver => element.Displayed);
        }

        public void ConfirmSuccess()
        {
            ElementOKButton = Driver.WaitElement(By.ClassName("confirm"));
            ElementOKButton.Click();
        }

        public void NavigateToEmployeeTab()
        {
            ElementEmployeeTab = Driver.WaitElement(By.XPath("//span[contains(text(), 'Сотрудники')]"));
            ElementEmployeeTab.Click();
        }
        
        public void EndTest()
        {
            Driver.Close();
        }
    }
}
