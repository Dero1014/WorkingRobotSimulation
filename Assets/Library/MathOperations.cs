using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public static class MathOperator
    {
        public static int Calculate(int a, int b, string c)
        {
            switch (c)
            {
                case "+": return a + b;
                case "-": return a - b;
                case "*": return a * b;
                case "/": return a / b;
                default:
                    return a;
            }
        }

    }

    public static class MathComparator
    {

        public static bool CompareTwoNumbersWith(float a, float b, string c)
        {
            switch (c)
            {
                case ">": return a > b;
                case "<": return a < b;
                case "=": return a == b; 
                case "!": return a != b;
                default:
                    return false;
            }
        }

    }

}
