using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TestCodeChallange.pom
{
    class HomePage : Base
    {
        private readonly string _HomePage_Url;
        private readonly string _HomePage_MenuClass;
        private readonly List<JToken> _HomePage_MenuItems;

        public HomePage(IWebDriver driver) : base(driver)
        {
            _HomePage_Url = Consts._home_page;
            _HomePage_MenuClass = _config.GetConfigItem(Consts._home_page_menu_class);
            _HomePage_MenuItems = _config.GetConfigItems(Consts._home_page_menu_items);
        }

        public void LoadHomePage()
        {
            Visit(_HomePage_Url);
        }

        public bool ValidateMenu()
        {
            bool Exists;
            By lc = By.CssSelector(_HomePage_MenuClass);
            List<IWebElement> MenuItems = FindElements(lc);
            List<string> WMenuItems = new List<string>();
            List<string> CMenuItems = _HomePage_MenuItems.Values<string>().ToList();
            MenuItems.ForEach(x => { if (!string.IsNullOrWhiteSpace(x.Text)) { WMenuItems.Add(x.Text); } });
            Exists = WMenuItems.Intersect(CMenuItems).Any();
            return Exists;
        }
    }
}
