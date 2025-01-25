using LibAnvl.Placeholder;

namespace LibAnvl.Placeholder.Tests;

public class CalculatorTest
{
    [Fact]
    public void Calculate_Add_ReturnsCorrectResult()
    {
        int result = Calculator.Calculate(2, 3, Calculator.Op.Add);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Calculate_Subtract_ReturnsCorrectResult()
    {
        int result = Calculator.Calculate(5, 3, Calculator.Op.Subtract);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Calculate_Multiply_ReturnsCorrectResult()
    {
        int result = Calculator.Calculate(2, 3, Calculator.Op.Multiply);
        Assert.Equal(6, result);
    }

    [Fact]
    public void Calculate_Divide_ReturnsCorrectResult()
    {
        int result = Calculator.Calculate(6, 3, Calculator.Op.Divide);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Calculate_DivideByZero_ThrowsException()
    {
        Assert.Throws<DivideByZeroException>(() => Calculator.Calculate(6, 0, Calculator.Op.Divide));
    }

    [Fact]
    public void Calculate_InvalidOperation_ThrowsException()
    {
        Assert.Throws<InvalidOperationException>(() => Calculator.Calculate(6, 3, (Calculator.Op)999));
    }
}