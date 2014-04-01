using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartEntity.Basic;

namespace SmartEntityTests.Basic
{
    [TestClass]
    public class HashHelperTests
    {        
        /// <summary>
        /// Computes the sha256 hash and expects a correct output.
        /// Correct hash taken from online application http://www.xorbin.com/tools/sha256-hash-calculator
        /// </summary>
        [TestMethod]
        public void ComputeSha256Hash_ExpectedOutput_Test1()
        {
            // Arrange
            const string inputString = "passwordToHash";
            const string expectedOutput = "155166377faa51932c3267608e4c95cd1e70768e69855f9a5363e83127a32d92";

            // Act
            var outputString = HashHelper.ComputeSha256Hash(inputString);

            // Assert
            Assert.IsTrue(expectedOutput.ToUpperInvariant() == outputString.ToUpperInvariant());
        }

        /// <summary>
        /// Computes the sha256 hash and expects a correct output.
        /// Correct hash and sentence taken from wiki: https://en.wikipedia.org/wiki/SHA-2
        /// </summary>
        [TestMethod]
        public void ComputeSha256Hash_ExpectedOutput_Test2()
        {
            // Arrange
            const string inputString = "The quick brown fox jumps over the lazy dog";
            const string expectedOutput = "d7a8fbb307d7809469ca9abcb0082e4f8d5651e46d3cdb762d02d0bf37c9e592";

            // Act
            var outputString = HashHelper.ComputeSha256Hash(inputString);

            // Assert
            Assert.IsTrue(expectedOutput.ToUpperInvariant() == outputString.ToUpperInvariant());
        }

        /// <summary>
        /// Computes the sha256 hash and expects a correct output.
        /// Correct hash and sentence taken from wiki: https://en.wikipedia.org/wiki/SHA-2
        /// </summary>
        [TestMethod]
        public void ComputeSha256Hash_ExpectedOutput_Test3()
        {
            // Arrange
            const string inputString = "The quick brown fox jumps over the lazy dog.";
            const string expectedOutput = "ef537f25c895bfa782526529a9b63d97aa631564d5d789c2b765448c8635fb6c";

            // Act
            var outputString = HashHelper.ComputeSha256Hash(inputString);

            // Assert
            Assert.IsTrue(expectedOutput.ToUpperInvariant() == outputString.ToUpperInvariant());
        }
    }
}
