using HeptaSoft.SmartEntity;
using HeptaSoft.SmartEntityTests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

namespace HeptaSoft.SmartEntityTests
{
    [TestClass]
    public class MappingTests
    {
        [TestMethod]
        public void CanAutoMap()
        {
            // Arrange
            var classA = new ClassA()
            {
                NumericProperty = 2,
                DateProperty = new DateTime(2000, 11, 29),
                StringProperty = "abcdef"
            };
            var factory = new SmartEntityFactory<ClassB>();
            var entity = factory.CreateEmpty();

            // Act
            entity.FillFromDto(classA);

            // Assert
            Assert.IsTrue(entity.Data.NumericProperty.Equals(classA.NumericProperty));
            Assert.IsTrue(entity.Data.DateProperty.Equals(classA.DateProperty));
            Assert.IsTrue(entity.Data.StringProperty.Equals(classA.StringProperty));
        }


        [TestMethod]
        public void CanAutoMapOverwrite()
        {
            // Arrange
            var classA = new ClassA()
            {
                NumericProperty = 1,
                DateProperty = new DateTime(2000, 10, 29),
                StringProperty = "abc"
            };

            var classB = new ClassB()
            {
                NumericProperty = 2,
                DateProperty = new DateTime(2000, 11, 29),
                StringProperty = "abcdef"
            };

            var factory = new SmartEntityFactory<ClassB>();
            var entity = factory.Create(classB);

            // Act
            entity.FillFromDto(classA);

            // Assert
            Assert.IsTrue(entity.Data.NumericProperty.Equals(classA.NumericProperty));
            Assert.IsTrue(entity.Data.DateProperty.Equals(classA.DateProperty));
            Assert.IsTrue(entity.Data.StringProperty.Equals(classA.StringProperty));
        }

        [TestMethod]
        public void CanAutoMapAndManualMap()
        {
            const int mappedValue = 7;

            // arrange

        }

        [TestMethod]
        public void CanAutoMapAndConvert()
        {
            /*
            // Arrange
            var classBStringifiedy = new ClassBStringified()
            {
                NumericProperty = "2",
                DateProperty = "2000/11/29",
                StringProperty = "abcdef"
            };
            var factory = new SmartEntityFactory<ClassB>();
            var entity = factory.CreateEmpty();

            // Act
            entity.FillFromDto(classBStringifiedy);

            // Assert
            Assert.IsTrue(entity.Data.NumericProperty.Equals(int.Parse(classBStringifiedy.NumericProperty)));
            Assert.IsTrue(entity.Data.DateProperty.Equals(DateTime.Parse(classBStringifiedy.DateProperty)));
            Assert.IsTrue(entity.Data.StringProperty.Equals(classBStringifiedy.StringProperty));
            */
        }

        [TestMethod]
        public void CanMapSiglePropertyToTwoDtos()
        {
            const int mappedValue = 7;

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
        public void CanAutoMapNestedProperties()
        {
            // Arrange

            // Act

            // Assert
        }

    }

}
