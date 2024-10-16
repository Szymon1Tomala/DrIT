using NUnit.Framework;
using Rekrutacja.Calculator;
using System;

namespace Rekrutacja.Tests.CalculatorTests
{
    internal class CalculatorTest
    {
        [Test]
        [TestCase(4, 3, 7)]
        [TestCase(7, -3, 4)]
        [TestCase(0, 0, 0)]
        public void PerformOperation_Addition_ReturnsProperResult(int firstNumber, int secondNumber, int expectedResult)
        {
            // Act
            var actualResult = Calculator.Calculator.PerformOperation(firstNumber, secondNumber, ArithmeticOperation.Addition);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        [TestCase(4, 3, 1)]
        [TestCase(7, -3, 10)]
        [TestCase(0, 0, 0)]
        public void PerformOperation_Subtraction_ReturnsProperResult(int firstNumber, int secondNumber, int expectedResult)
        {
            // Act
            var actualResult = Calculator.Calculator.PerformOperation(firstNumber, secondNumber, ArithmeticOperation.Subtraction);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        [TestCase(4, 3, 12)]
        [TestCase(7, -3, -21)]
        [TestCase(0, 5, 0)]
        public void PerformOperation_Multiplication_ReturnsProperResult(int firstNumber, int secondNumber, int expectedResult)
        {
            // Act
            var actualResult = Calculator.Calculator.PerformOperation(firstNumber, secondNumber, ArithmeticOperation.Multiplication);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        [TestCase(6, 3, 2)]
        [TestCase(7, -1, -7)]
        [TestCase(0, 5, 0)]
        public void PerformOperation_Division_ReturnsProperResult(int firstNumber, int secondNumber, int expectedResult)
        {
            // Act
            var actualResult = Calculator.Calculator.PerformOperation(firstNumber, secondNumber, ArithmeticOperation.Division);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void PerformOperation_DivisionByZero_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                Calculator.Calculator.PerformOperation(3, 0, ArithmeticOperation.Division)
            );
        }

        [Test]
        [TestCase(4, 3)]
        [TestCase(7, -3)]
        [TestCase(0, 0)]
        public void PerformOperation_InvalidOperation_ThrowsException(int firstNumber, int secondNumber)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                Calculator.Calculator.PerformOperation(firstNumber, secondNumber, (ArithmeticOperation)999)
            );
        }
    }
}
