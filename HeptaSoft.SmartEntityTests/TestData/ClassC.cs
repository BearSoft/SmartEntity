using System;

namespace HeptaSoft.SmartEntityTests.TestData
{
    public class ClassC
    {
        public int NumericPropertyC { get; set; }
        public DateTime DatePropertyC { get; set; }
        public string StringPropertyC { get; set; }

        public ClassA ObjectAProperty { get; set; }
        public ClassB ObjectBProperty { get; set; }
        public ClassBStringified ObjectBsProperty { get; set; }
    }
}
