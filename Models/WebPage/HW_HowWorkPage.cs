using Magenic.Maqs.BaseSeleniumTest;
using Magenic.Maqs.BaseSeleniumTest.Extensions;
using Magenic.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace PageModel
{
    public class HW_HowWorkPage : BaseSeleniumPageModel
    {

        public HW_HowWorkPage(SeleniumTestObject testObject) : base(testObject)
        {
        }

        private LazyElement lblWorksWell
        {
            get { return this.GetLazyElement(By.Id("HowWorks"), "Works Well Label"); }
        }

        private LazyElement tblNavigationMenu(string Menu)
        {
            return new LazyElement(this.TestObject, By.XPath($"//*//input[@value='{Menu}']//.."), "Navigation Menu");
        }

        public override bool IsPageLoaded()
        {
            return this.lblWorksWell.Displayed && tblNavigationMenu("How it works").GetAttribute("class") == "Selected";
        }

        public void navigateTo(string menu)
        {
            tblNavigationMenu(menu).Click();
        }
    }
}