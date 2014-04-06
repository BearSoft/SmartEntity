using System;
using System.Linq.Expressions;
using HeptaSoft.SmartEntityTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeptaSoft.SmartEntityTests.DomainModel
{
    [TestClass]
    public class MappingTests
    {
        [TestMethod]
        public void CanOutoMapAndManualMap()
        {
            const int mappedValue = 7;

            // arrange
          
        }

        [TestMethod]
        public void CanMapSiglePropertyToTwoDtos()
        {
            const int mappedValue = 7;

            // arrange
          
        }

        [TestMethod]
        public void CanAutoMap()
        {
            const int numericValue = 7;
            var dateValue = new DateTime(2000, 10, 29);
            const string stringValue = "abc";

            // arrange
           
        }

        [TestMethod]
        public void CanAutoMapAdnConvert()
        {
            const int numericValue = 7;
            var dateValue = new DateTime(2000, 10, 29);
            const string stringValue = "abc";

            // arrange
           
        }

        [TestMethod]
        public void CanBuildNestedObjectsByExpression()
        {
            // arrange
            Expression<Func<ClassA, int>> path = x => x.ObjectProperty.NumericProperty;
            
            // Act
          
            // Assert
           
        }

        [TestMethod]
        public void CanBuildNestedObjectsByString()
        {
            // arrange
           

            // Act
           

            // Assert
           
        }

        [TestMethod]
        public void CanAutoMapNestedPpoperties()
        {
            const int nestingLevel = 2;
            const int numericValue = 1;
            var dateValue = new DateTime(2000, 10, 29);
            const string stringValue = "abc";
            
            const int numericValueNested = 2;
            var dateValueNested = new DateTime(2000, 11, 29);
            const string stringValueNested = "abcdef";

            var objectValue = new ClassA()
                                  {
                                      NumericProperty = numericValueNested,
                                      DateProperty = dateValueNested,
                                      StringProperty = stringValueNested
                                  };
            // arrange
           
            // Assert : direct properties
          
        }
    }

}
