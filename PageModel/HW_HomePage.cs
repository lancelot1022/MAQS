using Magenic.Maqs.BaseSeleniumTest;
using Magenic.Maqs.BaseSeleniumTest.Extensions;
using Magenic.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace PageModel
{
    public class HW_HomePage : BaseSeleniumPageModel
    {
        const string menuHome = "Home";
        const string menuAbout = "About";

        public HW_HomePage(SeleniumTestObject testObject) : base(testObject)
        {
        }

        private LazyElement lblWelcomeHome
        {
            get { return this.GetLazyElement(By.Id("WelcomeMessage"), "Welcome Home Label"); }
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
            return this.lblWelcomeHome.Displayed && navHomePage.GetAttribute("class") == "Selected";
        }

        public HW_AboutPage navigateToAboutPage()
        {
            navAboutPage.Click();
            return new HW_AboutPage(this.TestObject);
        }
    }
}