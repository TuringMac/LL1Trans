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
        [TestCase("2", ExpectedResult = Term.Digit2)]
        [TestCase("+", ExpectedResult = Term.Plus)]
        [TestCase("*", ExpectedResult = Term.Mul)]
        [TestCase(")", ExpectedResult = Term.RBr)]
        [TestCase("", ExpectedResult = Term.End)]
        public Term yylexTest(string s)
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

        [TestCase("+5", Term.Digit2, ExpectedResult = 7)]
        [TestCase("2+8)", Term.LBr, ExpectedResult = 10)]
        public int ETest(string s, Term ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            return parser.E();
        }

        [TestCase("2+3)", Term.Mul)]
        public void ETestEx(string s, Term ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            Assert.Throws<ParseException>(() => parser.E());
        }

        [TestCase("2", Term.Plus, ExpectedResult = 2)]
        [TestCase("7", Term.Minus, ExpectedResult = -7)]
        [TestCase("2", Term.End, ExpectedResult = 0)]
        public int EPTest(string s, Term ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            return parser.EP();
        }

        [TestCase("2", Term.Devide)]
        public void EPTestEx(string s, Term ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            Assert.Throws<ParseException>(() => parser.EP());
        }

        [TestCase("*3", Term.Digit2, ExpectedResult = 6)]
        [TestCase("2", Term.LBr, ExpectedResult = 2)]
        public int TTest(string s, Term ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            return parser.T();
        }

        [TestCase("2", Term.Plus)]
        public void TTestEx(string s, Term ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            Assert.Throws<ParseException>(() => parser.T());
        }

        [TestCase("2", Term.Mul, ExpectedResult = 2)]
        [TestCase("2", Term.Devide, ExpectedResult = 0)]
        [TestCase("2", Term.End, ExpectedResult = 1)]
        public int TPTest(string s, Term ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            return parser.TP();
        }

        [TestCase("2", Term.Digit7)]
        public void TPTestEx(string s, Term ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            Assert.Throws<ParseException>(() => parser.TP());
        }

        [TestCase("2", Term.Digit2, ExpectedResult = 2)]
        [TestCase("2", Term.LBr, ExpectedResult = 2)]
        public int FTest(string s, Term ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            return parser.F();
        }

        [TestCase("2", Term.Minus)]
        public void FTestEx(string s, Term ch)
        {
            var parser = new Parser();
            parser.row = s;
            parser.symbol = ch;
            Assert.Throws<ParseException>(() => parser.F());
        }
    }
}
