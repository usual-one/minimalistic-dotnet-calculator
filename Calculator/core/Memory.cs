public class Memory
{
    private double? value_;

    public double? Value
    {
        get
        {
            return value_.Value;
        }
        set
        {
            value_ = value;
        }
    }

    public void Clear()
    {
        Value = null;
    }

    public bool IsEmpty()
    {
        return !value_.HasValue;
    }
}
