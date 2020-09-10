using System;

public static class ArithmeticCalculator
{
    public static double Add(double a, double b)
    {
        return a + b;
    }

    public static double ChangeSign(double a)
    {
        return -a;
    }

    public static double Divide(double a, double b)
    {
        return a / b;
    }

    public static double Invert(double a)
    {
        return 1 / a;
    }

    public static double Multiply(double a, double b)
    {
        return a * b;
    }

    public static double Subtract(double a, double b)
    {
        return a - b;
    }

    public static double SquareRoot(double a)
    {
        return Math.Sqrt(a);
    }
}
