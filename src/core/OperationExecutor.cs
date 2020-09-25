/// <summary>
/// Class that stores operands and operator, calculates value of expression.
/// </summary>
public class OperationExecutor
{
    /// <summary>
    /// Arithmetic operator.
    /// </summary>
    private OperatorType? operator_; 

    /// <summary>
    /// First operand.
    /// </summary>
    public double? FirstOperand { get; set; }

    /// <summary>
    /// Second operand.
    /// </summary>
    public double? SecondOperand { get; set; }

    /// <summary>
    /// Arithmetic operator property.
    /// </summary>
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

    /// <summary>
    /// Current state of class.
    /// </summary>
    public ExecutorState State { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public OperationExecutor()
    {
        Clear();
    }

    /// <summary>
    /// Calculates result of applying operator to operands.
    /// </summary>
    /// <returns>Calculation result</returns>
    /// <exception cref="System.NullReferenceException">Thrown in case of first operand being undefined</exception>
    public double? Calculate()
    {
        State = ExecutorState.ResultCalculated;

        if (FirstOperand == null)
        {
            throw new System.NullReferenceException();
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

    /// <summary>
    /// Clears current class state.
    /// </summary>
    public void Clear()
    {
        FirstOperand = null;
        SecondOperand = null;
        Operator = null;
        State = ExecutorState.FirstOperandInput;
    }

    /// <summary>
    /// Clears last defined operand.
    /// </summary>
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

    /// <summary>
    /// Performs arithmetic calculation depending on operator.
    /// </summary>
    /// <returns>Calculation result</returns>
    /// <exception cref="System.NotSupportedException">Thrown in case of operator is not recognized</exception>
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
            try
            {
                return ArithmeticCalculator.Divide((double)FirstOperand, (double)SecondOperand);
            }
            catch (System.DivideByZeroException)
            {
                State = ExecutorState.Error;
                return 0;
            }
        }
        else if (Operator == OperatorType.Inversion)
        {
            try
            {
                return ArithmeticCalculator.Invert((double)FirstOperand);
            }
            catch (System.DivideByZeroException)
            {
                State = ExecutorState.Error;
                return 0;
            }
        }
        else if (Operator == OperatorType.SquareRoot)
        {
            try
            {
                return ArithmeticCalculator.SquareRoot((double)FirstOperand);
            }
            catch (SquareRootOfNegativeException)
            {
                State = ExecutorState.Error;
                return 0;
            }
        }

        throw new System.NotSupportedException("unknown operation type");
    }
}
