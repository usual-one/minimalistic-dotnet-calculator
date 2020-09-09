public class Memory
{
    private double? Value_;

    public void Clear()
    {
        this.Value_ = null;
    }

    public double Get()
    {
        return this.Value_.Value;
    }

    public bool IsEmpty()
    {
        return !this.Value_.HasValue;
    }

    public void Set(double value_)
    {
        this.Value_ = value_;
    }
}
