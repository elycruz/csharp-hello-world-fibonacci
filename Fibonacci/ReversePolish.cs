using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fibonacci
{
    class ReversePolish
    {
        public static bool isReversePolishDigit (String x)
        {
            return Regex.IsMatch(x, @"^\d+(?:\.\d+)?$");
        }

        public static bool isSudoValidReversePolishString (String xs)
        {
            return xs.Length > 0 && (
                    Regex.IsMatch(xs, @"^\d+$") ||
                    Regex.IsMatch(xs, @"^[\d\+\-][\d\s\+\-\/\*\%\^eExX\÷]+$")
                );
        }

        public static Tuple<bool, String> toResult(String str)
        {
            if (str.Length <= 2) 
            {
                // Reverse string
                return Tuple.Create(true, new String(str.ToCharArray().Reverse().ToArray()));
            }

            ArrayList xs = new ArrayList();
            String[] parts = str.Split(' ');
            String lastOp = "";
            Tuple<bool, String> outResult;
            Int32 ind = 0;

            foreach (String x in parts)
            {
                // If is empty string, continue
                if (x.Length == 0)
                {
                    continue;
                }
                else if (ReversePolish.isReversePolishDigit(x))
                {
                    xs.Add(Convert.ToDouble(x));
                    ind += 1;
                    continue;
                }

                String possibleCorrectOp = x.ToLower();

                dynamic a = xs[ind - 2];
                dynamic b = xs[ind - 1];
                dynamic c;

                xs.RemoveAt(ind - 1);
                xs.RemoveAt(ind - 2);

                ind -= 2;

                switch (possibleCorrectOp)
                {
                    case "e":
                    case "^":
                        c = (float) Math.Pow(a, b);
                        break;
                    case "x":
                    case "*":
                        c = a * b;
                        break;
                    case "/":
                    case "÷":
                        c = a / b;
                        break;
                    case "%":
                        c = a % b;
                        break;
                    case "+":
                        c = a + b;
                        break;
                    case "-":
                        c = a - b;
                        break;
                    default:
                        c = null;
                        break;
                }

                if (c != null)
                {
                    xs.Add(c);
                    lastOp = possibleCorrectOp;
                    ind += 1;
                }
            }

            bool reversePolishSuccess = xs.Count == 1;

            if (!reversePolishSuccess)
            {
                int lastIndex = xs.Count - 1;
                xs.InsertRange(lastIndex, (new String(lastOp.ToCharArray()[0], lastIndex)).ToArray());
                outResult = toResult(String.Join(" ", xs));
            }
            else
            {
                outResult = Tuple.Create(reversePolishSuccess, Convert.ToString(xs[0]));
            }

            return outResult;
        }
    }
}
