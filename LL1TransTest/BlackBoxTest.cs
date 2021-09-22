using NUnit.Framework;
using LL1Trans;

namespace LL1Trans.Tests
{
    public class BlackBoxTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("2", ExpectedResult = 2)]
        public int SingleDigit(string s)
        {
            return new Parser().Calc(s);
        }

        [TestCase("12", ExpectedResult = 12)]
        public int SingleNumber(string s)
        {
            return new Parser().Calc(s);
        }

        [TestCase("2+4", ExpectedResult = 6)]
        public int AddTwoPositive(string s)
        {
            return new Parser().Calc(s);
        }

        [TestCase("-2+(-4)", ExpectedResult = -6)]
        public int AddTwoNegative(string s)
        {
            return new Parser().Calc(s);
        }

        [TestCase("2-4", ExpectedResult = -2)]
        public int SubTwoPositive(string s)
        {
            return new Parser().Calc(s);
        }

        [TestCase("-2-(-4)", ExpectedResult = 2)]
        public int SubTwoNegative(string s)
        {
            return new Parser().Calc(s);
        }

        [TestCase("2*4", ExpectedResult = 8)]
        public int MulTwoPositive(string s)
        {
            return new Parser().Calc(s);
        }

        [TestCase("-2*(-4)", ExpectedResult = 8)]
        public int MulTwoNegative(string s)
        {
            return new Parser().Calc(s);
        }

        [TestCase("2*(-4)", ExpectedResult = -8)]
        public int MulNegativeAndPositive(string s)
        {
            return new Parser().Calc(s);
        }

        [TestCase("4/3", ExpectedResult = 1)]
        public int DivTwoPositive(string s)
        {
            return new Parser().Calc(s);
        }

        [TestCase("-6/(-3)", ExpectedResult = 2)]
        public int DivTwoNegative(string s)
        {
            return new Parser().Calc(s);
        }

        [TestCase("2*(4+3)", ExpectedResult = 14)]
        public int PriorityWithBrackets(string s)
        {
            return new Parser().Calc(s);
        }

        [TestCase("2*(3+4/(6-4))", ExpectedResult = 10)]
        public int NestedBrackets(string s)
        {
            return new Parser().Calc(s);
        }

        [TestCase("2-3+4*2", ExpectedResult = 7)]
        public int LongExpression(string s)
        {
            return new Parser().Calc(s);
        }

        [TestCase("9*9*9*9*9*9*9*9*9*9*9*9", ExpectedResult = 282429536481)]
        public int NumberOverflow(string s)
        {
            return new Parser().Calc(s);
        }

        [TestCase("2.5")]
        public void WrongSymbol(string s)
        {
            Assert.Throws<ParseException>(() => new Parser().Calc(s));
        }

        [TestCase("2*(3+4")]
        public void UnclosedBracket(string s)
        {
            Assert.Throws<ParseException>(() => new Parser().Calc(s));
        }

        [TestCase("2*3)+4")]
        public void UnopenedBracket(string s)
        {
            Assert.Throws<ParseException>(() => new Parser().Calc(s));
        }

        [TestCase("3/0")]
        public void DevideByZero(string s)
        {
            Assert.Throws<System.DivideByZeroException>(() => new Parser().Calc(s));
        }
    }
}