using System;
using System.Collections.Generic;
using System.Text;

namespace LL1Trans
{
    public class ParseException : Exception
    {

    }

    /// <summary>
    /// E -> T E’
    /// E’ -> + T E’ | - T E'
    /// E’ -> Λ
    /// T -> F T’
    /// T’ -> * F T’ | / F T'
    /// T’ -> Λ
    /// F -> n
    /// F -> ( E )
    /// </summary>
    public class Parser
    {
        public enum Token
        {
            End = -1,
            Digit0 = 0,
            Digit1 = 1,
            Digit2 = 2,
            Digit3 = 3,
            Digit4 = 4,
            Digit5 = 5,
            Digit6 = 6,
            Digit7 = 7,
            Digit8 = 8,
            Digit9 = 9,
            Plus = 100,
            Minus = 101,
            Mul = 102,
            Devide = 103,
            LBr = 200,
            RBr = 201
        }

        public Token symbol;
        public string row = "";
        int i = 0;

        public int Calc(string input)
        {
            row = input;
            symbol = yylex();
            int y = E();

            return y;
        }

        public Token yylex()
        {
            if (i == row.Length)
                return Token.End;

            char ch = row[i++];
            if (char.IsDigit(ch))
                return (Token)int.Parse(ch.ToString());
            else
                switch (ch)
                {
                    case '+': Console.WriteLine($"Read: {ch}"); return Token.Plus;
                    case '-': Console.WriteLine($"Read: {ch}"); return Token.Minus;
                    case '*': Console.WriteLine($"Read: {ch}"); return Token.Mul;
                    case '/': Console.WriteLine($"Read: {ch}"); return Token.Devide;
                    case '(': Console.WriteLine($"Read: {ch}"); return Token.LBr;
                    case ')': Console.WriteLine($"Read: {ch}"); return Token.RBr;
                    default: throw new ParseException();
                }
        }

        public int E() /// E -> T E’
        {
            if (symbol >= Token.Digit0 && symbol <= Token.Digit9 || symbol == Token.LBr)
                return EP(T());
            else
                throw new ParseException();
        }

        public int EP(int inh = 0) /// E’ -> + T E’ | Λ
        {
            switch (symbol)
            {
                case Token.Plus:
                    symbol = yylex();
                    return EP(inh + T());
                case Token.Minus:
                    symbol = yylex();
                    return EP(inh - T());
                case Token.RBr:
                case Token.End: return inh; // E' -> Λ
                default: throw new ParseException();
            }
        }

        public int T() /// T -> F T’
        {
            if (symbol >= Token.Digit0 && symbol <= Token.Digit9 || symbol == Token.LBr)
                return TP(F());
            else
                throw new ParseException();
        }

        public int TP(int inh = 1) /// T’ -> * F T’ | Λ
        {
            switch (symbol)
            {
                case Token.Mul:
                    symbol = yylex();
                    return TP(inh * F());
                case Token.Devide:
                    symbol = yylex();
                    return TP(inh / F());
                case Token.Minus:
                case Token.Plus:
                case Token.RBr:
                case Token.End: return inh; // T' -> Λ
                default: throw new ParseException();
            }
        }

        public int F() /// F -> 2 | 3 | 4
        {
            if (symbol >= Token.Digit0 && symbol <= Token.Digit9)
            {
                int synt = (int)symbol;
                symbol = yylex();
                return synt;
            }
            else if (symbol == Token.LBr)
            {
                symbol = yylex();
                return E();
            }
            else
                throw new ParseException();
        }

    }
}
