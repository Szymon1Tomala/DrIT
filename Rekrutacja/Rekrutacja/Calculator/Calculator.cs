using System;

namespace Rekrutacja.Calculator
{
    public enum ArithmeticOperation
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public static class Calculator 
    {
        public static double PerformOperation(double firstNumber, double secondNumber, ArithmeticOperation operation)
        {

            if (Enum.IsDefined(typeof(ArithmeticOperation), (int)operation) is false)
            {
                throw new ArgumentException("Not allowed arithmetic operation.");
            }

            if (operation is ArithmeticOperation.Division && secondNumber == 0)
            {
                throw new ArgumentException("It's not allowed to divide by 0.");
            }

            switch (operation)
            {
                case ArithmeticOperation.Addition:
                    return firstNumber + secondNumber;
                case ArithmeticOperation.Subtraction:
                    return firstNumber - secondNumber;
                case ArithmeticOperation.Multiplication:
                    return firstNumber * secondNumber;
                case ArithmeticOperation.Division:
                    return firstNumber / secondNumber;
                default:
                    return 0;
            }
        }
    }
}
