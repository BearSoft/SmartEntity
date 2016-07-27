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
            var classB = new ClassB()
            {
                NumericProperty = 2,
                DateProperty = new DateTime(2000, 11, 29),
                StringProperty = "abcdef"
            };

            // Act
            var entity = new SmartEntityFactory<ClassB>().Create(classB);

            // Assert
            Assert.AreEqual(entity.Data.NumericProperty, classB.NumericProperty);
            Assert.AreEqual(entity.Data.DateProperty, classB.DateProperty);
            Assert.AreEqual(entity.Data.StringProperty, classB.StringProperty);
        }

        [TestMethod]
        public void CanAutoMapAndFillFromDto()
        {
            // Arrange
            var classA = new ClassA()
            {
                NumericProperty = 2,
                DateProperty = new DateTime(2000, 11, 29),
                StringProperty = "abcdef"
            };
            var entity = new SmartEntityFactory<ClassB>().CreateEmpty();

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
            var entity = new SmartEntityFactory<ClassB>().Create(classB);

            // Act
            entity.FillFromDto(classA);

            // Assert
            Assert.AreEqual(entity.Data.NumericProperty, classA.NumericProperty);
            Assert.AreEqual(entity.Data.DateProperty, classA.DateProperty);
            Assert.AreEqual(entity.Data.StringProperty, classA.StringProperty);
        }

        [TestMethod]
        public void CanAutoMapNestedObjects()
        {
            // Arrange
            var classB = new ClassB()
            {
                NumericProperty = 2,
                DateProperty = new DateTime(2000, 11, 29),
                StringProperty = "abcdef",
                ObjectProperty = new ClassB() {  NumericProperty = 41, StringProperty = "hhg" }
            };

            // Act
            var entity = new SmartEntityFactory<ClassB>().Create(classB);

            // Assert
            Assert.AreEqual(entity.Data.NumericProperty, classB.NumericProperty);
            Assert.AreEqual(entity.Data.DateProperty, classB.DateProperty);
            Assert.AreEqual(entity.Data.StringProperty, classB.StringProperty);

            Assert.AreEqual(entity.Data.ObjectProperty, classB.ObjectProperty);
            Assert.AreEqual(entity.Data.ObjectProperty.NumericProperty, classB.ObjectProperty.NumericProperty);
            Assert.AreEqual(entity.Data.ObjectProperty.StringProperty, classB.ObjectProperty.StringProperty);
        }

        [TestMethod]
        public void CanConvertNestedObjects()
        {
            // Arrange
            var classA = new ClassA()
            {
                NumericProperty = 2,
                DateProperty = new DateTime(2000, 11, 29),
                StringProperty = "abcdef",
                ObjectProperty = new ClassA() { NumericProperty = 32, StringProperty = "h21sadsadasdas1212hg" }
            };
            var entity = new SmartEntityFactory<ClassA>().Create(classA);

            // Act
            var classB = entity.ToDto<ClassB>();
            var classBS = entity.ToDto<ClassBStringified>();

            // Assert
            Assert.AreEqual(classB.NumericProperty, classA.NumericProperty);
            Assert.AreEqual(classB.DateProperty, classA.DateProperty);
            Assert.AreEqual(classB.StringProperty, classA.StringProperty);
            Assert.AreEqual(classB.ObjectProperty.NumericProperty, classA.ObjectProperty.NumericProperty);
            Assert.AreEqual(classB.ObjectProperty.StringProperty, classA.ObjectProperty.StringProperty);

            Assert.AreEqual(int.Parse(classBS.NumericProperty), classA.NumericProperty);
            Assert.AreEqual(DateTime.Parse(classBS.DateProperty), classA.DateProperty);
            Assert.AreEqual(classBS.StringProperty, classA.StringProperty);
            Assert.AreEqual(classBS.ObjectProperty.StringProperty, classA.ObjectProperty.StringProperty);
            Assert.AreEqual(int.Parse(classBS.ObjectProperty.NumericProperty), classA.ObjectProperty.NumericProperty);
        }


        [TestMethod]
        public void CanConvertNestedObjectsMultipleLevels()
        {
            // Arrange
            var simpleClassB = new ClassB()
            {
                NumericProperty = 222222222,
                StringProperty = "Helloooooo  ooo",
                DateProperty = new DateTime(1999, 5, 8)
            };

            var classA = new ClassA()
            {
                NumericProperty = 2,
                DateProperty = new DateTime(2000, 11, 29),
                StringProperty = "abcdef",
                ObjectProperty = new ClassA() { NumericProperty = 32, StringProperty = "h21sadsadasdas1212hg", ObjectProperty = new ClassA() {  NumericProperty = 55555, StringProperty = "aaaa", ObjectProperty = new SmartEntityFactory<ClassB>().Create(simpleClassB).ToDto<ClassA>() } }
            };
            
            // Act
            var classB = new SmartEntityFactory<ClassA>().Create(classA).ToDto<ClassB>();

            // Assert
            Assert.AreEqual(classB.NumericProperty, classA.NumericProperty);
            Assert.AreEqual(classB.DateProperty, classA.DateProperty);
            Assert.AreEqual(classB.StringProperty, classA.StringProperty);

            Assert.AreEqual(classB.ObjectProperty.NumericProperty, classA.ObjectProperty.NumericProperty);
            Assert.AreEqual(classB.ObjectProperty.StringProperty, classA.ObjectProperty.StringProperty);

            Assert.AreEqual(classB.ObjectProperty.ObjectProperty.NumericProperty, classA.ObjectProperty.ObjectProperty.NumericProperty);
            Assert.AreEqual(classB.ObjectProperty.ObjectProperty.StringProperty, classA.ObjectProperty.ObjectProperty.StringProperty);

            Assert.AreEqual(classB.ObjectProperty.ObjectProperty.ObjectProperty.NumericProperty, simpleClassB.NumericProperty);
            Assert.AreEqual(classB.ObjectProperty.ObjectProperty.ObjectProperty.StringProperty, simpleClassB.StringProperty);
            Assert.AreEqual(classB.ObjectProperty.ObjectProperty.ObjectProperty.DateProperty, simpleClassB.DateProperty);
        }


        [TestMethod]
        public void CanBuildNestedObjectsByExpression()
        {
            // Arrange
            // Expression<Func<ClassA, int>> path = x => x.ObjectProperty.NumericProperty;

            // Act

            // Assert

        }

        [TestMethod]
        public void CanBuildNestedObjectsByString()
        {
            // Arrange


            // Act


            // Assert

        }
    }
}
