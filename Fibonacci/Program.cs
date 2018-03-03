using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fibonacci;

namespace Fibonacci
{
    class Program
    {
        static void Greeting ()
        {
            Console.WriteLine("\nFibonacci List Generator." +
                "\n=============================================\n" + 
                "\nEnter a fib max number to get a list for it " +
                "(Reverse Polish notation is allowed for generating \"max\" number):\n\n");
        }

        static bool askIfContinue ()
        {
            Console.WriteLine("\nWould you like to enter another expression (y/n)?");
            String incoming = Console.ReadKey().Key.ToString().ToLower();
            Console.WriteLine("\n");

            while (incoming != "n" && incoming != "y")
            {
                Console.WriteLine("\nWould you like to enter another expression (y/n)?");
                incoming = Console.ReadKey().Key.ToString().ToLower();
                Console.WriteLine("\n");
            }

            return incoming == "y";
        }

        static void PrintList(ArrayList list)
        {
            Int32 listLen = list.Count;
            Int32 ind = 0;
            foreach (dynamic x in list)
            {
                Console.Write(ind != listLen ? "{0}, " : "{0}", x);
                ind += 1;
            }
            Console.WriteLine("\n");
        }

        static ArrayList fibTo(double max)
        {
            ArrayList outgoing = new ArrayList();
            Int64 a = 0;
            Int64 b = 1;
            while (a <= max)
            {
                if (a <= max)
                {
                    outgoing.Add(a);
                }
                if (b <= max)
                {
                    outgoing.Add(b);
                }
                a += b;
                b += a;
            }
            return outgoing;
        }

        static String getIncomingStr ()
        {
            String incomingStr = Console.ReadLine();

            while (!ReversePolish.isSudoValidReversePolishString(incomingStr))
            {
                Console.WriteLine("\"{0}\" Is not a valid number or polish-notation number.\n", incomingStr);

                Console.WriteLine("Please try again:\n");

                incomingStr = Console.ReadLine();
            }

            return incomingStr;
        }

        static void Main(string[] args)
        {
            bool quit = false;

            while (!quit)
            {
                Greeting();

                String incoming = getIncomingStr();

                Console.WriteLine("");

                Tuple<bool, String> normalizedIncomingRslt = ReversePolish.toResult(incoming);

                double fibMax = Convert.ToDouble(normalizedIncomingRslt.Item2);

                PrintList(fibTo(fibMax));

                quit = !askIfContinue();
            }

        }

    }

}
