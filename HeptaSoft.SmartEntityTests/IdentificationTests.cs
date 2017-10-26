using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HeptaSoft.SmartEntityTests.TestData;
using HeptaSoft.SmartEntityTests;
using HeptaSoft.SmartEntity;
using System.Collections.Generic;

namespace HeptaSoft.SmartEntityTests
{
    [TestClass]
    public class IdentificationTests
    {
        [TestMethod]
        public void CanBuildRequiredFilter()
        {
            // Arrange
            var classA = new ClassA()
            {
                NumericProperty = 7,
                DateProperty = new DateTime(2000, 10, 29),
            };            
            // Act


            // Assert
        }
    }
}
