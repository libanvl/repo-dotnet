using System;
using Xunit;

namespace libanvl.Test;

public class ResultTests
{
    [Fact]
    public void Ok_Result_Should_Be_Success()
    {
        var result = new Result<int, string>(42);
        Assert.True(result.IsOk);
        Assert.False(result.IsErr);
        Assert.Equal(42, result.Unwrap());
    }

    [Fact]
    public void Err_Result_Should_Be_Error()
    {
        var result = new Result<int, string>("error");
        Assert.False(result.IsOk);
        Assert.True(result.IsErr);
        Assert.Throws<InvalidOperationException>(() => result.Unwrap());
    }

    [Fact]
    public void OkOr_Should_Return_Value_If_Success()
    {
        var result = new Result<int, string>(42);
        Assert.Equal(42, result.OkOr(0));
    }

    [Fact]
    public void OkOr_Should_Return_Default_If_Error()
    {
        var result = new Result<int, string>("error");
        Assert.Equal(0, result.OkOr(0));
    }

    [Fact]
    public void Match_Should_Invoke_Ok_Action_If_Success()
    {
        var result = new Result<int, string>(42);
        bool okCalled = false;
        bool errCalled = false;
        result.Match(
            ok => okCalled = true,
            err => errCalled = true
        );
        Assert.True(okCalled);
        Assert.False(errCalled);
    }

    [Fact]
    public void Match_Should_Invoke_Err_Action_If_Error()
    {
        var result = new Result<int, string>("error");
        bool okCalled = false;
        bool errCalled = false;
        result.Match(
            ok => okCalled = true,
            err => errCalled = true
        );
        Assert.False(okCalled);
        Assert.True(errCalled);
    }

    [Fact]
    public void Match_Function_Should_Return_Ok_Result_If_Success()
    {
        var result = new Result<int, string>(42);
        var matchResult = result.Match(
            ok => ok.ToString(),
            err => err
        );
        Assert.Equal("42", matchResult);
    }

    [Fact]
    public void Match_Function_Should_Return_Err_Result_If_Error()
    {
        var result = new Result<int, string>("error");
        var matchResult = result.Match(
            ok => ok.ToString(),
            err => err
        );
        Assert.Equal("error", matchResult);
    }
}
