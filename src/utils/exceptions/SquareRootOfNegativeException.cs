public class SquareRootOfNegativeException : System.ArithmeticException
{

    public SquareRootOfNegativeException() : base() { }

    public SquareRootOfNegativeException(string message) : base(message) { }

    public SquareRootOfNegativeException(string message, System.ArithmeticException inner) : base(message, inner) { }

}
