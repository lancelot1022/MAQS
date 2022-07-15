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

        private LazyElement navHomePage
        {
            get { return this.GetLazyElement(By.XPath("//*[@id='Home']/.."), "Nav Home Page"); }
        }
        private LazyElement navAboutPage
        {
            get { return this.GetLazyElement(By.XPath("//*[@id='About']/.."), "Nav About Page"); }
        }

        public override bool IsPageLoaded()
        {
            return this.lblEmail.Displayed && this.lblPhone.Displayed && navAboutPage.GetAttribute("class") == "Selected";
        }
    }
}