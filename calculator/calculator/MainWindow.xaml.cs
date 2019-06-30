using System;
using System.Diagnostics;
using System.Windows;

namespace calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddNumber(string number)
        {
            try
            {
                if (_operation == "")
                {
                    UpdateResult(number);

                    _lvalue = double.Parse(Result.Text);
                }
                else
                {
                    ClearString(LastChar());

                    UpdateResult(number);

                    _rvalue = double.Parse(Result.Text);
                }
            }
            catch (Exception e)
            {
                FullReset();
                Debug.WriteLine(e);
            }
        }

        private void Button_Click0(object sender, RoutedEventArgs e) { Reset(); AddNumber("0"); }
        private void Button_Click1(object sender, RoutedEventArgs e) { Reset(); AddNumber("1"); }
        private void Button_Click2(object sender, RoutedEventArgs e) { Reset(); AddNumber("2"); }
        private void Button_Click3(object sender, RoutedEventArgs e) { Reset(); AddNumber("3"); }
        private void Button_Click4(object sender, RoutedEventArgs e) { Reset(); AddNumber("4"); }
        private void Button_Click5(object sender, RoutedEventArgs e) { Reset(); AddNumber("5"); }
        private void Button_Click6(object sender, RoutedEventArgs e) { Reset(); AddNumber("6"); }
        private void Button_Click7(object sender, RoutedEventArgs e) { Reset(); AddNumber("7"); }
        private void Button_Click8(object sender, RoutedEventArgs e) { Reset(); AddNumber("8"); }
        private void Button_Click9(object sender, RoutedEventArgs e) { Reset(); AddNumber("9"); }
        private void Button_Eq(object sender, RoutedEventArgs e)
        {
            _eqWasUsed = true;
            switch (_operation)
            {
                case "+":
                    {
                        Result.Text = add(ref _lvalue, _rvalue);
                        break;
                    }
                case "-":
                    {
                        Result.Text = sub(ref _lvalue, _rvalue);
                        break;
                    }
                case "*":
                    {
                        Result.Text = mul(ref _lvalue, _rvalue);
                        break;
                    }
                case "/":
                    {
                        Result.Text = dev(ref _lvalue, _rvalue);
                        break;
                    }
                default:
                    {
                        ResetValues();
                        break;
                    }
            }

        }

        private void Button_Comma(object sender, RoutedEventArgs e)
        {
            Reset();

            if (!_commaIsUsed)
            {
                Result.Text += ",";
                _commaIsUsed = true;
            }
        }

        private void Button_Plus(object sender, RoutedEventArgs e) { SetSign("+"); }
        private void Button_Mul(object sender, RoutedEventArgs e) { SetSign("*"); }
        private void Button_Div(object sender, RoutedEventArgs e) { SetSign("/"); }
        private void Button_Minus(object sender, RoutedEventArgs e) { SetSign("-"); }
        private void Button_C(object sender, RoutedEventArgs e) { FullReset(); }
        public static string Add(ref double a, double b) { a += b; ; return a.ToString(); }
        public static string Sub(ref double a, double b) { a -= b; ; return a.ToString(); }
        public static string Mul(ref double a, double b) { a *= b; return a.ToString(); }
        public static string Dev(ref double a, double b)
        {
            if (b != 0)
            {
                a /= b;
                return a.ToString();
            }
            else
                return "You can't divide by 0!";
        }

        private void UpdateResult(string number)
        {
            if (Result.Text[0] == '0' && !_commaIsUsed)
                Result.Text = number;
            else
                Result.Text += number;
        }

        private void SetSign(string s)
        {
            _eqWasUsed = false;//possibility to do operate on the previous result

            _commaIsUsed = false;

            if (Char.IsNumber(LastChar()))
            {
                _operation = s;
                Result.Text += s;
            }
        }

        private void ClearString(char s)
        {
            switch (s)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                    {
                        Result.Text = "0"; break;
                    }

                default:
                    break;
            }
        }

        private void ResetValues()
        {
            _operation = "";
            _rvalue = 0;
            Result.Text = _lvalue.ToString();
            _eqWasUsed = false;
            _commaIsUsed = false;
        }

        private void Reset() { if (_eqWasUsed) FullReset(); }
        private void FullReset() { _lvalue = 0; ResetValues(); }
        private char LastChar() { return Result.Text[Result.Text.Length - 1]; }
        private double _lvalue = 0, _rvalue = 0;
        private bool _commaIsUsed = false;
        private bool _eqWasUsed = false;
        private string _operation = "";

        //Actions
        internal delegate string Action(ref double a, double b);

        internal Action dev = new Action(Dev);
        internal Action sub = new Action(Sub);
        internal Action add = new Action(Add);
        internal Action mul = new Action(Mul);
    }
}