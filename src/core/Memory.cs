public class Memory
{
    public double? Value { get; set; }

    public void Clear()
    {
        Value = null;
    }

    public bool IsEmpty()
    {
        return !Value.HasValue;
    }
}
