using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AttentiTests
{
    public class SeleniumProcesses
    {
        private IWebDriver driver = null;

        private static SeleniumProcesses instance = null;

        private SeleniumProcesses() {}

        public static SeleniumProcesses Instance
        {
            get
            {
                if(instance == null)
                    instance = new SeleniumProcesses();
                return instance;
            }
        }

        public void SetWebDriver(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void NavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        private void WaitExists(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
        }

        private void WaitClickable(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.PollingInterval = TimeSpan.FromSeconds(1);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }

        public void SetTextBoxValue(By by, string value)
        {
            WaitExists(by);
            WaitClickable(by);
            IWebElement element = driver.FindElement(by);
            element.Clear();
            element.SendKeys(value);
            element.SendKeys("\n");
        }

        public string GetText(By by)
        {
            WaitExists(by);
            IWebElement element = driver.FindElement(by);
            return element.Text;
        }

        public void Click(By by)
        {
            WaitExists(by);
            WaitClickable(by);
            IWebElement element = driver.FindElement(by);
            element.Click();
        }

        public void SelectFromDropDown(By by, string value)
        {
             WaitExists(by);
             WaitClickable(by);
             IWebElement element = driver.FindElement(by);
             SelectElement selectElement  = new SelectElement(element);

             selectElement.SelectByText(value);
        }
    }
}