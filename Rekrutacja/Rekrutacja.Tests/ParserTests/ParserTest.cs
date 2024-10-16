using NUnit.Framework;

namespace Rekrutacja.Tests.ParserTests
{
    internal class ParserTest
    {
        [Test]
        [TestCase("278", 278)]
        [TestCase("345345", 345345)]
        [TestCase("0000", 0)]
        [TestCase("-78", 78)]
        [TestCase("00230200022", 230200022)]
        public void Parse_DigitsOnly_ProperResult(string input, int expected)
        {
            Assert.AreEqual(expected, Parser.Parser.Parse(input));
        }

        [Test]
        [TestCase("cbV67Vb87", 6787)]
        [TestCase("g004b", 4)]
        [TestCase("0000", 0)]
        [TestCase("0042", 42)]
        [TestCase("", 0)]
        [TestCase("   ", 0)]
        [TestCase("abc", 0)]
        [TestCase("!@#$%^", 0)]
        public void Parse_NotOnlyDigits_IgnoresNotDigits(string input, int expected)
        {
            Assert.AreEqual(expected, Parser.Parser.Parse(input));
        }
    }
}
