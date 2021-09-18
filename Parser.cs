using System;
using System.Collections.Generic;
using System.Text;

namespace LL1Trans
{
    class ParseException : Exception
    {

    }

    /// <summary>
    /// S -> E
    /// E -> T E’
    /// E’ -> + T E’
    /// E’ -> Λ
    /// T -> F T’
    /// T’ -> * F T’
    /// T’ -> Λ
    /// F -> 2|3|4
    /// </summary>
    class Parser
    {
        enum Term
        {
            End = 0,
            Digit2 = 2,
            Digit3 = 3,
            Digit4 = 4,
            Plus = 5,
            Mul = 6
        }

        Term symbol;
        string row = "";
        int i = 0;

        public int Calc(string input)
        {
            row = input;
            symbol = yylex();
            int y = E(0);

            return y;
        }

        Term yylex()
        {
            if (i == row.Length)
                return Term.End;

            char ch = row[i++];
            switch (ch)
            {
                case '2': Console.WriteLine("Read: 2"); return Term.Digit2;
                case '3': Console.WriteLine("Read: 3"); return Term.Digit3;
                case '4': Console.WriteLine("Read: 4"); return Term.Digit4;
                case '+': Console.WriteLine("Read: +"); return Term.Plus;
                case '*': Console.WriteLine("Read: *"); return Term.Mul;
                case '$': Console.WriteLine("Read: $"); return Term.End;
                default: throw new ParseException();
            }
        }

        int S(int inh)
        {
            return E(inh);
        }

        int E(int inh) /// E -> T E’
        {
            switch (symbol)
            {
                case Term.Digit2:
                case Term.Digit3:
                case Term.Digit4:
                    int synt = T();
                    return EP(synt);
                default: throw new ParseException();
            }
        }

        int EP(int inh = 0) /// E’ -> + T E’ | Λ
        {
            switch (symbol)
            {
                case Term.Plus:
                    symbol = yylex();
                    int synt = inh + T();
                    return EP(synt);
                case Term.End: return inh; // E' -> Λ
                default: throw new ParseException();
            }
        }

        int T() /// T -> F T’
        {
            switch (symbol)
            {
                case Term.Digit2:
                case Term.Digit3:
                case Term.Digit4:
                    int synt = F();
                    return TP(synt);
                default: throw new ParseException();
            }
        }

        int TP(int inh = 1) /// T’ -> * F T’ | Λ
        {
            switch (symbol)
            {
                case Term.Mul:
                    symbol = yylex();
                    int synt = inh * F();
                    return TP(synt);
                case Term.Plus: return inh;
                case Term.End: return inh; // T' -> Λ
                default: throw new ParseException();
            }
        }

        int F() /// F -> 2 | 3 | 4
        {
            switch (symbol)
            {
                case Term.Digit2:
                case Term.Digit3:
                case Term.Digit4:
                    int synt = (int)symbol;
                    symbol = yylex();
                    return synt;
                default: throw new ParseException();
            }
        }

    }
}
