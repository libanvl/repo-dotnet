namespace LibAnvl.Placeholder;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public static class Calculator
{
    public enum Op
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public static int Calculate(int a, int b, Op operation)
    {
        return operation switch
        {
            Op.Add => a + b,
            Op.Subtract => a - b,
            Op.Multiply => a * b,
            Op.Divide => a / b,
            _ => throw new InvalidOperationException("Invalid operation")
        };
    }
}
