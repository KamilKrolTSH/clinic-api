using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClinicApi;

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

        [TestMethod]
        public void TestMethod2()
        {
            //Arrange
            int x1 = 4;
            int x2 = 0;
            int x3 = 0;
            int x4 = 0;
            int x5 = 0;
            int x6 = 0;
            float x7 = 0;
            float x8 = 0;

            //Act
            float Score_test = ClinicApi.Controllers.UserDiagnoseController.GetScore(x1, x2, x3, x4, x5, x6, x7, x8);

            //Assert
            Assert.AreEqual(4, Score_test);
        }
    }
}
