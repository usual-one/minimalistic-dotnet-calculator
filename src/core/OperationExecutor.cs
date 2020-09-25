public class OperationExecutor
{
    private OperatorType? operator_; 

    public double? FirstOperand { get; set; }

    public double? SecondOperand { get; set; }

    public OperatorType? Operator
    {
        get
        {
            return operator_;
        }
        set
        {
            operator_ = value;
            State = ExecutorState.OperatorGot;
        }
    } 

    public ExecutorState State { get; set; }

    public OperationExecutor()
    {
        Clear();
    }

    public double? Calculate()
    {
        State = ExecutorState.ResultCalculated;

        if (FirstOperand == null)
        {
            // TODO: throw exception
            throw new System.Exception();
        }

        if (Operator == null)
        {
            return FirstOperand;
        }

        if (Operator == OperatorType.Inversion ||
            Operator == OperatorType.SquareRoot)
        {
            return MakeArithmeticCalculation();
        }

        if (SecondOperand == null)
        {
            SecondOperand = FirstOperand;
        }

        return MakeArithmeticCalculation();
    }

    public void Clear()
    {
        FirstOperand = null;
        SecondOperand = null;
        Operator = null;
        State = ExecutorState.FirstOperandInput;
    }

    public void ClearLastOperand()
    {
        if (SecondOperand != null)
        {
            SecondOperand = null;
        }
        else
        {
            Operator = null;
            FirstOperand = null;
        }
    }

    private double MakeArithmeticCalculation()
    {
        if (Operator == OperatorType.Addition)
        {
            return ArithmeticCalculator.Add((double)FirstOperand, (double)SecondOperand);
        }
        else if (Operator == OperatorType.Subtraction)
        {
            return ArithmeticCalculator.Subtract((double)FirstOperand, (double)SecondOperand);
        }
        else if (Operator == OperatorType.Multiplication)
        {
            return ArithmeticCalculator.Multiply((double)FirstOperand, (double)SecondOperand);
        }
        else if (Operator == OperatorType.Division)
        {
            return ArithmeticCalculator.Divide((double)FirstOperand, (double)SecondOperand);
        }
        else if (Operator == OperatorType.Inversion)
        {
            return ArithmeticCalculator.Invert((double)FirstOperand);
        }
        else if (Operator == OperatorType.SquareRoot)
        {
            return ArithmeticCalculator.SquareRoot((double)this.FirstOperand);
        }

        // TODO: make exception
        throw new System.Exception();
    }
}
