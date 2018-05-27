using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SeleniumTest.PageObject
{
    public class Company : Base
    {
        [FindsBy(How = How.Id, Using = "input_3")]
        public IWebElement ElementInputBox;

        [FindsBy(How = How.Id, Using = "AddButton")]
        public IWebElement ElementAddButton;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'company-class']/div")]
        public List<IWebElement> ElementCompanyNames;
        
        public Company(IWebDriver _driver) : base(_driver)
        {
        }

        public void AddCompany(string company)
        {
            ElementInputBox = Driver.WaitElement(By.Id("input_3"));
            ElementAddButton = Driver.WaitElement(By.Id("AddButton"));
            ElementInputBox.SendKeys(company);
            ElementAddButton.Click();
            
            Thread.Sleep(2000);
        }

        public void DeleteCompany(string companyName)
        {
            var deleteButton = Driver.WaitElement(By.Id(companyName));
            deleteButton.Click();
        }

        public List<IWebElement> GetCompanies()
        {
            ElementCompanyNames = Driver.WaitElements(By.XPath("//div[contains(@class, 'company-class')]/div"));

            return ElementCompanyNames;
        }
    }
}
