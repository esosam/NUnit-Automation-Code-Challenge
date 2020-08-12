using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestCodeChallange.pom
{
    public abstract class APage : IPage
    {
        public APage() { }

        public abstract IWebDriver DriverConn();

        public abstract void DriverDown();

        public abstract IWebElement FindElement(By locator);
        
        public abstract List<IWebElement> FindElements(By locator);

        public abstract string GetText(IWebElement element);

        public abstract string GetTextBy(By locator);

        public abstract void ClickByLocator(By locator);

        public abstract void TypeText(string inputText, By locator);

        public abstract bool IsDisplayed(By locator);

        public abstract void Visit(string node);

        public abstract void ExecJavaScript(string javaCode);

        public abstract void ClickByElement(IWebElement element);

        public abstract void MoveToByElement(IWebElement element);
    }
}
