using Magenic.Maqs.BaseSeleniumTest;
using Magenic.Maqs.BaseSeleniumTest.Extensions;
using Magenic.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace PageModel
{
    public class HW_AsyncPage : BaseSeleniumPageModel
    {

        public HW_AsyncPage(SeleniumTestObject testObject) : base(testObject)
        {
        }

        private LazyElement lblOptions
        {
            get { return this.GetLazyElement(By.XPath("//*//label[text()='Options']"), "Works Well Label"); }
        }

        private LazyElement dropdownOptions
        {
            get { return this.GetLazyElement(By.Id("Selector"), "Works Well Label"); }
        }

        private LazyElement tblNavigationMenu(string Menu)
        {
            return new LazyElement(this.TestObject, By.XPath($"//*//input[@value='{Menu}']//.."), "Navigation Menu");
        }

        public override bool IsPageLoaded()
        {
            return this.lblOptions.Displayed && this.dropdownOptions.Displayed && tblNavigationMenu("Async page").GetAttribute("class") == "Selected";
        }

        public void navigateTo(string menu)
        {
            tblNavigationMenu(menu).Click();
        }
    }
}