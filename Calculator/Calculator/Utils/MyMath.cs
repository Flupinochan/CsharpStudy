using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator.Utils
{
    public class MyMath
    {
        public static double Add(double firstNumber, double secondNumber)
        {
            return firstNumber + secondNumber;
        }

        public static double Substract(double firstNumber, double secondNumber)
        {
            return firstNumber - secondNumber;
        }

        public static double Multiply(double firstNumber, double secondNumber)
        {
            return firstNumber * secondNumber;
        }

        public static double Divide(double firstNumber, double secondNumber)
        {
            if (secondNumber == 0)
            {
                MessageBox.Show("0で割ることはできません", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            else
            {
                return firstNumber / secondNumber;
            }
        }

        public static double Square(double firstNumber, double secondNumber)
        {
            return Math.Pow(firstNumber, secondNumber);
        }

        public static double SquareRoot(double labelContent)
        {
            return Math.Sqrt(labelContent);
        }
    }
}
