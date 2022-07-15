using Magenic.Maqs.BaseSeleniumTest;
using Magenic.Maqs.BaseWebServiceTest;
using Magenic.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PageModel;

namespace Tests
{
    [TestClass]
    public class HomeworkTwo : BaseSeleniumTest
    {
        /// <summary>
        /// Homework 2:
        /// Add username and password with values from appsettings.json
        /// Replace the hardcoded username and password with the values from the appsetting.json
        /// Add custom logging to show  when you open the login page and when successfully logged in
        /// Capture response time for how long it takes to login
        /// </summary>
        [TestMethod]
        public void LoginUsingAppSettingCredentials()
        {

            LoginPageModel loginPage = new LoginPageModel(this.TestObject);
            HomePageModel homePage = new HomePageModel(this.TestObject);

            string userName = Config.GetGeneralValue("UserName");
            string password = Config.GetGeneralValue("Password");

            loginPage.OpenLoginPage();
            this.Log.LogMessage("Successfully navigated to login page.");
            this.PerfTimerCollection.StartTimer("Test Login Performance");
            loginPage.LoginWithValidCredentials(userName,password);
            this.PerfTimerCollection.StopTimer("Test Login Performance");
            this.Log.LogMessage("Successfully logged in and navigated to homepage.");

        }

        /// <summary>
        /// Homework 2:
        /// Extra credit
        /// Add web service 
        /// Use generic wait and manager store
        /// Use 2 instances of Selenium
        /// </summary>
        [TestMethod]
        public void GenericWaitAndManagerStore()
        {
            LoginPageModel loginPage = new LoginPageModel(this.TestObject);
            HomePageModel homePage = new HomePageModel(this.TestObject);

            string userName = Config.GetGeneralValue("UserName");
            string password = Config.GetGeneralValue("Password");
            //IWebDriver testDriver;

            //Use of ManagerStore: Web Service
            HttpClient client = new HttpClient { BaseAddress = new Uri("http://magenicautomation.azurewebsites.net/") };
            this.ManagerStore.Add("newService", new WebServiceDriverManager(() => client, this.TestObject));
            var apiResponse = ((WebServiceDriverManager)this.ManagerStore["newService"]).GetWebServiceDriver().GetWithResponse("/api/String/1", "text/plain",false);
            Assert.AreEqual(HttpStatusCode.OK, apiResponse.StatusCode, "Response status code did NOT match.");

            //Use of ManagerStore: New Instance of chrome browser
            this.ManagerStore.Add("2ndChromeBrowser", new SeleniumDriverManager(() => WebDriverFactory.GetBrowserWithDefaultConfiguration(BrowserType.Chrome) , this.TestObject));
            ((SeleniumDriverManager)this.ManagerStore["2ndChromeBrowser"]).GetWebDriver().Navigate().GoToUrl(SeleniumConfig.GetWebSiteBase());

            //Use of Generic wait to verify that Homepage is loaded after logging in
            loginPage.OpenLoginPage();
            loginPage.LoginWithValidCredentials(userName,password);
            GenericWait.WaitUntil(homePage.IsPageLoaded);
        }
    }
}