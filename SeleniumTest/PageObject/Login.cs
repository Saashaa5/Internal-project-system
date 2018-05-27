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
    public class Login : Base
    {
        [FindsBy(How = How.Id, Using = "Login")]
        public IWebElement ElementLoginBox;

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement ElementPasswordBox;

        [FindsBy(How = How.Id, Using = "LogInBtn")]
        public IWebElement ElementLogInButton;

        public Login(IWebDriver driver) : base(driver){ }

        public void LoginAs(string login, string password)
        {
            //login
            ElementLoginBox = Driver.WaitElement(By.Id("Login"));
            ElementPasswordBox = Driver.WaitElement(By.Id("Password"));
            ElementLogInButton = Driver.WaitElement(By.Id("LogInBtn"));

            ElementLoginBox.SendKeys(login);
            ElementPasswordBox.SendKeys(password);
            ElementLogInButton.Submit();
            
            Thread.Sleep(2000);
        }
    }
}
