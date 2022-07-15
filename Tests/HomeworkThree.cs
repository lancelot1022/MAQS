using Magenic.Maqs.BaseWebServiceTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServiceModel;
using System.Net.Http;
using System.Net;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Tests
{
    [TestClass]
    public class HomeworkThree : BaseWebServiceTest
    {
        /// <summary>
        /// Homework 3:
        /// Create a web service project
        /// - Override WebServiceDriver and add the key "Auth" and value "AuthKey" to the default request header
        /// - Create and XML/JSON test that does get from "/api/XML_JSON/GetAllProducts" and saves the results as an array of Products
        /// </summary>
        [TestInitialize]
        public void AuthSetup()
        {
            HttpClient client = new HttpClient{BaseAddress = this.GetBaseWebServiceUri()};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Auth", "AuthKey");

            this.WebServiceDriver = new WebServiceDriver(client);
        }

        [TestMethod]
        public void GetAllProducts()
        {
            string[] productList = new string[] {"Tomato Soup", "Yo-yo", "Hammer"};

            var apiResponse = this.WebServiceDriver.GetWithResponse("/api/XML_JSON/GetAllProducts","application/xml", false);
            Assert.AreEqual(HttpStatusCode.OK,apiResponse.StatusCode);

            ProductJson[] apiProducts = this.WebServiceDriver.Get<ProductJson[]>("/api/XML_JSON/GetAllProducts","application/json", false);

            for(int i = 0; i < apiProducts.Length; i++)
            {
                Assert.AreEqual(productList[i],apiProducts[i].Name);
            }
            

        }

    }

}