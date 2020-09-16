using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InputValidator inputValidator;

        private Memory memory;

        private OperationExecutor operationExecutor;

        private OutputFormatter outputFormatter;
        public MainWindow()
        {
            inputValidator = new InputValidator();
            memory = new Memory();
            operationExecutor = new OperationExecutor();
            outputFormatter = new OutputFormatter();

            InitializeComponent();

            this.PaddingTopGrid.MouseDown += new MouseButtonEventHandler(this.DragMoveWindow);
            this.ToolbarGrid.MouseDown += new MouseButtonEventHandler(this.DragMoveWindow);

            this.btnMinimize.MouseDown += new MouseButtonEventHandler(this.MinimizeWindow);
            this.btnClose.MouseDown += new MouseButtonEventHandler(this.CloseWindow);

            this.btnOne.Click += new RoutedEventHandler(this.AppendCharacter);
            this.btnTwo.Click += new RoutedEventHandler(this.AppendCharacter);
            this.btnThree.Click += new RoutedEventHandler(this.AppendCharacter);
            this.btnFour.Click += new RoutedEventHandler(this.AppendCharacter);
            this.btnFive.Click += new RoutedEventHandler(this.AppendCharacter);
            this.btnSix.Click += new RoutedEventHandler(this.AppendCharacter);
            this.btnSeven.Click += new RoutedEventHandler(this.AppendCharacter);
            this.btnEight.Click += new RoutedEventHandler(this.AppendCharacter);
            this.btnNine.Click += new RoutedEventHandler(this.AppendCharacter);
            this.btnZero.Click += new RoutedEventHandler(this.AppendCharacter);
            this.btnDot.Click += new RoutedEventHandler(this.AppendCharacter);

            this.btnDel.Click += new RoutedEventHandler(this.RemoveCharacter);
            this.btnCE.Click += new RoutedEventHandler(this.ClearInput);
            this.btnC.Click += new RoutedEventHandler(this.ClearAll);

            this.btnResult.Click += new RoutedEventHandler(this.CalculateResult);
            this.btnChangeSign.Click += new RoutedEventHandler(this.ChangeSign);

            this.btnMC.Click += new RoutedEventHandler(this.SetMemoryState);
            this.btnMS.Click += new RoutedEventHandler(this.SetMemoryState);
            this.btnMR.Click += new RoutedEventHandler(this.SetMemoryState);

            this.btnAdd.Click += new RoutedEventHandler(this.SetOperation);
            this.btnSubtract.Click += new RoutedEventHandler(this.SetOperation);
            this.btnMultiply.Click += new RoutedEventHandler(this.SetOperation);
            this.btnDivide.Click += new RoutedEventHandler(this.SetOperation);
            this.btnInvert.Click += new RoutedEventHandler(this.SetOperation);
            this.btnSquareRoot.Click += new RoutedEventHandler(this.SetOperation);
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void DragMoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void MinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void AppendCharacter(object sender, RoutedEventArgs e)
        {
            if (operationExecutor.State == ExecutorState.OperatorGot)
            {
                inputValidator.ClearInput();
                operationExecutor.State = ExecutorState.SecondOperandInput;
            }

            string newText = inputValidator.Validate((sender as Button).Content.ToString());
            SetMainOutput(newText);
        }

        private void CalculateResult(object sender, RoutedEventArgs e)
        {
            if (operationExecutor.State == ExecutorState.FirstOperandInput)
            {
                operationExecutor.FirstOperand = inputValidator.getInput();
            }
            else
            {
                operationExecutor.SecondOperand = inputValidator.getInput();
            }
            double? result = operationExecutor.Calculate();

            SetMainOutput(outputFormatter.MakeResult(result.Value));
            SetSecondaryOutput(outputFormatter.MakeTempExpression(operationExecutor.FirstOperand,
                                                                           operationExecutor.SecondOperand,
                                                                           operationExecutor.Operator));
            operationExecutor.FirstOperand = result;

        }

        private void ChangeSign(object sender, RoutedEventArgs e)
        {
            SetMainOutput(inputValidator.ChangeSign());
        }

        private void ClearAll(object sender, RoutedEventArgs e)
        {
            operationExecutor.Clear();
            SetSecondaryOutput("");
            SetMainOutput(inputValidator.ClearInput());
        }

        private void ClearInput(object sender, RoutedEventArgs e)
        {
            if (operationExecutor.State == ExecutorState.ResultCalculated)
            {
                ClearAll(sender, e);
                return;
            }
            SetMainOutput(inputValidator.ClearInput());
        }

        private void RemoveCharacter(object sender, RoutedEventArgs e)
        {
            if (operationExecutor.State == ExecutorState.ResultCalculated)
            {
                ClearAll(sender, e);
                return;
            }
            SetMainOutput(inputValidator.ClearCharacter());
        }

        private void SetMainOutput(string output)
        {
            if (output.Length <= 10)
            {
                this.MainOutput.FontSize = 54;
            }
            else if (output.Length <= 15)
            {
                this.MainOutput.FontSize = 36;
            }
            else if (output.Length <= 20)
            {
                this.MainOutput.FontSize = 28;
            }
            this.MainOutput.Content = output;
        }

        private void SetMemoryIndicator(bool state)
        {
            if (state)
            {
                this.MemoryIndicator.Opacity = 1;
            }
            else
            {
                this.MemoryIndicator.Opacity = 0;
            }
        }

        private void SetMemoryState(object sender, RoutedEventArgs e)
        {
            string buttonText = (sender as Button).Content.ToString();
            if (buttonText == "MC")
            {
                SetMemoryIndicator(false);
                memory.Clear();
            }
            else if (buttonText == "MS")
            {
                SetMemoryIndicator(true);
                memory.Value = inputValidator.getInput();
            }
            else if (buttonText == "MR")
            {
                if (memory.IsEmpty())
                {
                    return;
                }

                string memoryValue = memory.Value.ToString();
                SetMainOutput(memoryValue);
                inputValidator.Value = memoryValue;

                if (operationExecutor.State == ExecutorState.ResultCalculated)
                {
                    SetSecondaryOutput("");
                }
            }
        }

        private void SetSecondaryOutput(string output)
        {
            if (output.Length <= 20)
            {
                this.SecondaryOutput.FontSize = 27;
            }
            else if (output.Length <= 30)
            {
                this.SecondaryOutput.FontSize = 18;
            }
            else
            {
                this.SecondaryOutput.FontSize = 14;
            }
            this.SecondaryOutput.Content = output;
        }

        private void SetOperation(object sender, RoutedEventArgs e)
        {
            string buttonText = (sender as Button).Content.ToString();
            OperatorType operation = InputValidator.ConvertToOperatorType(buttonText);

            if (operationExecutor.State == ExecutorState.SecondOperandInput)
            {
                CalculateResult(sender, e);
            }

            if (operationExecutor.State == ExecutorState.FirstOperandInput)
            {
                operationExecutor.FirstOperand = inputValidator.getInput();
            }

            if (operationExecutor.State == ExecutorState.ResultCalculated)
            {
                operationExecutor.SecondOperand = null;
                operationExecutor.State = ExecutorState.OperatorGot;
            }


            operationExecutor.Operator = operation;
            if (operation == OperatorType.Inversion || operation == OperatorType.SquareRoot)
            {
                CalculateResult(sender, e);
                return;
            }

            SetSecondaryOutput(outputFormatter.MakeTempExpression(operationExecutor.FirstOperand,
                                                                  operationExecutor.SecondOperand,
                                                                  operationExecutor.Operator));
        }
    }
}
