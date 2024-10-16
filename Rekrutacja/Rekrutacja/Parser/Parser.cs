using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekrutacja.Parser
{
    public static class Parser
    {
        public static int Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;       
            }

            var charArray = input.ToCharArray();

            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            charArray = charArray.Where(x => digits.Contains(x)).ToArray();

            int power = charArray.Length - 1;
            int result = 0;

            for (int i = 0; i >= 0; i++)
            {
                result = (int)Math.Pow(10, power) * MapDigitToInt(charArray[i]);
            }




            return result;
        }

        private static int MapDigitToInt(char digit)
        {
            switch (digit)
            {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                default: return 0;
            }
        }
    }
}
