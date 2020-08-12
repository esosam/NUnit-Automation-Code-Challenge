using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCodeChallenge.pom;

namespace TestCodeChallange.pom
{
    class WindowsPage : Base
    {
        private readonly string _Windows_Url;
        private readonly string _Windows_MenuId;
        private By _Windows_MenudDDItems;

        public WindowsPage(IWebDriver driver) : base(driver)
        {
            _Windows_Url = Consts._windows_page;
            _Windows_MenuId = _config.GetConfigItem(Consts._windows_page_menu_id);
            _Windows_MenudDDItems = By.XPath("/html/body/span[2]/div/div/div/header/div/div/nav/ul/li[2]/div/ul//li/a");
        }

        public void LoadWindowsPage()
        {
            Visit(_Windows_Url);
        }

        public void ClickWindow()
        {
            By lc = By.Id(_Windows_MenuId);
            ClickByLocator(lc);   
        }      

        public bool ValidateHasDDValues()
        {
            List<string> DDValues = new List<string>();            
            List<IWebElement> DDElements = FindElements(_Windows_MenudDDItems);
            StringBuilder JavaCode = new StringBuilder("return alert('");
           
            DDElements.ForEach(x =>
            {
                if (!string.IsNullOrWhiteSpace(x.GetAttribute("text").ToString()))
                {
                    DDValues.Add(x.GetAttribute("text"));
                    JavaCode.Append(string.Concat(x.GetAttribute("text")," | "));
                    System.Diagnostics.Debug.WriteLine(x.GetAttribute("text"));
                }
            });

            JavaCode.Append("');");

            if (!DDValues.Count.Equals(0))
            {
                ExecJavaScript(JavaCode.ToString());
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
