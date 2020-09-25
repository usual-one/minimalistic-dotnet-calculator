/// <summary>
/// Executor state type enum.
/// </summary>
public enum ExecutorState
{
    /// <summary>
    /// Waiting for first operand input. Ends when operator is got.
    /// </summary>
    FirstOperandInput,
    /// <summary>
    /// Sets when operator is got. Waiting for operator change or starting second operand input.
    /// </summary>
    OperatorGot,
    /// <summary>
    /// Waiting for second operand input. Ends when result is requested.
    /// </summary>
    SecondOperandInput,
    /// <summary>
    /// Sets when result is calculated. Ends when class is cleared or new input started.
    /// </summary>
    ResultCalculated,
    /// <summary>
    /// Sets when arithmetic error occurred while calculating result. Ends when class is cleared or new input started.
    /// </summary>
    Error
}
