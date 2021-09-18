using System;
using System.Collections.Generic;
using System.Text;

namespace LL1Trans
{
    /// <summary>
    /// A ::= T Z   { Ti = Aa ; As = Tt }
    /// Z ::= T Z
    /// Z ::= Λ
    /// T ::= (Y    { Yi = Tt + 1; Ts = Yy }
    /// Y ::= A )   { Ai = Yy ; Ys = Aa }
    /// Y ::= )     { Ys = Yi }
    /// </summary>
    class ParserSquare
    {
        const int AP_left_KW = 56;
        const int AP_right_KW = 57;
        const int AP_end_KW = 58;

        int symbol;
        string row = "";
        int i = 0;

        public int Calc(string input)
        {
            row = input;
            symbol = yylex();

            int y = A(0); //A - аксиома
                          // анализ результата трансляции:
            if (y < 0)
                Console.WriteLine("error");
            else
                Console.WriteLine("prefix length " + y);
            return 0;
        }

        int yylex()
        {
            if (i == row.Length)
                return -1;

            char ch = row[i++];
            switch (ch)
            {
                case '(': return AP_left_KW;
                case ')': return AP_right_KW;
                case '$': return AP_end_KW;
                default: return -1;
            }
        }

        int Y(int inherited)
        {
            int synthesized;
            switch (symbol)
            {
                case AP_left_KW:
                    {
                        if ((synthesized = A(inherited)) == -1) //Y ::= { Ai = Y i } A )
                            return -1;
                        else if (symbol == AP_right_KW)
                            return synthesized; //{Ys = As }
                        else return -1;
                    }
                case AP_right_KW:
                    {
                        symbol = yylex();//Y ::= )
                        return inherited;
                    }// { Ys = Yi }
                default: return -1;
            }
        }

        int T(int inherited)
        {
            symbol = yylex();
            return Y(inherited + 1);// T ::= ( { Yi = Ti + 1 } Y { Ts = Ys }
        }

        int Z()
        {
            switch (symbol)
            {
                case AP_left_KW: T(0); return Z();//Z ::= { Ti = 0 } T Z
                case AP_right_KW: return 0; // Z ::= Λ
                case AP_end_KW: return 0; // Z ::= Λ
                default: return -1;
            }
        }

        int A(int inherited)
        {
            if (symbol == AP_left_KW)
            {
                int synthesized = T(inherited); //A ::= { Ti = Ai } T Z
                Z();
                return synthesized; //{ As = Ts }
            }
            else return -1;
        }
    }
}
