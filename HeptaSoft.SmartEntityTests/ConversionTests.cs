using HeptaSoft.SmartEntity;
using HeptaSoft.SmartEntityTests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HeptaSoft.SmartEntityTests
{
    [TestClass]
    public class ConversionTests
    {

        [TestMethod]
        public void CanBasicConvert()
        {
            // Arrange
            var classBStringifiedy = new ClassBStringified()
            {
                NumericProperty = "2",
                DateProperty = "2000/11/29",
                StringProperty = "abcdef"
            };
            var entity = new SmartEntityFactory<ClassB>().CreateEmpty();

            // Act
            entity.FillFromDto(classBStringifiedy);

            // Assert
            Assert.AreEqual(entity.Data.NumericProperty, int.Parse(classBStringifiedy.NumericProperty));
            Assert.AreEqual(entity.Data.DateProperty, DateTime.Parse(classBStringifiedy.DateProperty));
            Assert.AreEqual(entity.Data.StringProperty, classBStringifiedy.StringProperty);
        }

        [TestMethod]
        public void CanComplexConvertToTwoDtos()
        {
            // Arrange
            var classA = new ClassA()
            {
                NumericProperty = 1,
                DateProperty = new DateTime(2000, 10, 29),
                StringProperty = "abc",
                ObjectProperty = new ClassA() { NumericProperty = 444, DateProperty = new DateTime(2011, 01, 5) }
            };
            var entity = new SmartEntityFactory<ClassA>().Create(classA);

            // Act
            var classB = entity.ToDto<ClassB>();
            var classBS = entity.ToDto<ClassBStringified>();

            // Assert
            Assert.AreEqual(entity.Data.NumericProperty, classA.NumericProperty);
            Assert.AreEqual(entity.Data.DateProperty, classA.DateProperty);
            Assert.AreEqual(entity.Data.StringProperty, classA.StringProperty);
            Assert.AreEqual(entity.Data.ObjectProperty, classA.ObjectProperty);

            Assert.AreEqual(classB.NumericProperty, classA.NumericProperty);
            Assert.AreEqual(classB.DateProperty, classA.DateProperty);
            Assert.AreEqual(classB.StringProperty, classA.StringProperty);
            Assert.AreEqual(classB.ObjectProperty.NumericProperty, classA.ObjectProperty.NumericProperty);
            Assert.AreEqual(classB.ObjectProperty.DateProperty, classA.ObjectProperty.DateProperty);

            Assert.AreEqual(int.Parse(classBS.NumericProperty), classA.NumericProperty);
            Assert.AreEqual(DateTime.Parse(classBS.DateProperty), classA.DateProperty);
            Assert.AreEqual(classBS.StringProperty, classA.StringProperty);
            Assert.AreEqual(int.Parse(classBS.ObjectProperty.NumericProperty), classA.ObjectProperty.NumericProperty);
            Assert.AreEqual(DateTime.Parse(classBS.ObjectProperty.DateProperty), classA.ObjectProperty.DateProperty);
        }
    }
}
