using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TestCodeChallenge.pom;

namespace TestCodeChallange.pom
{    
    class Base : APage
    {
        private IWebDriver _driver;  
        protected Config _config = new Config();
        protected IJavaScriptExecutor js;

        protected string _driver_path;
        protected string _json_config_path;
        protected int _timeout_ms;

        public Base(IWebDriver driver)
        {
            this._driver = driver;

            _driver_path = _config.GetConfigPath();
            _json_config_path = Consts._json_path;

            js = _driver as IJavaScriptExecutor;

            string strTimeout = _config.GetConfigItem(Consts._timeout_ms);
            if (!int.TryParse(strTimeout, out _timeout_ms))
            {
                _timeout_ms = 5000;
            }
        }

        public override IWebDriver DriverConn()
        {            
            _driver = new ChromeDriver(_driver_path);
            _driver.Manage().Window.Maximize();
            return _driver;
        }

        public override void DriverDown()
        {
            if (_driver != null)
            {
                _driver.Close();
                _driver.Quit();
            }
        }

        public override IWebElement FindElement(By locator)
        {
            WaitDriver();
            return _driver.FindElement(locator);
        }

        public override List<IWebElement> FindElements(By locator)
        {
            WaitDriver();
            return _driver.FindElements(locator).ToList();
        }

        public override string GetText(IWebElement element)
        {
            WaitDriver();
            return element.Text;
        }

        public override string GetTextBy(By locator)
        {
            WaitDriver();
            return _driver.FindElement(locator).Text;
        }

        public override void TypeText(string inputText, By locator)
        {
            WaitDriver();
            _driver.FindElement(locator).SendKeys(inputText);
        }

        public override void ClickByLocator(By locator)
        {
            WaitDriver();
            if (IsDisplayed(locator))
            {
                _driver.FindElement(locator).Click();
            }
        }

        public override bool IsDisplayed(By locator)
        {
            try
            {
                WaitDriver();
                return _driver.FindElement(locator).Displayed;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return false;
            }
        }

        public override void Visit(string node)
        {
            WaitDriver();
            _driver.Url = _config.GetConfigItem(node);          
        }

        public override void ExecJavaScript(string javaCode)
        {            
            js.ExecuteScript(javaCode);
            WaitDriver();
            DismissAlert();
        }         
        protected void DismissAlert()
        {
            _driver.SwitchTo().Alert().Dismiss();
        }

        protected void WaitDriver()
        {
            Thread.Sleep(_timeout_ms);
        }

        protected void DismissModalDialog(string strXPath)
        {
            By lc = By.XPath(strXPath);
            if (IsDisplayed(lc))
            {
                ClickByLocator(lc);
            }
        }

        public override void ClickByElement(IWebElement element)
        {
            WaitDriver();
            Actions actions = new Actions(_driver);
            actions.MoveToElement(element).Click().Build().Perform();
        }

        public override void MoveToByElement(IWebElement element)
        {
            WaitDriver();
            Actions actions = new Actions(_driver);
            actions.MoveToElement(element).Perform();
        }

    }
}
