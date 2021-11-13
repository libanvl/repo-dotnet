using Xunit;

namespace libanvl.Test;

public class WrapExtensionsTests
{
    public class WrapOpt_T
    {
        [Fact]
        public void Returns_None_For_Null()
        {
            object? a = null;
            var x = a.WrapOpt();

            Assert.True(x.IsNone);
            Assert.False(x.IsSome);
        }

        [Fact]
        public void Returns_Some_For_NonNull()
        {
            object? a = new object();
            var x = a.WrapOpt();

            Assert.True(x.IsSome);
            Assert.False(x.IsNone);
        }
    }

    public class WrapOpt_T_U
    {
        [Fact]
        public void Returns_None_For_Projected_Null()
        {
            var a = 5;
            var x = a.WrapOpt<int, object>(_ => null!);
            Assert.True(x.IsNone);
            Assert.False(x.IsSome);
        }

        [Fact]
        public void Returns_Some_For_Projected()
        {
            var a = 5;
            var x = a.WrapOpt(i => i != 5);
            Assert.True(x.IsSome);
            Assert.False(x.IsNone);
            Assert.IsType<bool>(x.Unwrap());
        }
    }

    public class WrapOpt_string
    {
        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("\t", true)]
        [InlineData(" ", true)]
        [InlineData("\n", true)]
        public void Returns_None(string? a, bool whitespaceIsNone)
        {
            var x = a.WrapOpt(whitespaceIsNone);
            Assert.True(x.IsNone);
            Assert.False(x.IsSome);
        }

        [Theory]
        [InlineData("test string", true)]
        [InlineData("test string", false)]
        [InlineData("\t", false)]
        [InlineData(" ", false)]
        [InlineData("\n", false)]
        public void Returns_Some(string? a, bool whitespaceIsNone)
        {
            var x = a.WrapOpt(whitespaceIsNone);
            Assert.True(x.IsSome);
            Assert.False(x.IsNone);
        }
    }
}
