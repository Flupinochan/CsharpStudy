using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Calculator.Utils;

namespace Calculator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    double firstNumber;
    double secondNumber;
    string clickedOperation;

    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// AC ボタン クリック時の処理
    /// </summary>
    private void allClearButton_Click(object sender, RoutedEventArgs e)
    {
        resultLabel.Content = "0";
        expressionLabel.Content = "";
    }

    /// <summary>
    /// 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 ボタン クリック時の処理
    /// </summary>
    private void numberButton_Click(object sender, RoutedEventArgs e)
    {
        int clickedButtonNumber = 0;

        // ボタンの数値をintで取得
        if (sender is Button button && int.TryParse(button.Content.ToString(), out clickedButtonNumber))
        {
            // labelが0の場合は、取得した数値で上書き
            if (resultLabel.Content.Equals("0"))
            {
                resultLabel.Content = clickedButtonNumber.ToString();
                expressionLabel.Content += clickedButtonNumber.ToString();
            }
            // labelが0以外の場合は、末尾に数値を付け加える
            else
            {
                resultLabel.Content += clickedButtonNumber.ToString();
                expressionLabel.Content += clickedButtonNumber.ToString();
            }
        }
    }

    /// <summary>
    /// +, -, ×, ÷, x² ボタン クリック時の処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void operationButton_Click(object sender, RoutedEventArgs e)
    {
        // labelをfirstNumberにして、0にリセット
        if (double.TryParse(resultLabel.Content.ToString(), out firstNumber))
            resultLabel.Content = "0";
        // クリックしたボタンのOperationを取得
        if (sender is Button button && button.Content.ToString() is string buttonContent)
        {
            clickedOperation = buttonContent;
            expressionLabel.Content += buttonContent;
        }
    }

    /// <summary>
    /// √ ボタン クリック時の処理
    /// </summary>
    private void squareRootButton_Click(object sender, RoutedEventArgs e)
    {
            double calculateResult;
            if (double.TryParse(resultLabel.Content.ToString(), out calculateResult))
            {
                calculateResult = MyMath.SquareRoot(calculateResult);
                resultLabel.Content = calculateResult.ToString();
                expressionLabel.Content = calculateResult.ToString();
            }
        }

    /// <summary>
    /// . ボタン クリック時の処理
    /// </summary>
    private void decimalButton_Click(object sender, RoutedEventArgs e)
    {
        // .を含んでいなければ.を追加 ※数値に.は1つしか存在しないため
        if (resultLabel.Content.ToString() is string value && !value.Contains("."))
        {
            resultLabel.Content += ".";
            expressionLabel.Content += ".";
        }
    }

    /// <summary>
    /// = ボタン クリック時の処理
    /// </summary>
    private void equalButton_Click(object sender, RoutedEventArgs e)
    {
        double calculateResult = 0.0;

        if (double.TryParse(resultLabel.Content.ToString(), out secondNumber))
        {

            switch (clickedOperation)
            {
                case "+":
                    calculateResult = MyMath.Add(firstNumber, secondNumber);
                    break;
                case "-":
                    calculateResult = MyMath.Substract(firstNumber, secondNumber);
                    break;
                case "×":
                    calculateResult = MyMath.Multiply(firstNumber, secondNumber);
                    break;
                case "÷":
                    calculateResult = MyMath.Divide(firstNumber, secondNumber);
                    break;
                case "x²":
                    calculateResult = MyMath.Square(firstNumber, secondNumber);
                    break;
            }

            resultLabel.Content = calculateResult.ToString();
            expressionLabel.Content = calculateResult.ToString();
        }
    }


}