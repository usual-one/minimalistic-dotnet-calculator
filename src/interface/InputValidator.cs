using System.Linq;
using System.Globalization;

public class InputValidator
{   
    private static string decimalSeparator = ".";

    private static string negativeSign = "-";

    private static string defaultValue = "0";

    public string Value { get; set; }

    public InputValidator()
    {
        ClearInput();
    }

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

    public string ClearCharacter()
    {
        Value = Value.Remove(Value.Length - 1, 1);
        if (Value.Length == 0 || Value == negativeSign)
        {
            ClearInput();
        } 
        return Value;
    }

    public string ClearInput()
    {
        Value = defaultValue;
        return Value;
    }

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

        // TODO: make exception
        throw new System.Exception();
    }

    public double getInput()
    {
        if (Value.EndsWith(decimalSeparator))
        {
            Value.TrimEnd(decimalSeparator.ToCharArray());
        }
        return double.Parse(Value, System.Globalization.CultureInfo.InvariantCulture);
    }

    public bool HasDefault()
    {
        return Value == defaultValue;
    }

    public string Validate(string input)
    {
        if (Value.Length >= 20)
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

        // TODO: make exception
        throw new System.Exception();
    }
}
