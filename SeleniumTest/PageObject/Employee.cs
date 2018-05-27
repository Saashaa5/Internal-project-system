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
    public class Employee : Base
    {
        [FindsBy(How = How.Id, Using = "input_5")]
        public IWebElement ElementInputNameBox;

        [FindsBy(How = How.Id, Using = "input_6")]
        public IWebElement ElementInputSurnameBox;

        [FindsBy(How = How.Id, Using = "input_7")]
        public IWebElement ElementInputPatronymicBox;

        [FindsBy(How = How.Id, Using = "input_8")]
        public IWebElement ElementInputEmailBox;

        [FindsBy(How = How.Id, Using = "select_9")]
        public IWebElement ElementSelectCompanyBox;

        [FindsBy(How = How.Id, Using = "select_option_24")]
        public IWebElement ElementSelectCompanyYandexOption;

        [FindsBy(How = How.Id, Using = "addEmployeeButton")]
        public IWebElement ElementAddEmployeeButton;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'employee-class']/div")]
        public List<IWebElement> ElementEmployeeEmails;

        public Employee(IWebDriver _driver) : base(_driver)
        {
        }

        public void AddEmployee(string name, string surname, string patronymic, string email)
        {
            ElementInputNameBox = Driver.WaitElement(By.Id("input_5"));
            ElementInputSurnameBox = Driver.WaitElement(By.Id("input_6"));
            ElementInputPatronymicBox = Driver.WaitElement(By.Id("input_7"));
            ElementInputEmailBox = Driver.WaitElement(By.Id("input_8"));
            ElementSelectCompanyBox = Driver.WaitElement(By.Id("select_9"));
            ElementAddEmployeeButton = Driver.WaitElement(By.Id("addEmployeeButton"));
            
            ElementInputNameBox.SendKeys(name);
            ElementInputSurnameBox.SendKeys(surname);
            ElementInputPatronymicBox.SendKeys(patronymic);
            ElementInputEmailBox.SendKeys(email);
            ElementSelectCompanyBox.Click();

            Thread.Sleep(5000);

            ElementSelectCompanyYandexOption = Driver.WaitElement(By.Id("select_option_24"));
            //Driver.WaitElementVisible(ElementSelectCompanyYandexOption);
            ElementSelectCompanyYandexOption.Click();
            
            ElementAddEmployeeButton.Click();

            Thread.Sleep(1000);
        }

        public void DeleteEmployee(string employeeEmail)
        {
            var deleteButton = Driver.WaitElement(By.Id(employeeEmail));
            deleteButton.Click();
        }

        public List<IWebElement> GetEmployeeEmails()
        {
            ElementEmployeeEmails = Driver.WaitElements(By.XPath("//div[contains(@class, 'employee-class')]/div"));

            return ElementEmployeeEmails;
        }
    }
}
