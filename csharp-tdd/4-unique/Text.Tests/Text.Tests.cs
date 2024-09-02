using NUnit.Framework;

namespace Text.Tests
{
    public class Tests
    {
        const string TestString = "abc", TestString2 = "llama",
            TestString3 = "racecar", TestString4 = "aabbaa";


        [SetUp]
        public void Setup()
        {
        }

        [TestCase(TestString, ExpectedResult = 0)]
        [TestCase(TestString2, ExpectedResult = 3)]
        [TestCase(TestString3, ExpectedResult = 3)]
        [TestCase(TestString4, ExpectedResult = -1)]
        [TestCase("", ExpectedResult = -1)]
        [TestCase(null, ExpectedResult = -1)]
        public int failCases(string value) 
        {
            return (Str.UniqueChar(value));
        }
    }
}