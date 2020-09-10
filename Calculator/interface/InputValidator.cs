using System.Linq;

public class InputValidator
{
    private string value_;

    private static char negativeSign = '-';

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
        clearInput();
    }

    public string hangeSign()
    {
        if (value_[0] == negativeSign)
        {
            value_ = value_.TrimStart(negativeSign);
        }
        else
        {
            value_ = negativeSign.ToString() + value_;
        }
        return Value;
    }

    public string clearCharacter()
    {
        value_ = value_.TrimEnd(value_[value_.Length - 1]);
        if (value_.Length == 0 || value_ == negativeSign.ToString())
        {
            clearInput();
        } 
        return Value;
    }

    public string clearInput()
    {
        value_ = defaultValue;
        return Value;
    }

    public string validate(char character)
    {
        char decimalSeparator = '.';
        char[] digits = {'1', '2', '3', '4', '5', '6', '7', '8', '9'};

        if (character == decimalSeparator)
        {
            if (value_.Contains(decimalSeparator.ToString()))
            {
                return Value;
            }
            if (value_ == negativeSign.ToString())
            {
                value_ += defaultValue + decimalSeparator.ToString();
                return Value;
            }
        }

        if (character == defaultValue[0] && defaultValue.Length == 1)
        {
            if (value_ == defaultValue ||
                value_ == negativeSign.ToString() + defaultValue)
            {
                return Value;
            }
        } 

        if (character == negativeSign)
        {
            if (value_ == defaultValue)
            {
                value_ = negativeSign.ToString();    
                return Value;
            }
        }
        
        if (digits.Contains(character))
        {
            value_ += character.ToString();
            return Value;
        }

        // TODO: make exception
        throw new System.Exception();
    }
}
