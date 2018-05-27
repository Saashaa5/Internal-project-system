using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SeleniumTest.PageObject
{
    public class Project : Base
    {
        [FindsBy(How = How.Id, Using = "input_13")]
        public IWebElement ElementInputNameBox;

        [FindsBy(How = How.Id, Using = "input_15")]
        public IWebElement ElementInputStartDateBox;

        [FindsBy(How = How.Id, Using = "input_17")]
        public IWebElement ElementInputEndDateBox;

        [FindsBy(How = How.Id, Using = "input_18")]
        public IWebElement ElementInputPriorityBox;

        [FindsBy(How = How.Id, Using = "input_19")]
        public IWebElement ElementInputCommentBox;

        [FindsBy(How = How.Id, Using = "select_20")]
        public IWebElement ElementInputClientCompanyBox;

        [FindsBy(How = How.Id, Using = "select_22")]
        public IWebElement ElementInputExecutorCompanyBox;

        [FindsBy(How = How.Id, Using = "addProjectButton")]
        public IWebElement ElementAddProjectButton;
        public Project(IWebDriver _driver) : base(_driver)
        {
        }
    }
}
