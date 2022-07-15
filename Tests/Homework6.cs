using System;
using Magenic.Maqs.BaseSeleniumTest;
using Magenic.Maqs.Utilities.Helper;
using NUnit.Framework;
using PageModel;
using System.Collections.Generic;
using Magenic.Maqs.BaseWebServiceTest;
using System.Net.Http;

namespace Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Homework6 : BaseSeleniumTest
    {
        /// <summary>
        /// Homework 6:
        ///  - Create Page Models for the ff:
        ///  - .../training2/loginpage /HomePage /HowWork /Async /About
        ///  - Data drive 5 invalid login
        ///  - Setup tests up to run in parallel
        ///  - Create a class setup function that writes to the console:
        ///   - System.Console.WriteLine("Class Setup")
        ///  - Create a method teardown function that writes to the console:
        ///   - System.Console.WriteLine("Function Teardown")
        /// Extra Credit:
        ///  - Create a composite project
        ///   - Remove the database, email and Appium pieces
        ///   - Add a web service call for the test cleanup
        ///   - Make all the tests use NUnit instead of VSUnit
        /// </summary>

        [SetUp]
        public void ClassSetup()
        {
            System.Console.WriteLine("Class Setup");
        }

        [TearDown]
        public void Cleanup()
        {
            System.Console.WriteLine("Function Teardown");

            HttpClient client = new HttpClient { BaseAddress = new Uri("http://magenicautomation.azurewebsites.net/") };
            WebServiceDriver webService = new WebServiceDriver(client);
            webService.GetWithResponse("/api/String/1", "text/plain",false);
        }

        [Category("DataDriven")]
        [Category(TestCategories.NUnit)]
        [TestCaseSource("userCredentials")]
        public void DataDriveInvalidLogin(string userName, string password)
        {
            HW_LoginPage loginPage = new HW_LoginPage(this.TestObject);

            loginPage.OpenLoginPage();
            bool isErrorMsgDisplayed = loginPage.InvalidLogin(userName, password);
            Assert.IsTrue(isErrorMsgDisplayed, "Error message NOT displayed for invalid login");

        }

        public static IEnumerable<string[]> userCredentials
        {
            get
            {
                yield return new string[] { "invalidUser1", "password1" };
                yield return new string[] { "invalidUser2", "password2" };
                yield return new string[] { "invalidUser3", "password3" };
                yield return new string[] { "invalidUser4", "password4" };
                yield return new string[] { "invalidUser5", "password5" };
            }
        }

        [Test]
        public void VerifyHomeAndAsyncPages()
        {
            string username = "Ted";
            string password = "123";

            HW_LoginPage loginPage = new HW_LoginPage(this.TestObject);
            HW_HomePage homePage = new HW_HomePage(this.TestObject);
            HW_AsyncPage asyncPage = new HW_AsyncPage(this.TestObject);

            loginPage.OpenLoginPage();
            loginPage.Login(username, password);
            Assert.IsTrue(homePage.IsPageLoaded(), "Homepage failed to load.");

            homePage.navigateTo("Async page");
            Assert.IsTrue(asyncPage.IsPageLoaded(), "About page faile to load.");
        }
    }

}
