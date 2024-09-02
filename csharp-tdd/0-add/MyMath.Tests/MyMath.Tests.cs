using NUnit.Framework;
using MyMath;
namespace MyMath.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SimpleAddition()
        {
            int res = MyMath.Operations.Add(1, 1);
            Assert.AreEqual(2, res);
            Assert.Pass();
        }
    }
}