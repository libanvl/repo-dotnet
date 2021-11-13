using Xunit;
using System.Collections.Generic;

namespace libanvl.Test;

public class OptExtensionsTests
{
 
    [Fact]
    public void SomeOrEmptyTest_string()
    {
        var x = None.String;
        Assert.Equal(string.Empty, x.SomeOrEmpty());
    }

    [Fact]
    public void SomeOrEmptyTest_enumerable()
    {
        var x = Opt<IEnumerable<object>>.None;
        Assert.Empty(x.SomeOrEmpty());
    }

    [Fact]
    public void OptObjectNone_SomeOrNull_IsNull()
    {
        var x = Opt<object>.None;
        Assert.Null(x.SomeOrNull());
    }

    [Fact]
    public void OptList_SomeOrEmpty_IsAssignableFrom_IEnumerable()
    {
        var x = Opt<List<object>>.None;
        Assert.IsAssignableFrom<IEnumerable<object>>(x.SomeOrEmpty());
    }

    [Fact]
    public void OptList_Some_IsIterable()
    {
        var x = new Opt<List<object>>.Some(new List<object>());
        Assert.IsAssignableFrom<IEnumerator<object>>(x.GetEnumerator());
    }

    [Fact]
    public void OptList_None_IsIterable()
    {
        var x = Opt<List<object>>.None;
        Assert.IsAssignableFrom<IEnumerator<object>>(x.GetEnumerator());
    }

}
