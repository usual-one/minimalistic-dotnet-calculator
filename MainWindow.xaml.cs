
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalculatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Member for storing and validation current input.
        /// </summary>
        private InputValidator inputValidator;

        /// <summary>
        /// Member for storing memory value.
        /// </summary>
        private Memory memory;

        /// <summary>
        /// Member for executing operations and storing operands, operator.
        /// </summary>
        private OperationExecutor operationExecutor;

        /// <summary>
        /// Member for formatting output for main, secondary outputs.
        /// </summary>
        private OutputFormatter outputFormatter;

        /// <summary>
        /// Constructor.
        /// </summary>
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

        /// <summary>
        /// Closes window. Exits application.
        /// </summary>
        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Sets windown into DragMove state.
        /// </summary>
        private void DragMoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// Minimizes window.
        /// </summary>
        private void MinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Appends character depending on pressed button.
        /// </summary>
        private void AppendCharacter(object sender, RoutedEventArgs e)
        {
            if (operationExecutor.State == ExecutorState.OperatorGot)
            {
                inputValidator.ClearInput();
                operationExecutor.State = ExecutorState.SecondOperandInput;
            }

            if (operationExecutor.State == ExecutorState.ResultCalculated ||
                operationExecutor.State == ExecutorState.Error)
            {
                inputValidator.ClearInput();
                SetSecondaryOutput("");
                operationExecutor.Clear();
            }
            string newText = inputValidator.Validate((sender as Button).Content.ToString());
            SetMainOutput(newText);
        }

        /// <summary>
        /// Calculates result of inputted expression.
        /// </summary>
        private void CalculateResult(object sender, RoutedEventArgs e)
        {
            if (operationExecutor.State == ExecutorState.Error)
            {
                return;
            }

            if (operationExecutor.State == ExecutorState.FirstOperandInput)
            {
                operationExecutor.FirstOperand = inputValidator.getInput();
            }
            else
            {
                operationExecutor.SecondOperand = inputValidator.getInput();
            }
            double? result = operationExecutor.Calculate();

            if (operationExecutor.State == ExecutorState.ResultCalculated)
            {
                SetMainOutput(outputFormatter.MakeResult(result.Value));
                SetSecondaryOutput(outputFormatter.MakeTempExpression(operationExecutor.FirstOperand,
                                                                               operationExecutor.SecondOperand,
                                                                               operationExecutor.Operator));
            }
            else if (operationExecutor.State == ExecutorState.Error)
            {
                SetMainOutput(outputFormatter.PrintError());
                SetSecondaryOutput("");
            }
            
            operationExecutor.FirstOperand = result;

        }

        /// <summary>
        /// Changes sign of current input.
        /// </summary>
        private void ChangeSign(object sender, RoutedEventArgs e)
        {
            if (operationExecutor.State == ExecutorState.ResultCalculated  ||
                operationExecutor.State == ExecutorState.Error)
            {
                inputValidator.ClearInput();
                SetSecondaryOutput("");
                operationExecutor.Clear();
            }
            SetMainOutput(inputValidator.ChangeSign());
        }

        /// <summary>
        /// Sets app into default state.
        /// </summary>
        private void ClearAll(object sender, RoutedEventArgs e)
        {
            operationExecutor.Clear();
            SetSecondaryOutput("");
            SetMainOutput(inputValidator.ClearInput());
        }

        /// <summary>
        /// Clears current input.
        /// </summary>
        private void ClearInput(object sender, RoutedEventArgs e)
        {
            if (operationExecutor.State == ExecutorState.ResultCalculated ||
                operationExecutor.State == ExecutorState.Error)
            {
                ClearAll(sender, e);
                return;
            }
            SetMainOutput(inputValidator.ClearInput());
        }

        /// <summary>
        /// Removes one character from the right of current input.
        /// </summary>
        private void RemoveCharacter(object sender, RoutedEventArgs e)
        {
            if (operationExecutor.State == ExecutorState.ResultCalculated ||
                operationExecutor.State == ExecutorState.Error)
            {
                ClearAll(sender, e);
                return;
            }
            SetMainOutput(inputValidator.ClearCharacter());
        }

        /// <summary>
        /// Sets main output label text to given.
        /// </summary>
        /// <param name="output">Text to set into main output label</param>
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
            else if (output.Length <= InputValidator.MaxInputLength)
            {
                this.MainOutput.FontSize = 28;
            }
            this.MainOutput.Content = output;
        }

        /// <summary>
        /// Sets memory indicator state.
        /// </summary>
        /// <param name="state">Current memory state (full - true or empty - false)</param>
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

        /// <summary>
        /// Sets memory state depending on pressed button.
        /// </summary>
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
                if (operationExecutor.State == ExecutorState.Error)
                {
                    return;
                }
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

                if (operationExecutor.State == ExecutorState.ResultCalculated ||
                    operationExecutor.State == ExecutorState.Error)
                {
                    SetSecondaryOutput("");
                }
            }
        }

        /// <summary>
        /// Sets secondary output label text to given.
        /// </summary>
        /// <param name="output">Text to set into secondary output label</param>
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

        /// <summary>
        /// Sets operator depending on pressed button.
        /// </summary>
        private void SetOperation(object sender, RoutedEventArgs e)
        {
            string buttonText = (sender as Button).Content.ToString();
            OperatorType operation = InputValidator.ConvertToOperatorType(buttonText);

            if (operationExecutor.State == ExecutorState.Error)
            {
                return;
            }

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
