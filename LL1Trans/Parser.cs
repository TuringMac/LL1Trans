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
        public enum Term
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

        public Term symbol;
        public string row = "";
        int i = 0;

        public int Calc(string input)
        {
            row = input;
            symbol = yylex();
            int y = E();

            return y;
        }

        public Term yylex()
        {
            if (i == row.Length)
                return Term.End;

            char ch = row[i++];
            if (char.IsDigit(ch))
                return (Term)int.Parse(ch.ToString());
            else
                switch (ch)
                {
                    case '+': Console.WriteLine($"Read: {ch}"); return Term.Plus;
                    case '-': Console.WriteLine($"Read: {ch}"); return Term.Minus;
                    case '*': Console.WriteLine($"Read: {ch}"); return Term.Mul;
                    case '/': Console.WriteLine($"Read: {ch}"); return Term.Devide;
                    case '(': Console.WriteLine($"Read: {ch}"); return Term.LBr;
                    case ')': Console.WriteLine($"Read: {ch}"); return Term.RBr;
                    default: throw new ParseException();
                }
        }

        public int E() /// E -> T E’
        {
            if (symbol >= Term.Digit0 && symbol <= Term.Digit9 || symbol == Term.LBr)
                return EP(T());
            else
                throw new ParseException();
        }

        public int EP(int inh = 0) /// E’ -> + T E’ | Λ
        {
            switch (symbol)
            {
                case Term.Plus:
                    symbol = yylex();
                    return EP(inh + T());
                case Term.Minus:
                    symbol = yylex();
                    return EP(inh - T());
                case Term.RBr:
                case Term.End: return inh; // E' -> Λ
                default: throw new ParseException();
            }
        }

        public int T() /// T -> F T’
        {
            if (symbol >= Term.Digit0 && symbol <= Term.Digit9 || symbol == Term.LBr)
                return TP(F());
            else
                throw new ParseException();
        }

        public int TP(int inh = 1) /// T’ -> * F T’ | Λ
        {
            switch (symbol)
            {
                case Term.Mul:
                    symbol = yylex();
                    return TP(inh * F());
                case Term.Devide:
                    symbol = yylex();
                    return TP(inh / F());
                case Term.Minus:
                case Term.Plus:
                case Term.RBr:
                case Term.End: return inh; // T' -> Λ
                default: throw new ParseException();
            }
        }

        public int F() /// F -> 2 | 3 | 4
        {
            if (symbol >= Term.Digit0 && symbol <= Term.Digit9)
            {
                int synt = (int)symbol;
                symbol = yylex();
                return synt;
            }
            else if (symbol == Term.LBr)
            {
                symbol = yylex();
                return E();
            }
            else
                throw new ParseException();
        }

    }
}
