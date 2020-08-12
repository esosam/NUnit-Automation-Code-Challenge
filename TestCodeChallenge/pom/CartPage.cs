using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using TestCodeChallenge.pom;

namespace TestCodeChallange.pom
{
    class CartPage : Base
    {
        private readonly By _Cart_ButtonId;
        private readonly By _Cart_ItemPrice;
        private readonly By _Cart_SummaryPrice;
        private readonly By _Cart_TotalPrice;
        private readonly By _Cart_AmountDD;
        private readonly By _Cart_OptionDD;
        private readonly By _Cart_ReviewLink;

        private readonly string _Cart_Items2Add;
        public CartPage(IWebDriver driver) : base(driver)
        {
            _Cart_Items2Add = _config.GetConfigItem(Consts._cart_add_amount_items);

            //_Cart_ButtonId = By.XPath("/html/body/section/div[1]/div[1]/div[1]/div[2]/div[6]/div/div[3]/div/div/div/button");
            _Cart_ButtonId = By.Id(_config.GetConfigItem(Consts._cart_add_button_id));

            _Cart_ItemPrice = By.XPath("/html/body/section/div[1]/div/div/div/div/div/section[1]/div/div/div/div/div/div[2]/div[2]/div[2]/div/span/span[2]/span");
            _Cart_SummaryPrice = By.XPath("/html/body/section/div[1]/div/div/div/div/div/section[2]/div/div/div[1]/div/span[1]/span[2]/div/span/span[2]/span");
            _Cart_TotalPrice = By.XPath("/html/body/section/div[1]/div/div/div/div/div/section[2]/div/div/div[2]/div/span/span[2]/strong/span");
            _Cart_AmountDD = By.XPath("/html/body/section/div[1]/div/div/div/div/div/section[1]/div/div/div/div/div/div[2]/div[2]/div[1]/select");
            _Cart_OptionDD = By.XPath(string.Concat("/html/body/section/div[1]/div/div/div/div/div/section[1]/div/div/div/div/div/div[2]/div[2]/div[1]/select/option[", _Cart_Items2Add, "]"));
            _Cart_ReviewLink = By.XPath("/html/body/section/section/div/div[2]/header/a[1]");
        }

        public void LoadCartPage()
        {
            IWebElement reviewlink = FindElement(_Cart_ReviewLink);
            MoveToByElement(reviewlink);

            IWebElement cartbutton = FindElement(_Cart_ButtonId);
            ClickByElement(cartbutton);

            //ClickByLocator(_Cart_ButtonId);
        }
        
        public bool ValidateCartPrice(string _search_itemprice)
        {
            try
            {                
                IWebElement _CartItemPrice = FindElement(_Cart_ItemPrice);
                string cItemPrice = GetText(_CartItemPrice);

                IWebElement _CartSummaryPrice = FindElement(_Cart_SummaryPrice);
                string cSumPrice = GetText(_CartSummaryPrice);

                IWebElement _CartTotalPrice = FindElement(_Cart_TotalPrice);
                string cTotalPrice = GetText(_CartTotalPrice);

                if (cItemPrice.Equals(cSumPrice) && 
                    cSumPrice.Equals(cTotalPrice) && 
                    cTotalPrice.Equals(cItemPrice))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
                return false;
            }
        }
        
        public void LoadCar20Items()
        {
            ClickByLocator(_Cart_AmountDD);
            ClickByLocator(_Cart_OptionDD);
        }

        public bool ValidateCart20ItemsPrice()
        {
            try
            {
                IWebElement _CartItemPrice = FindElement(_Cart_ItemPrice);
                string cItemPrice = GetText(_CartItemPrice);

                IWebElement _CartTotalPrice = FindElement(_Cart_TotalPrice);
                string cTotalPrice = GetText(_CartTotalPrice);

                if (!float.TryParse(cItemPrice.Replace("$", ""), out float _Unit_Price))
                {
                    _Unit_Price = 0.0f;
                }

                if (!float.TryParse(cTotalPrice.Replace("$", ""), out float _Total_Price))
                {
                    _Total_Price = 0.0f;
                }

                if (!float.TryParse(_Cart_Items2Add, out float _Total_Items_In_Cart))
                {
                    _Total_Items_In_Cart = 0.0f;
                }

                float _Unit_Total_Price;
                _Unit_Total_Price = _Unit_Price * _Total_Items_In_Cart;

                if (_Total_Price.Equals(_Unit_Total_Price))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
                return false;
            }
        }
    }
}
