using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCodeChallenge.pom;

namespace TestCodeChallange.pom
{
    class SearchPage : Base
    {
        public string ItemPriceSearch;

        private readonly By _Search_ButtonId;
        private readonly By _Search_InputId;
        private readonly By _Search_AmountItems;
        private readonly string _Search_CancelId_Dialog;
        private readonly string _Search_InputText;
        private readonly int _Search_ManyItems;

        public SearchPage(IWebDriver driver) : base(driver)
        {
            _Search_ButtonId = By.Id(_config.GetConfigItem(Consts._windows_page_search_btn));
            _Search_InputId = By.Id(_config.GetConfigItem(Consts._windows_page_search_input));
            _Search_CancelId_Dialog = "/html/body/div[4]/div[2]/button";
            _Search_InputText = Consts._search_sw_input_search;

            string manyItems = _config.GetConfigItem(Consts._search_sw_list_items);
            if (!int.TryParse(manyItems, out _Search_ManyItems))
            {
                _Search_ManyItems = 0;
            }

            _Search_ManyItems++;

            _Search_AmountItems = By.XPath(string.Concat("/html/body/div[2]/section/div[1]/div[1]/div[3]/div/div/ul/li[position()<", _Search_ManyItems.ToString(), "]/div/a/div[2]/div[2]/div/span[3]/span[1]"));

        }

        public void SendSearchInput()
        {            
            TypeText(_Search_InputText, _Search_InputId);
            LoadSearchPage();
            DismissModalDialog(_Search_CancelId_Dialog);
        }

        public void LoadSearchPage()
        {
            ClickByLocator(_Search_ButtonId);
        }

        public void LoadSoftwareListItems()
        {
            try
            {               
                List<IWebElement> SoftwareItems = FindElements(_Search_AmountItems);
                List<string> SoftwarePrices = new List<string>();
                StringBuilder JavaCode = new StringBuilder("return alert('");

                SoftwareItems.ForEach(x =>
                {
                    if (!string.IsNullOrWhiteSpace(x.Text))
                    {
                        SoftwarePrices.Add(x.Text);
                        JavaCode.Append(string.Concat(x.Text, " | "));
                        System.Diagnostics.Debug.WriteLine(x.Text);
                    }
                });

                JavaCode.Append("');");

                if (!SoftwarePrices.Count.Equals(0))
                {
                    ItemPriceSearch = SoftwarePrices.FirstOrDefault();
                    ExecJavaScript(JavaCode.ToString());                    
                }
                else
                {
                    ItemPriceSearch = "$0.0";
                }
            }
            catch(Exception err)
            {
                ItemPriceSearch = "$0.0";
                System.Diagnostics.Debug.WriteLine(err);
            }
        }
    }
}
