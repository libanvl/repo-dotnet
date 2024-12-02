using System;
using Xunit;

namespace libanvl.Test;

public class OptTests
{
    [Fact]
    public void None_Are_SameInstance()
    {
        var x = XOpt<object>.None;
        var y = XOpt<object>.None;

        Assert.Same(x, y);
    }

    private class Base { }
    private class Derived : Base { }
    private class NotDerived { }

    [Fact]
    public void Failed_Cast_IsNone()
    {
        var a = new NotDerived().WrapOpt();
        var x = a.Cast<Base>();
        Assert.True(x.IsNone);
        Assert.False(x.IsSome);
    }

    [Fact]
    public void Cast_IsSome()
    {
        var a = new Derived().WrapOpt();
        var x = a.Cast<Base>();
        Assert.True(x.IsSome);
        Assert.False(x.IsNone);
        Assert.True(x.ValueType == typeof(Base));
    }

    [Fact]
    public void Unwrap_ThrowsForNone()
    {
        var x = XOpt<object>.None;
        Assert.Throws<InvalidOperationException>(() => x.Unwrap());
    }

    [Fact]
    public void Unwrap_DoesNotThrowForSome()
    {
        var x = Opt.Some(new object());
        x.Unwrap();
    }
}
