using System;

namespace LL1Trans
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                try
                {
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine("Type statement to calculate ex. 2+4*3");
                    string stmString = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(stmString))
                        stmString = "2+4*3";
                    Parser parser = new Parser();
                    Console.WriteLine(parser.Calc(stmString));
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (true);
        }
    }
}
