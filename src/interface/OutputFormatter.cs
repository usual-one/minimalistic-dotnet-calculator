using System.Globalization;

public class OutputFormatter
{
    private NumberFormatInfo nfi;
    public OutputFormatter()
    {
        nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";
    }
    private string BeautifyNumber(double number)
    {
        if ((int) number == number)
        {
            number = (int) number;
        }

        return number.ToString(nfi);
    }

    public string MakeMemory(double memory)
    {
        return BeautifyNumber(memory); 
    }

    public string MakeResult(double result)
    {
        return BeautifyNumber(result);
    }

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
}
