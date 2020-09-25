using System.Linq;
using System.Globalization;

/// <summary>
/// Buffer that contains current input in its state and validates it.
/// </summary>
public class InputValidator
{
    /// <summary>
    /// Maximum valid input length.
    /// </summary>
    public static int MaxInputLength { get; set; }

    /// <summary>
    /// Decimal fractional separator.
    /// </summary>
    private static string decimalSeparator = ".";

    /// <summary>
    /// Negative sign.
    /// </summary>
    private static string negativeSign = "-";

    /// <summary>
    /// Default input value (state).
    /// </summary>
    private static string defaultValue = "0";

    /// <summary>
    /// Current value (state) of input.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public InputValidator()
    {
        ClearInput();
        MaxInputLength = 20;
    }

    /// <summary>
    /// Changes sign of current input.
    /// </summary>
    /// <returns>Updated input value</returns>
    public string ChangeSign()
    {
        if (Value.StartsWith(negativeSign))
        {
            Value = Value.TrimStart(negativeSign.ToCharArray());
        }
        else
        {
            Value = negativeSign + Value;
        }
        return Value;
    }

    /// <summary>
    /// Clears last right character from input. If input contained one-digit number, sets it to default value;
    /// </summary>
    /// <returns>Updated input value</returns>
    public string ClearCharacter()
    {
        Value = Value.Remove(Value.Length - 1, 1);
        if (Value.Length == 0 || Value == negativeSign)
        {
            ClearInput();
        } 
        return Value;
    }

    /// <summary>
    /// Sets value to its default value;
    /// </summary>
    /// <returns>Updated input value</returns>
    public string ClearInput()
    {
        Value = defaultValue;
        return Value;
    }

    /// <summary>
    /// Converts string representation of operator to OperatorType.
    /// </summary>
    /// <param name="operatorText">String representation of operator</param>
    /// <returns>Operator as OperatorType</returns>
    /// <exception cref="System.NotSupportedException">Thrown in case of string is not recognized as operator</exception>
    public static OperatorType ConvertToOperatorType(string operatorText)
    {
        if (operatorText == "+")
        {
            return OperatorType.Addition;
        }
        else if (operatorText == "-")
        {
            return OperatorType.Subtraction;
        }
        else if (operatorText == "×")
        {
            return OperatorType.Multiplication;
        }
        else if (operatorText == "÷")
        {
            return OperatorType.Division;
        }
        else if (operatorText == "1/x")
        {
            return OperatorType.Inversion;
        }
        else if (operatorText == "√")
        {
            return OperatorType.SquareRoot;
        }

        throw new System.NotSupportedException("unknown operation type");
    }

    /// <summary>
    /// Gets input value as double.
    /// </summary>
    /// <returns>Input value as double</returns>
    public double getInput()
    {
        if (Value.EndsWith(decimalSeparator))
        {
            Value.TrimEnd(decimalSeparator.ToCharArray());
        }
        return double.Parse(Value, System.Globalization.CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Checks if input is in its default state.
    /// </summary>
    /// <returns>True if value is equal to default value else false</returns>
    public bool HasDefault()
    {
        return Value == defaultValue;
    }

    /// <summary>
    /// Appends digits or decimal fraction separator characters to input if it becomes valid afterwards.
    /// </summary>
    /// <param name="input">Characters to append</param>
    /// <returns>Updated input value</returns>
    /// <exception cref="System.NotSupportedException">Thrown if given characters is not digit or decimal fraction separator.</exception>
    public string Validate(string input)
    {
        if (Value.Length >= MaxInputLength)
        {
            return Value;
        }

        string[] digits = {"1", "2", "3", "4", "5", "6", "7", "8", "9"};

        if (input == decimalSeparator)
        {
            if (Value == negativeSign)
            {
                Value += defaultValue + decimalSeparator;
            }
            else if (!Value.Contains(decimalSeparator))
            {
                Value += decimalSeparator;
            }
            return Value;
        }

        if (input == defaultValue)
        {
            if (Value != defaultValue &&
                Value != negativeSign + defaultValue)
            {
                Value += input;
            }
            return Value;
        } 
        
        if (digits.Contains(input))
        {
            if (Value == defaultValue || Value == negativeSign + defaultValue)
            {
                Value = Value.TrimEnd(defaultValue.ToCharArray());
            }
            Value += input;
            return Value;
        }

        throw new System.NotSupportedException("unknown input type");
    }
}
