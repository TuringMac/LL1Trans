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
        enum Term
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

        Term symbol;
        string row = "";
        int i = 0;

        public int Calc(string input)
        {
            row = input;
            symbol = yylex();
            int y = E();

            return y;
        }

        Term yylex()
        {
            if (i == row.Length)
                return Term.End;

            char ch = row[i++];
            switch (ch)
            {
                case '0': Console.WriteLine($"Read: {ch}"); return Term.Digit0;
                case '1': Console.WriteLine($"Read: {ch}"); return Term.Digit1;
                case '2': Console.WriteLine($"Read: {ch}"); return Term.Digit2;
                case '3': Console.WriteLine($"Read: {ch}"); return Term.Digit3;
                case '4': Console.WriteLine($"Read: {ch}"); return Term.Digit4;
                case '5': Console.WriteLine($"Read: {ch}"); return Term.Digit5;
                case '6': Console.WriteLine($"Read: {ch}"); return Term.Digit6;
                case '7': Console.WriteLine($"Read: {ch}"); return Term.Digit7;
                case '8': Console.WriteLine($"Read: {ch}"); return Term.Digit8;
                case '9': Console.WriteLine($"Read: {ch}"); return Term.Digit9;
                case '+': Console.WriteLine($"Read: {ch}"); return Term.Plus;
                case '-': Console.WriteLine($"Read: {ch}"); return Term.Minus;
                case '*': Console.WriteLine($"Read: {ch}"); return Term.Mul;
                case '/': Console.WriteLine($"Read: {ch}"); return Term.Devide;
                case '(': Console.WriteLine($"Read: {ch}"); return Term.LBr;
                case ')': Console.WriteLine($"Read: {ch}"); return Term.RBr;
                case '$': Console.WriteLine($"Read: {ch}"); return Term.End;
                default: throw new ParseException();
            }
        }

        int E() /// E -> T E’
        {
            switch (symbol)
            {
                case Term.Digit0:
                case Term.Digit1:
                case Term.Digit2:
                case Term.Digit3:
                case Term.Digit4:
                case Term.Digit5:
                case Term.Digit6:
                case Term.Digit7:
                case Term.Digit8:
                case Term.Digit9:
                case Term.LBr:
                    return EP(T());
                default: throw new ParseException();
            }
        }

        int EP(int inh = 0) /// E’ -> + T E’ | Λ
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

        int T() /// T -> F T’
        {
            switch (symbol)
            {
                case Term.Digit0:
                case Term.Digit1:
                case Term.Digit2:
                case Term.Digit3:
                case Term.Digit4:
                case Term.Digit5:
                case Term.Digit6:
                case Term.Digit7:
                case Term.Digit8:
                case Term.Digit9:
                case Term.LBr:
                    return TP(F());
                default: throw new ParseException();
            }
        }

        int TP(int inh = 1) /// T’ -> * F T’ | Λ
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

        int F() /// F -> 2 | 3 | 4
        {
            switch (symbol)
            {
                case Term.Digit0:
                case Term.Digit1:
                case Term.Digit2:
                case Term.Digit3:
                case Term.Digit4:
                case Term.Digit5:
                case Term.Digit6:
                case Term.Digit7:
                case Term.Digit8:
                case Term.Digit9:
                    int synt = (int)symbol;
                    symbol = yylex();
                    return synt;
                case Term.LBr:
                    symbol = yylex();
                    return E();
                default: throw new ParseException();
            }
        }

    }
}
