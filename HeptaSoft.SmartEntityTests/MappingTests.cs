using HeptaSoft.SmartEntity;
using HeptaSoft.SmartEntityTests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            Assert.AreEqual(entity.Data.NumericProperty, classA.NumericProperty);
            Assert.AreEqual(entity.Data.DateProperty, classA.DateProperty);
            Assert.AreEqual(entity.Data.StringProperty, classA.StringProperty);
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
            Assert.AreEqual(entity.Data.NumericProperty, classA.NumericProperty);
            Assert.AreEqual(entity.Data.DateProperty, classA.DateProperty);
            Assert.AreEqual(entity.Data.StringProperty, classA.StringProperty);
        }

        [TestMethod]
        public void CanAutoMapAndBasicConvert()
        {
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
            Assert.AreEqual(entity.Data.NumericProperty, int.Parse(classBStringifiedy.NumericProperty));
            Assert.AreEqual(entity.Data.DateProperty, DateTime.Parse(classBStringifiedy.DateProperty));
            Assert.AreEqual(entity.Data.StringProperty, classBStringifiedy.StringProperty);
        }

        [TestMethod]
        public void CanAutoMapToTwoDtos()
        {
            const int mappedValue = 7;
            // Arrange
            var classA = new ClassA()
            {
                NumericProperty = 1,
                DateProperty = new DateTime(2000, 10, 29),
                StringProperty = "abc"
            };

            var factory = new SmartEntityFactory<ClassA>();
            var entity = factory.Create(classA);

            // Act
            var classB = entity.ToDto<ClassB>();
            var classBS = entity.ToDto<ClassBStringified>();

            // Assert
            Assert.AreEqual(entity.Data.NumericProperty, classA.NumericProperty);
            Assert.AreEqual(entity.Data.DateProperty, classA.DateProperty);
            Assert.AreEqual(entity.Data.StringProperty, classA.StringProperty);

            Assert.AreEqual(classB.NumericProperty, classA.NumericProperty);
            Assert.AreEqual(classB.DateProperty, classA.DateProperty);
            Assert.AreEqual(classB.StringProperty, classA.StringProperty);

            Assert.AreEqual(int.Parse(classBS.NumericProperty), classA.NumericProperty);
            Assert.AreEqual(DateTime.Parse(classBS.DateProperty), classA.DateProperty);
            Assert.AreEqual(classBS.StringProperty, classA.StringProperty);
        }

        [TestMethod]
        public void CanBuildNestedObjectsByExpression()
        {
            // arrange
            // Expression<Func<ClassA, int>> path = x => x.ObjectProperty.NumericProperty;

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
