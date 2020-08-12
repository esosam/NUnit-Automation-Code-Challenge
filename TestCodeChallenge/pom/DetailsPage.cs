using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCodeChallenge.pom;

namespace TestCodeChallange.pom
{
    class DetailsPage : Base
    {
        public string ItemPriceDetails;

        private readonly By _Details_FirstItem;
        private readonly By _Details_PriceItem;
        private readonly By _Details_AmountItems;
        private readonly string _Details_ManyItems;
        private readonly string _Details_CancelId_Dialog;

        public DetailsPage(IWebDriver driver) : base(driver)
        {
            _Details_ManyItems = _config.GetConfigItem(Consts._search_sw_list_item);
            _Details_AmountItems = By.XPath(string.Concat("/html/body/div[2]/section/div[1]/div[1]/div[3]/div/div/ul/li[", _Details_ManyItems, "]/div/a/div[2]/div[2]/div/span[3]/span[1]"));
            _Details_CancelId_Dialog = "/html/body/div[3]/div[2]/button"; 
            _Details_FirstItem = By.XPath("/html/body/div[2]/section/div[1]/div[1]/div[3]/div/div/ul/li[1]/div/a");
            _Details_PriceItem = By.XPath("/html/body/section/div[1]/div[1]/div[1]/div[2]/div[6]/div/div[1]/div/div/div[1]/span");
        }

        public void LoadDetailsPage()
        {
            ClickByLocator(_Details_AmountItems);
            DismissModalDialog(_Details_CancelId_Dialog);
        }

        public bool ValidateDetailsPrice(string _search_itemprice)
        {
            try
            {
                ClickByLocator(_Details_FirstItem);
                DismissModalDialog(_Details_CancelId_Dialog);

                IWebElement _DetailItemPrice = FindElement(_Details_PriceItem);
                ItemPriceDetails = GetText(_DetailItemPrice);

                if (ItemPriceDetails.Equals(_search_itemprice))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception err)
            {
                ItemPriceDetails = "$0.0";
                System.Diagnostics.Debug.WriteLine(err);
                return false;
            }
        }
    }
}
