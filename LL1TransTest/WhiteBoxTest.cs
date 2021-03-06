using LL1Trans;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static LL1Trans.Parser;

namespace LL1Trans.Tests
{
    public class WhiteBoxTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("2.5", ExpectedResult = 2.5)]
        [TestCase("-2", ExpectedResult = -2)]
        [TestCase("2", ExpectedResult = Token.Digit2)]
        [TestCase("+", ExpectedResult = Token.Plus)]
        [TestCase("*", ExpectedResult = Token.Mul)]
        [TestCase(")", ExpectedResult = Token.RBr)]
        [TestCase("", ExpectedResult = Token.End)]
        public Token yylexTest(string s)
        {
            var parser = new Parser();
            parser.row = s;
            return parser.yylex();
        }

        [TestCase("?")]
        public void yylexTestEx(string s)
        {
            var parser = new Parser();
            parser.row = s;
            Assert.Throws<ParseException>(() => parser.yylex());
        }

        [TestCase("+5", Token.Digit2, ExpectedResult = 7)]
        [TestCase("2+8)", Token.LBr, ExpectedResult = 10)]
        public int ETest(string s, Token ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            return parser.E();
        }

        [TestCase("2+3)", Token.Mul)]
        public void ETestEx(string s, Token ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            Assert.Throws<ParseException>(() => parser.E());
        }

        [TestCase("2", Token.Plus, ExpectedResult = 2)]
        [TestCase("7", Token.Minus, ExpectedResult = -7)]
        [TestCase("2", Token.End, ExpectedResult = 0)]
        public int EPTest(string s, Token ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            return parser.EP();
        }

        [TestCase("2", Token.Devide)]
        public void EPTestEx(string s, Token ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            Assert.Throws<ParseException>(() => parser.EP());
        }

        [TestCase("*3", Token.Digit2, ExpectedResult = 6)]
        [TestCase("2", Token.LBr, ExpectedResult = 2)]
        public int TTest(string s, Token ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            return parser.T();
        }

        [TestCase("2", Token.Plus)]
        public void TTestEx(string s, Token ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            Assert.Throws<ParseException>(() => parser.T());
        }

        [TestCase("2", Token.Mul, ExpectedResult = 2)]
        [TestCase("2", Token.Devide, ExpectedResult = 0)]
        [TestCase("2", Token.End, ExpectedResult = 1)]
        public int TPTest(string s, Token ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            return parser.TP();
        }

        [TestCase("2", Token.Digit7)]
        public void TPTestEx(string s, Token ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            Assert.Throws<ParseException>(() => parser.TP());
        }

        [TestCase("2", Token.Digit2, ExpectedResult = 2)]
        [TestCase("2", Token.LBr, ExpectedResult = 2)]
        public int FTest(string s, Token ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            return parser.F();
        }

        [TestCase("2", Token.Minus)]
        public void FTestEx(string s, Token ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            Assert.Throws<ParseException>(() => parser.F());
        }
    }
}
