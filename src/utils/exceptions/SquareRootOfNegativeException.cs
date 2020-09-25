/// <summary>
/// Exception that is thrown in case of taking square root of negative number.
/// </summary>
public class SquareRootOfNegativeException : System.ArithmeticException
{

    /// <summary>
    /// Constructor.
    /// </summary>
    public SquareRootOfNegativeException() : base() { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message</param>
    public SquareRootOfNegativeException(string message) : base(message) { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="inner">Exception that is the cause of this exception</param>
    public SquareRootOfNegativeException(string message, System.ArithmeticException inner) : base(message, inner) { }

}
