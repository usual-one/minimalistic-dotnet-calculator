using System.Globalization;

/// <summary>
/// Class that formats output for output labels.
/// </summary>
public class OutputFormatter
{
    /// <summary>
    /// Util for setting number format configuration.
    /// </summary>
    private NumberFormatInfo nfi;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public OutputFormatter()
    {
        nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";
    }

    /// <summary>
    /// Converts number to string. If double is equal to int (contains only whole part), converts it to int.
    /// </summary>
    /// <param name="number">Number to convert</param>
    /// <returns>Converted string</returns>
    private string BeautifyNumber(double number)
    {
        if ((int)number == number)
        {
            number = (int)number;
        }

        return number.ToString(nfi);
    }

    /// <summary>
    /// Beautifies given number to be shown in memory output.
    /// </summary>
    /// <param name="memory">Number to be shown in memory output</param>
    /// <returns>Beautified given number</returns>
    public string MakeMemory(double memory)
    {
        return BeautifyNumber(memory);
    }

    /// <summary>
    /// Beautifies given number to be shown in result (main) output.
    /// </summary>
    /// <param name="result">Number to be show in result (main) output</param>
    /// <returns>Beautified given number</returns>
    public string MakeResult(double result)
    {
        return BeautifyNumber(result);
    }

    /// <summary>
    /// Makes temporary expression from given expression parts.
    /// </summary>
    /// <param name="firstOperand">First operand</param>
    /// <param name="secondOperand">Second operand</param>
    /// <param name="operator_">Operator</param>
    /// <returns>Temporary expression as string</returns>
    public string MakeTempExpression(double? firstOperand, double? secondOperand, OperatorType? operator_)
    {
        if (operator_ == OperatorType.SquareRoot)
        {
            return "√" + BeautifyNumber((double)firstOperand);
        }
        else if (operator_ == OperatorType.Inversion)
        {
            return MakeTempExpression(1, firstOperand, OperatorType.Division);
        }

        string output = firstOperand.ToString();
        if (operator_ == OperatorType.Addition)
        {
            output += "+";
        }
        else if (operator_ == OperatorType.Subtraction)
        {
            output += "-";
        }
        else if (operator_ == OperatorType.Multiplication)
        {
            output += "×";
        }
        else if (operator_ == OperatorType.Division)
        {
            output += "÷";
        }

        if (operator_ == null)
        {
            return output;
        }

        if (secondOperand != null)
        {
            output += BeautifyNumber((double)secondOperand);
        }

        return output;
    }

    /// <summary>
    /// Returns error message to be shown in result (main) output.
    /// </summary>
    /// <returns>Error message as string</returns>
    public string PrintError()
    {
        return "Error";
    }
}
