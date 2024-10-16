using System;

namespace Rekrutacja.Calculator
{
    public static class AreaCalculator 
    {
        public static int Calculate(int firstNumber, int secondNumber, GeometricFigure figure)
        {

            if (Enum.IsDefined(typeof(GeometricFigure), (int)figure) is false)
            {
                throw new ArgumentException("Undefined geometric figure.");
            }

            if (firstNumber < 0 || secondNumber < 0)
            {
                throw new ArgumentException("Values can't be negative");
            }

            switch (figure)
            {
                case GeometricFigure.Square:
                    return (int)Math.Pow(firstNumber, 2);

                case GeometricFigure.Rectangle:
                    return firstNumber * secondNumber;

                case GeometricFigure.Triangle:
                    return firstNumber * secondNumber / 2;

                case GeometricFigure.Circle:
                    return (int)(Math.PI * Math.Pow(firstNumber, 2));

                default:
                    return 0;
            }
        }
    }
}
