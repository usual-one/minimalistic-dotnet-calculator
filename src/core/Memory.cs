/// <summary>
/// Memory buffer.
/// </summary>
public class Memory
{
    /// <summary>
    /// Current value.
    /// </summary>
    public double? Value { get; set; }

    /// <summary>
    /// Sets current value to undefined state.
    /// </summary>
    public void Clear()
    {
        Value = null;
    }

    /// <summary>
    /// Checks if current value is in undefined state.
    /// </summary>
    /// <returns>True if current value is equal to null else false</returns>
    public bool IsEmpty()
    {
        return !Value.HasValue;
    }
}
