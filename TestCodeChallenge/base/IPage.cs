using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace TestCodeChallange.pom
{
    interface IPage
    {      
        public IWebDriver DriverConn();

        public void DriverDown();

        public IWebElement FindElement(By locator);

        public List<IWebElement> FindElements(By locator);

        public string GetText(IWebElement element);

        public string GetTextBy(By locator);

        public void ClickByLocator(By locator);

        public void TypeText(string inputText, By locator);

        public bool IsDisplayed(By locator);

        public void Visit(string node);

        public void ExecJavaScript(string javaCode);

        public void ClickByElement(IWebElement element);

        public void MoveToByElement(IWebElement element);
    }
}
