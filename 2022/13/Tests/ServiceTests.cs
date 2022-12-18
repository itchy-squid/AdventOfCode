using System.Linq;
using Xunit;
namespace AdventOfCode.Day13.Tests;

public class ServiceTests
{
    [Theory]
    [InlineData("[1,1,3,1]", "[1,1,5,1,1]")]
    [InlineData("[1,1,3,1,1]", "[1,1,5,1,1]")]
    [InlineData("[[1],[2,3,4]]", "[[1],4]")]
    public void Inputs_AreOrdered(params string[] lines)
    {
        var model = Parser.BuildModel(lines);
        Assert.True(new Solver().IsOrdered(model.ElementAt(0)));
    }

    [Theory]
    [InlineData("[1,1,3,1,1]", "[1,1,5,1]")]
    [InlineData("[1,1,5,1,1]", "[1,1,3,1,1]")]
    [InlineData("[9]", "[[8,7,6]]")]
    public void Inputs_AreNotOrdered(params string[] lines)
    {
        var model = Parser.BuildModel(lines);
        Assert.False(new Solver().IsOrdered(model.ElementAt(0)));
    }
}