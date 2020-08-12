using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using TestCodeChallange.pom;

namespace TestCodeChallange
{
    [TestFixture]
    public class TestFocus
    {
        protected IWebDriver web_driver;
        HomePage p_home;
        WindowsPage p_windows;
        SearchPage p_search;
        DetailsPage p_details;
        CartPage p_cart;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            p_home = new HomePage(web_driver);
            web_driver = p_home.DriverConn();

            p_windows = new WindowsPage(web_driver);
            p_search = new SearchPage(web_driver);
            p_details = new DetailsPage(web_driver);
            p_cart = new CartPage(web_driver);
        } 

        [Test, Order(1)]
        public void TestHomePageMenu()
        {            
            p_home.LoadHomePage();            
            Assert.IsTrue(p_home.ValidateMenu(), "Menu is not valid, Missing Items", null);            
        }

        [Test, Order(2)]
        public void TestWindowsPageDD()
        {            
            p_windows.LoadWindowsPage();
            p_windows.ClickWindow();
            Assert.IsTrue(p_windows.ValidateHasDDValues(), "DD is not valid, has no DD values to print", null);          
        }

        [Test, Order(3)]
        public void TestDetailsPrice()
        {           
            p_search.LoadSearchPage();
            p_search.SendSearchInput();
            p_search.LoadSoftwareListItems();
            p_details.LoadDetailsPage();
            Assert.IsTrue(p_details.ValidateDetailsPrice(p_search.ItemPriceSearch), "Prices on Search Page and Detail Page for First Item are not the same Price Value", null);
        }

        [Test, Order(4)]
        public void TestCartPrice()
        {
            p_cart.LoadCartPage();            
            Assert.IsTrue(p_cart.ValidateCartPrice(p_search.ItemPriceSearch), "Prices on Cart are not a match, 3 prices on screen are not the same", null);
        }

        [Test, Order(5)]
        public void TestCart20ItemsPrice()
        {
            p_cart.LoadCar20Items();
            Assert.IsTrue(p_cart.ValidateCart20ItemsPrice(), "Total Price of 20 Items base on Unit Price is not correct, the price don't match", null);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            p_home.DriverDown();
        }       
    }
}