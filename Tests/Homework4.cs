using Magenic.Maqs.BaseDatabaseTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using DapperExtensions;

namespace Tests
{
    /// <summary>
    /// Tests test class with VSUnit
    /// </summary>
    [TestClass]
    public class Homework4 : BaseDatabaseTest
    {
        /// <summary>
        /// Homework 4:
        /// Create a database project
        /// - Connect to a database
        /// - Run at least one query
        /// </summary>
        [TestMethod]
        public void VerifyProductNameInDB()
        {
            int index = 0;
            string[] arrProductName = new string[] {"Car", "Fish Sticks", "Bike", "House", "Chicken Nuggts"};
            IEnumerable<Products> productTable = this.DatabaseDriver.Query<Products>("SELECT Id, ProductName FROM products").ToList();

            foreach(Products row in productTable)
            {
                Assert.AreEqual(arrProductName[index], row.ProductName, "Product Name did NOT match.");
                index++;
            }

        }

        /// <summary>
        /// Extra Credit:
        /// Write a query test and get the results as an object 
        /// - Write an execute
        /// - Write an insert
        /// - Write an update
        /// - Write a delete
        /// </summary>

        /// <summary>
        /// Test Method that uses EXECUTE command.
        /// </summary>
        [TestMethod]
        public void InsertNewOrderInDB()
        {
            Orders newOrder = new Orders();
            newOrder.OrderName = "LanzOrder1";
            newOrder.OrderId = 114;
            newOrder.ProductId = 1;
            newOrder.UserId = 2;

            int result = this.DatabaseDriver.Execute(
                "INSERT INTO orders (OrderId, OrderName, ProductId, UserId) VALUES (@OrderId, @OrderName, @ProductId, @UserId)",
                new {OrderId = newOrder.OrderId, OrderName = newOrder.OrderName, ProductId = newOrder.ProductId, UserId = newOrder.UserId});
            
            Assert.AreEqual(1, result, "Expected 1 row affected failed.");

            Orders newOrderNameInDB = this.DatabaseDriver.Query<Orders>(
                $"SELECT Id, OrderName FROM orders WHERE OrderName = '{newOrder.OrderName}'").Last();
            
            Assert.AreEqual(newOrder.OrderName, newOrderNameInDB.OrderName, "Adding new order in DB failed.");
            this.Log.LogMessage("Successfully added order: "+ newOrderNameInDB.OrderName +" with order Id: "+ newOrderNameInDB.Id);

        }

        /// <summary>
        /// Test Method that uses INSERT command.
        /// </summary>
        [TestMethod]
        public void InsertNewUserInDB()
        {
            Users newUser = new Users();
            newUser.FirstName = "Alphinaud";
            newUser.LastName = "Leveilleur";

            long result = this.DatabaseDriver.Insert<Users>(newUser);

            Users newUserInDB = this.DatabaseDriver.Query<Users>(
                $"SELECT Id, FirstName FROM users WHERE FirstName = '{newUser.FirstName}'").Last();

            Assert.AreNotEqual(0, result, "Expected 1 row affected failed.");
            Assert.AreEqual(newUser.FirstName, newUserInDB.FirstName, "Adding new user in DB failed.");
            this.Log.LogMessage("Successfully added user: "+ newUserInDB.FirstName +" with user Id: "+ newUserInDB.Id);
        
        }

        /// <summary>
        /// Test Method that uses UPDATE command.
        /// </summary>
        [TestMethod]
        public void UpdateFirstNameInDB()
        {
            string userFirstNametoUpdate = "Alphinaud";
            string newFirstName = "Alisaie";

            Users userToUpdate = this.DatabaseDriver.Query<Users>(
                $"SELECT * FROM users WHERE FirstName = '{userFirstNametoUpdate}'").First();
            
            userToUpdate.FirstName = newFirstName;
            bool isFirstNameUpdated = this.DatabaseDriver.Update(userToUpdate);

            Assert.AreEqual(true, isFirstNameUpdated, "Updating first name in DB failed.");

            Users updatedUserInDB = this.DatabaseDriver.Query<Users>(
                $"SELECT * FROM users WHERE FirstName = '{newFirstName}'").Last();

            Assert.AreEqual(newFirstName, updatedUserInDB.FirstName, "Adding new user in DB failed.");
            this.Log.LogMessage("Successfully updated first name of user with Id: "+ updatedUserInDB.Id +" to: "+ updatedUserInDB.FirstName);

        }

        /// <summary>
        /// Test Method that uses DELETE command.
        /// </summary>
        [TestMethod]
        public void DeleteOrderInDB()
        {
            string orderNameToDelete = "LanzOrder1";

            Orders orderToDelete = this.DatabaseDriver.Query<Orders>(
                $"SELECT * FROM orders WHERE OrderName = '{orderNameToDelete}'").First();
            
            int deletedOrderId = orderToDelete.Id;

            bool isOrderDeleted = this.DatabaseDriver.Delete(orderToDelete);

            Assert.AreEqual(true, isOrderDeleted, "Deleting order in DB failed.");

            List<Orders> updatedOrders = this.DatabaseDriver.Query<Orders>(
                $"SELECT Id, OrderName FROM orders WHERE OrderName = '{orderNameToDelete}'").ToList();

            foreach(Orders row in updatedOrders)
            {
                Assert.AreNotEqual(deletedOrderId,row.Id, "Deleted order still exists.");
                this.Log.LogMessage("Order with Id: "+ deletedOrderId +" does NOT exist.");
            }

        }

        [Table("Products")]
        public class Products
        {
            public int Id { get; set; }
            public string ProductName { get; set; }
        }

        [Table("Orders")]
        public class Orders
        {
            public int Id { get; set; }

            public int OrderId { get; set; }

            public string OrderName { get; set; }

            public int ProductId { get; set; }

            public int UserId { get; set; }
        }

        [Table("Users")]
        public class Users
        {
            public int Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }
        }



    }

}