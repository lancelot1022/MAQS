using System;
using Magenic.Maqs.BaseTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]

    public class HomeworkClass : BaseTest
    {
        /// <summary>
        /// Homework:
        /// - Create a base project
        /// - Create a new test class
        /// - Create own test method
        /// </summary>

        [TestMethod]
        public void VerifyUserInputIsEven()
        {
            int sampleInput;
            bool isEven;

            this.TestObject.Log.LogMessage("Start Test");
            sampleInput = 30;
            this.TestObject.Log.LogMessage("Sample input is: "+ sampleInput);
            HomeworkMethod sampleMethod = new HomeworkMethod();
            isEven = sampleMethod.IsInputEven(sampleInput);

            Assert.IsTrue(isEven,"PASSED - User Input is even.");
            
        }

    }

}