using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorWinForms
{
    public partial class MainWindow : Form
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

            this.btnOne.Click += new System.EventHandler(this.AppendCharacter);
            this.btnTwo.Click += new System.EventHandler(this.AppendCharacter);
            this.btnThree.Click += new System.EventHandler(this.AppendCharacter);
            this.btnFour.Click += new System.EventHandler(this.AppendCharacter);
            this.btnFive.Click += new System.EventHandler(this.AppendCharacter);
            this.btnSix.Click += new System.EventHandler(this.AppendCharacter);
            this.btnSeven.Click += new System.EventHandler(this.AppendCharacter);
            this.btnEight.Click += new System.EventHandler(this.AppendCharacter);
            this.btnNine.Click += new System.EventHandler(this.AppendCharacter);
            this.btnZero.Click += new System.EventHandler(this.AppendCharacter);
            this.btnDot.Click += new System.EventHandler(this.AppendCharacter);

            this.btnDel.Click += new System.EventHandler(this.RemoveCharacter);
            this.btnCE.Click += new System.EventHandler(this.ClearInput);
            this.btnC.Click += new System.EventHandler(this.ClearAll);

            this.btnResult.Click += new System.EventHandler(this.CalculateResult);
            this.btnChangeSign.Click += new System.EventHandler(this.ChangeSign);

            this.btnMC.Click += new System.EventHandler(this.SetMemoryState);
            this.btnMS.Click += new System.EventHandler(this.SetMemoryState);
            this.btnMR.Click += new System.EventHandler(this.SetMemoryState);

            this.btnAdd.Click += new System.EventHandler(this.SetOperation);
            this.btnSubtract.Click += new System.EventHandler(this.SetOperation);
            this.btnMultiply.Click += new System.EventHandler(this.SetOperation);
            this.btnDivide.Click += new System.EventHandler(this.SetOperation);
            this.btnInvert.Click += new System.EventHandler(this.SetOperation);
            this.btnSquareRoot.Click += new System.EventHandler(this.SetOperation);
        }

        protected void AppendCharacter(object sender, EventArgs a)
        {
            if (operationExecutor.State == ExecutorState.OperatorGot)
            {
                inputValidator.ClearInput();
                operationExecutor.State = ExecutorState.SecondOperandInput;
            }

            string newText = inputValidator.Validate((sender as Button).Text);
            SetMainOutput(newText);
        }

        protected void CalculateResult(object sender, EventArgs a)
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

        protected void ChangeSign(object sender, EventArgs a)
        {
            SetMainOutput(inputValidator.ChangeSign());
        }

        protected void ClearAll(object sender, EventArgs a)
        {
            operationExecutor.Clear();
            SetSecondaryOutput("");
            SetMainOutput(inputValidator.ClearInput());
        }

        protected void ClearInput(object sender, EventArgs a)
        {
            if (operationExecutor.State == ExecutorState.ResultCalculated)
            {
                ClearAll(sender, a);
                return;
            }
            SetMainOutput(inputValidator.ClearInput());
        }

        protected void RemoveCharacter(object sender, EventArgs a)
        {
            if (operationExecutor.State == ExecutorState.ResultCalculated)
            {
                ClearAll(sender, a);
                return;
            }
            SetMainOutput(inputValidator.ClearCharacter());
        }

        protected void SetMainOutput(string output)
        {
            this.lblMainOutput.Text = output;
        }

        protected void SetMemoryIndicator(bool state)
        {
            if (state)
            {
                this.lblMemoryIndicator.Text = "M";
            }
            else
            {
                this.lblMemoryIndicator.Text = "";
            }
        }

        protected void SetMemoryState(object sender, EventArgs a)
        {
            string buttonText = (sender as Button).Text;
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

        protected void SetSecondaryOutput(string output)
        {
            this.lblSecondaryOutput.Text = output;
        }

        protected void SetOperation(object sender, EventArgs a)
        {
            string buttonText = (sender as Button).Text;
            OperatorType operation = InputValidator.ConvertToOperatorType(buttonText);

            if (operationExecutor.State == ExecutorState.SecondOperandInput)
            {
                CalculateResult(sender, a);
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
                CalculateResult(sender, a);
                return;
            }

            SetSecondaryOutput(outputFormatter.MakeTempExpression(operationExecutor.FirstOperand,
                                                                  operationExecutor.SecondOperand,
                                                                  operationExecutor.Operator));
        }
    }
}
