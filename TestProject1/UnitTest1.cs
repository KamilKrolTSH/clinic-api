using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            int one = 1;
            int second_one = 1;

            //Act
            int two = one + second_one;

            //Assert
            Assert.AreEqual(2, two);
        }
    }
}
