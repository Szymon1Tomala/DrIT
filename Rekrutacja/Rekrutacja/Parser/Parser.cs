using System.Linq;

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

            int result = 0;

            var charArray = input.Where(x => char.IsDigit(x)).ToArray();

            foreach (char c in charArray)
            {
                result = result * 10 + (c - '0');
            }

            return result;
        }
    }
}
