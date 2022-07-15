using Magenic.Maqs.BaseSeleniumTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PageModel;

namespace Tests
{
    [TestClass]
    public class Homework5 : BaseSeleniumTest
    {
        /// <summary>
        /// Homework 5:
        /// Create a login page model for: ../Training1/loginpage.html
        /// Create a page model for login, home and about pages
        /// Create a test that uses the model to do th ff:
        /// - Opens login page
        /// - Logs in with credentials (Username: Ted Password: 123)
        /// - Verify that the Homepage is displayed
        /// - Click About navigation bar
        /// - Verify About page is displayed
        /// </summary>
        [TestMethod]
        public void VerifyHomeAndAboutPages()
        {
            string username = "Ted";
            string password = "123";

            HW_LoginPage loginPage = new HW_LoginPage(this.TestObject);

            loginPage.OpenLoginPage();
            HW_HomePage homePage = loginPage.Login(username, password);
            Assert.IsTrue(homePage.IsPageLoaded(), "Homepage failed to load.");

            HW_AboutPage aboutPage = homePage.navigateToAboutPage();
            Assert.IsTrue(aboutPage.IsPageLoaded(), "About page failed to load.");
        }

        /// <summary>
        /// Extra Credit 5:
        /// Override the webdriver in at least one test
        /// </summary>
        [TestMethod]
        public void FireFox_VerifyHomeAndAboutPages()
        {
            this.ManagerStore.AddOrOverride(new SeleniumDriverManager(() =>
            WebDriverFactory.GetBrowserWithDefaultConfiguration(BrowserType.Firefox), this.TestObject));

            string username = "Ted";
            string password = "123";

            HW_LoginPage loginPage = new HW_LoginPage(this.TestObject);

            loginPage.OpenLoginPage();
            HW_HomePage homePage = loginPage.Login(username, password);
            Assert.IsTrue(homePage.IsPageLoaded(), "Homepage failed to load.");

            HW_AboutPage aboutPage = homePage.navigateToAboutPage();
            Assert.IsTrue(aboutPage.IsPageLoaded(), "About page failed to load.");

        }

    }
}