using Magenic.Maqs.BaseSeleniumTest;
using Magenic.Maqs.BaseSeleniumTest.Extensions;
using Magenic.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace PageModel
{
    public class HW_AboutPage : BaseSeleniumPageModel
    {
        public HW_AboutPage(SeleniumTestObject testObject) : base(testObject)
        {
        }
        private LazyElement lblEmail
        {
            get { return this.GetLazyElement(By.XPath("//*//table[@id='AboutTable']//td[text() = 'Email']"), "Email label"); }
        }

        private LazyElement lblPhone
        {
            get { return this.GetLazyElement(By.XPath("//*//table[@id='AboutTable']//td[text() = 'Phone']"), "Phone label"); }
        }

        private LazyElement tblNavigationMenu(string Menu)
        {
            return new LazyElement(this.TestObject, By.XPath($"//*//input[@value='{Menu}']//.."), "Navigation Menu");
        }

        public override bool IsPageLoaded()
        {
            return this.lblEmail.Displayed && this.lblPhone.Displayed && tblNavigationMenu("About").GetAttribute("class") == "Selected";
        }
    }
}