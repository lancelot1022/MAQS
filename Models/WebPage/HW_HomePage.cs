using Magenic.Maqs.BaseSeleniumTest;
using Magenic.Maqs.BaseSeleniumTest.Extensions;
using Magenic.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace PageModel
{
    public class HW_HomePage : BaseSeleniumPageModel
    {

        public HW_HomePage(SeleniumTestObject testObject) : base(testObject)
        {
        }

        private LazyElement lblWelcomeHome
        {
            get { return this.GetLazyElement(By.Id("WelcomeMessage"), "Welcome Home Label"); }
        }

        private LazyElement tblNavigationMenu(string Menu)
        {
            return new LazyElement(this.TestObject, By.XPath($"//*//input[@value='{Menu}']//.."), "Navigation Menu");
        }

        public override bool IsPageLoaded()
        {
            return this.lblWelcomeHome.Displayed && tblNavigationMenu("Home").GetAttribute("class") == "Selected";
        }

        public void navigateTo(string menu)
        {
            tblNavigationMenu(menu).Click();
        }
    }
}