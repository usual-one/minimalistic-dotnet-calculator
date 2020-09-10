using System.Linq;

public class InputValidator
{
    private string value_;
    
    private static string decimalSeparator = ".";

    private static string negativeSign = "-";

    private static string defaultValue = "0";

    public string Value
    {
        get
        {
            return value_;
        }
    }

    public InputValidator()
    {
        ClearInput();
    }

    public string ChangeSign()
    {
        if (value_.StartsWith(negativeSign))
        {
            value_ = value_.TrimStart(negativeSign.ToCharArray());
        }
        else
        {
            value_ = negativeSign + value_;
        }
        return Value;
    }

    public string ClearCharacter()
    {
        value_ = value_.Remove(value_.Length - 1, 1);
        if (value_.Length == 0 || value_ == negativeSign)
        {
            ClearInput();
        } 
        return Value;
    }

    public string ClearInput()
    {
        value_ = defaultValue;
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
        if (value_.EndsWith(decimalSeparator))
        {
            value_.TrimEnd(decimalSeparator.ToCharArray());
        }
        return double.Parse(Value, System.Globalization.CultureInfo.InvariantCulture);
    }

    public bool HasDefault()
    {
        return Value == defaultValue;
    }

    public string Validate(string input)
    {
        string[] digits = {"1", "2", "3", "4", "5", "6", "7", "8", "9"};

        if (input == decimalSeparator)
        {
            if (value_ == negativeSign)
            {
                value_ += defaultValue + decimalSeparator;
            }
            else if (!value_.Contains(decimalSeparator))
            {
                value_ += decimalSeparator;
            }
            return Value;
        }

        if (input == defaultValue)
        {
            if (value_ != defaultValue &&
                value_ != negativeSign + defaultValue)
            {
                value_ += input;
            }
            return Value;
        } 
        
        if (digits.Contains(input))
        {
            if (value_ == defaultValue)
            {
                value_ = "";
            }
            value_ += input;
            return Value;
        }

        // TODO: make exception
        throw new System.Exception();
    }
}
