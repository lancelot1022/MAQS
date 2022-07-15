using Magenic.Maqs.BaseSeleniumTest;
using Magenic.Maqs.BaseSeleniumTest.Extensions;
using Magenic.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace PageModel
{
    public class HW_LoginPage : BaseSeleniumPageModel
    {
        private static string LoginPageUrl = SeleniumConfig.GetWebSiteBase() + "Static/Training1/loginpage.html";

        public HW_LoginPage(SeleniumTestObject testObject) : base(testObject)
        {
        }

        private LazyElement lblLogin
        {
            get { return this.GetLazyElement(By.XPath("//*//p[contains(text(), 'Login')]"), "Login Label"); }
        }

        private LazyElement txtUserName
        {
            get { return this.GetLazyElement(By.Id("UserName"), "User Name textbox"); }
        }

        private LazyElement txtPassword
        {
            get { return this.GetLazyElement(By.Id("Password"), "Password textbox"); }
        }

        private LazyElement btnLogin
        {
            get { return this.GetLazyElement(By.Id("Login"), "Login button"); }
        }

        private LazyElement ErrorMessage
        {
            get { return this.GetLazyElement(By.Id("LoginError"), "Error message"); }
        }

        public bool OpenLoginPage()
        {
            this.TestObject.WebDriver.Navigate().GoToUrl(LoginPageUrl);
            return this.IsPageLoaded();
        }

        public override bool IsPageLoaded()
        {
            return this.lblLogin.Displayed;
        }

        public HW_HomePage Login(string username, string password)
        {
            this.txtUserName.SendKeys(username);
            this.txtPassword.SendSecretKeys(password);
            this.btnLogin.Click();

            return new HW_HomePage(this.TestObject);
        }
        
    }
}