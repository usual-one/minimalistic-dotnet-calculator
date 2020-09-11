using System;
using Gtk;

public partial class MainWindow : Gtk.Window
{
    private InputValidator inputValidator;

    private Memory memory;

    private OperationExecutor operationExecutor;

    private OutputFormatter outputFormatter;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        inputValidator = new InputValidator();
        memory = new Memory();
        operationExecutor = new OperationExecutor();
        outputFormatter = new OutputFormatter();

        Build();

        this.OneButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.TwoButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.ThreeButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.FourButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.FiveButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.SixButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.SevenButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.EightButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.NineButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.ZeroButton.Clicked += new global::System.EventHandler(this.AppendCharacter);
        this.DotButton.Clicked += new global::System.EventHandler(this.AppendCharacter);

        this.DelButton.Clicked += new global::System.EventHandler(this.RemoveCharacter);
        this.CEButton.Clicked += new global::System.EventHandler(this.ClearInput);
        this.CButton.Clicked += new global::System.EventHandler(this.ClearAll);
        
        this.ResultButton.Clicked += new global::System.EventHandler(this.CalculateResult);
        this.ChangeSignButton.Clicked += new global::System.EventHandler(this.ChangeSign);

        this.MCButton.Clicked += new global::System.EventHandler(this.SetMemoryState);
        this.MSButton.Clicked += new global::System.EventHandler(this.SetMemoryState);
        this.MRButton.Clicked += new global::System.EventHandler(this.SetMemoryState);

        this.AddButton.Clicked += new global::System.EventHandler(this.SetOperation);
        this.SubtractButton.Clicked += new global::System.EventHandler(this.SetOperation);
        this.MultiplyButton.Clicked += new global::System.EventHandler(this.SetOperation);
        this.DivideButton.Clicked += new global::System.EventHandler(this.SetOperation);
        this.InvertButton.Clicked += new global::System.EventHandler(this.SetOperation);
        this.SquareRootButton.Clicked += new global::System.EventHandler(this.SetOperation);
    }

    protected void AppendCharacter(object sender, EventArgs a)
    {
        if (operationExecutor.State == ExecutorState.OperatorGot) {
            inputValidator.ClearInput();
            operationExecutor.State = ExecutorState.SecondOperandInput;
        }

        string newText = inputValidator.Validate((sender as Button).Label);
        SetMainOutput(newText);
    }

    protected void CalculateResult(object sender, EventArgs a)
    {
        if (operationExecutor.State == ExecutorState.FirstOperandInput) {
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

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
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
        this.MainOutput.Text = output;
    }

    protected void SetMemoryIndicator(bool state)
    {
        if (state)
        {
            this.MemoryIndicator.Text = "M";
        }
        else
        {
            this.MemoryIndicator.Text = "";
        }
    }

    protected void SetMemoryState(object sender, EventArgs a)
    {
        string buttonText = (sender as Button).Label;
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
        this.SecondaryOutput.Text = output;
    }

    protected void SetOperation(object sender, EventArgs a)
    {
        string buttonText = (sender as Button).Label;
        OperatorType operation = InputValidator.ConvertToOperatorType(buttonText);

        if (operationExecutor.State == ExecutorState.SecondOperandInput)
        {
            return;
        } 
        
        if (operationExecutor.State == ExecutorState.FirstOperandInput) {
            operationExecutor.FirstOperand = inputValidator.getInput();
        }

        if (operationExecutor.State == ExecutorState.ResultCalculated) {
            operationExecutor.SecondOperand = null;
            operationExecutor.State = ExecutorState.OperatorGot;
        }

        operationExecutor.Operator = operation;
        SetSecondaryOutput(outputFormatter.MakeTempExpression(operationExecutor.FirstOperand,
                                                              operationExecutor.SecondOperand,
                                                              operationExecutor.Operator));
    }
}
