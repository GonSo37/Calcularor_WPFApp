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

namespace Calculator_WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary
    public partial class MainWindow : Window
    {

        double numberOne;
        double numberTwo;
        double result;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickButton = sender as Button;
            
            if(clickButton != null)
            {
                if (resultLabel.Content.ToString() == "0")
                {
                    resultLabel.Content = clickButton.Content.ToString();
                }
                else
                {
                    resultLabel.Content += clickButton.Content.ToString();
                }
            }

        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            Button clearButton = sender as Button;
            if(clearButton != null)
            {
                resultLabel.Content = "0";
            }
        }

        private void Button_Backspace(object sender, RoutedEventArgs e)
        {
            Button backspaceButton = sender as Button;
            if(backspaceButton != null)
            {
               
                if(resultLabel.Content.ToString() != "0" && resultLabel.Content.ToString().Length > 0)
                {
                    resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    if (resultLabel.Content.ToString() == "")
                    {
                        resultLabel.Content = "0";
                    }

                }
            }
        }
        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button clickButton = sender as Button;

            if(clickButton != null) 
            {
                if(clickButton.Name.ToString() == "ButtonMultiply")
                {
                    if (resultLabel.Content.ToString().EndsWith("+"))
                    {
                        
                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }
                    else if(resultLabel.Content.ToString().EndsWith("-"))
                    {
                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }
                    else if (resultLabel.Content.ToString().EndsWith("/"))
                    {
                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }
                    else if (resultLabel.Content.ToString().EndsWith("*"))
                    {
                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }
                    resultLabel.Content = resultLabel.Content + "*";
                }
                else if(clickButton.Name.ToString() == "ButtonMinus")
                {
                    if (resultLabel.Content.ToString().EndsWith("+"))
                    {

                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }
                    else if (resultLabel.Content.ToString().EndsWith("-"))
                    {
                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }
                    else if (resultLabel.Content.ToString().EndsWith("/"))
                    {
                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }
                    else if (resultLabel.Content.ToString().EndsWith("*"))
                    {
                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }

                    resultLabel.Content = resultLabel.Content + "-";
                }
                else if (clickButton.Name.ToString() == "ButtonPlus")
                {
                    if (resultLabel.Content.ToString().EndsWith("+"))
                    {

                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }
                    else if (resultLabel.Content.ToString().EndsWith("-"))
                    {
                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }
                    else if (resultLabel.Content.ToString().EndsWith("/"))
                    {
                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }
                    else if (resultLabel.Content.ToString().EndsWith("*"))
                    {
                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }
                    resultLabel.Content = resultLabel.Content + "+";
                }
                else if (clickButton.Name.ToString() == "ButtonDivide")
                {
                    if (resultLabel.Content.ToString().EndsWith("+"))
                    {

                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }
                    else if (resultLabel.Content.ToString().EndsWith("-"))
                    {
                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }
                    else if (resultLabel.Content.ToString().EndsWith("/"))
                    {
                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }
                    else if (resultLabel.Content.ToString().EndsWith("*"))
                    {
                        resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - 1);
                    }
                    resultLabel.Content = resultLabel.Content + "/";
                }
            }

        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            string input = resultLabel.Content.ToString();

            List<string> tokens = Tokenize(input);

            double result = Evaluate(tokens);

            resultLabel.Content = result.ToString();
        }



        static List<string> Tokenize(string input)
        {
            List<string> tokens = new List<string>();
            string currentToken = "";

            foreach (char c in input)
            {
                
                
                if (char.IsDigit(c))
                {
                    currentToken += c;
                }
                else
                {
                    if (currentToken != "")
                    {
                        tokens.Add(currentToken);
                        currentToken = "";
                    }
                    tokens.Add(c.ToString());
                }
            }

            if (currentToken != "")
            {
                tokens.Add(currentToken);
            }

            return tokens;
        }

        static double Evaluate(List<string> tokens)
        {
            Stack<double> numbers = new Stack<double>();
            Stack<char> operators = new Stack<char>();

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double number))
                {
                    numbers.Push(number);
                }
                else
                {
                    while (operators.Count > 0 && Priority(operators.Peek()) >= Priority(token[0]))
                    {
                        double operand2 = numbers.Pop();
                        double operand1 = numbers.Pop();
                        char op = operators.Pop();

                        double result = PerformOperation(operand1, operand2, op);
                        numbers.Push(result);
                    }

                    operators.Push(token[0]);
                }
            }

            while (operators.Count > 0)
            {
                double operand2 = numbers.Pop();
                double operand1 = numbers.Pop();
                char op = operators.Pop();

                double result = PerformOperation(operand1, operand2, op);
                numbers.Push(result);
            }

            return numbers.Pop();
        }

        static int Priority(char op)
        {
            if (op == '*' || op == '/')
                return 2;
            else if (op == '+' || op == '-')
                return 1;
            else
                return 0;
        }

        static double PerformOperation(double operand1, double operand2, char op)
        {
            switch (op)
            {
                case '+':
                    return operand1 + operand2;
                case '-':
                    return operand1 - operand2;
                case '*':
                    return operand1 * operand2;
                case '/':
                    return operand1 / operand2;
                default:
                    throw new ArgumentException("Unkownn operator: " + op);
            }
        }
    }
}