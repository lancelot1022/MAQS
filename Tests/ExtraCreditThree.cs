using Magenic.Maqs.BaseWebServiceTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Tests
{
    [TestClass]
    public class ExtraCreditThree : BaseWebServiceTest
    {

        /// <summary>
        /// Extra Credit:
        /// Create a user
        /// Create a test that will get the lists of users
        ///                 -- Not sure how to implement due to the specified endpoint only returns currently logged user
        /// Code must compile and pass
        /// </summary>
        [TestInitialize]
        public void AuthSetupExtra()
        {
            FormUrlEncodedContent tokenRequest = new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", "lanz22"),
                    new KeyValuePair<string, string>("password", "lanz22"),
                    new KeyValuePair<string, string>("scope", "authTripsAPI"),
                    new KeyValuePair<string, string>("client_id", "lanz22"),
                    new KeyValuePair<string, string>("client_secret", "lanz22")
                }
            );

            WebServiceDriver driver = new WebServiceDriver("https://magenicautomationident.azurewebsites.net/");
            var tokenEndPoint = driver.Post("https://magenicautomationident.azurewebsites.net/connect/token", "text/html", tokenRequest);
            JObject tokenObj = JObject.Parse(tokenEndPoint);
            string accessToken = (string)tokenObj["access_token"];

            HttpClient client = new HttpClient{BaseAddress = new Uri("https://magenictripinfoapi.azurewebsites.net/")};
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            this.WebServiceDriver = new WebServiceDriver(client);

        }

        [TestMethod]
        public void GetuserList()
        {
            var apiResponse = this.WebServiceDriver.GetWithResponse("/authTripsAPI/users","application/xml", false);
            Assert.AreEqual(HttpStatusCode.OK,apiResponse.StatusCode);

            JObject responseBody = this.WebServiceDriver.Get<JObject>("/authTripsAPI/users","application/json", false);
            string userId = (string)responseBody["value"]["userId"];
            
            Assert.AreNotEqual("0",userId);
        }
    }
}
