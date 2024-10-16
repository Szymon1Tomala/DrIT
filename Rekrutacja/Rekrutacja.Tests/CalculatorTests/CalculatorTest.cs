using NUnit.Framework;
using Rekrutacja.Calculator;
using System;

namespace Rekrutacja.Tests.CalculatorTests
{
    internal class AreaCalculatorTest
    {
        [Test]
        [TestCase(4, 0, 16)]
        [TestCase(4, 10, 16)]
        [TestCase(7, 0, 49)]
        [TestCase(7, 5, 49)]
        public void Calculate_Square_ReturnsProperResult_IgnoringSecondNumber(int firstNumber, int secondNumber, int expectedResult)
        {
            // Act
            var actualResult = AreaCalculator.Calculate(firstNumber, secondNumber, GeometricFigure.Square);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        [TestCase(3, 0, 28)]
        [TestCase(3, 100, 28)]
        [TestCase(5, 0, 78)]
        [TestCase(5, 10, 78)]
        public void Calculate_Circle_ReturnsProperResult_IgnoringSecondNumber(int firstNumber, int secondNumber, int expectedResult)
        {
            // Act
            var actualResult = AreaCalculator.Calculate(firstNumber, secondNumber, GeometricFigure.Circle);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        [TestCase(4, 5, 20)]
        [TestCase(7, 3, 21)]
        public void Calculate_Rectangle_ReturnsProperResult(int firstNumber, int secondNumber, int expectedResult)
        {
            // Act
            var actualResult = AreaCalculator.Calculate(firstNumber, secondNumber, GeometricFigure.Rectangle);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        [TestCase(4, 5, 10)]
        [TestCase(6, 8, 24)]
        public void Calculate_Triangle_ReturnsProperResult(int firstNumber, int secondNumber, int expectedResult)
        {
            // Act
            var actualResult = AreaCalculator.Calculate(firstNumber, secondNumber, GeometricFigure.Triangle);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        [TestCase(2, -3, GeometricFigure.Rectangle)]
        [TestCase(-92, -3, GeometricFigure.Rectangle)]
        [TestCase(7, -5, GeometricFigure.Triangle)]
        [TestCase(4, -13, GeometricFigure.Triangle)]
        public void Calculate_InvalidFigure_ThrowsException(int firstNumber, int secondNumber, GeometricFigure figure)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                AreaCalculator.Calculate(firstNumber, secondNumber, figure)
            );
        }

        [Test]
        public void Calculate_InvalidFigure_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                AreaCalculator.Calculate(4, 5, (GeometricFigure)999)
            );
        }
    }
}
