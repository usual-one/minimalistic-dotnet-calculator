/// <summary>
/// Static class that performs all the arithmetic calculations.
/// </summary>
public static class ArithmeticCalculator
{
    /// <summary>
    /// Calculates sum of given operands.
    /// </summary>
    /// <param name="a">First operand</param>
    /// <param name="b">Second operand</param>
    /// <returns>Sum of given operands</returns>
    public static double Add(double a, double b)
    {
        return a + b;
    }

    /// <summary>
    /// Changes unary sign of given number.
    /// </summary>
    /// <param name="a">Number to change sing of</param>
    /// <returns>Negative number</returns>
    public static double ChangeSign(double a)
    {
        return -a;
    }

    /// <summary>
    /// Divides first operand by second operand.
    /// </summary>
    /// <param name="a">First operand</param>
    /// <param name="b">Second operand</param>
    /// <returns>Result of division of given operands</returns>
    /// <exception cref="System.DivideByZeroException">Thrown if second operand is equal to 0</exception>
    public static double Divide(double a, double b)
    {
        if (b == 0)
        {
            throw new System.DivideByZeroException();
        }
        return a / b;
    }

    /// <summary>
    /// Inverts a number i. e. divides 1 by given number.
    /// </summary>
    /// <param name="a">Number to invert</param>
    /// <returns>Inverted number</returns>
    /// <exception cref="System.DivideByZeroException">Thrown if given number is equal to 0</exception>
    public static double Invert(double a)
    {
        return Divide(1, a);
    }

    /// <summary>
    /// Multiplies first operand by second operand.
    /// </summary>
    /// <param name="a">First operand</param>
    /// <param name="b">Second operand</param>
    /// <returns>Result of multiplication of given operands</returns>
    public static double Multiply(double a, double b)
    {
        return a * b;
    }

    /// <summary>
    /// Subtracts second operand from first operand.
    /// </summary>
    /// <param name="a">First operand</param>
    /// <param name="b">Second operand</param>
    /// <returns>Result of subtraction of given operands</returns>
    public static double Subtract(double a, double b)
    {
        return a - b;
    }

    /// <summary>
    /// Calculates square root of given number.
    /// </summary>
    /// <param name="a">Number to calculate square root of</param>
    /// <returns>Square root of given number</returns>
    /// <exception cref="SquareRootOfNegativeException">Thrown if given number is less than 0</exception>
    public static double SquareRoot(double a)
    {
        if (a < 0)
        {
            throw new SquareRootOfNegativeException();
        }
        return System.Math.Sqrt(a);
    }
}
