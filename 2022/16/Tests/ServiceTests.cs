using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace AdventOfCode.Day13.Tests;

public class NodeComparerTests
{
    // [Theory]
    // [InlineData("[1,1,3,1]", "[1,1,5,1,1]")]
    // [InlineData("[1,1,3,1,1]", "[1,1,5,1,1]")]
    // [InlineData("[[1],[2,3,4]]", "[[1],4]")]
    // [InlineData("[[4,4],4,4]", "[[4,4],4,4,4]")]
    // [InlineData("[]", "[3]")]
    // public void Inputs_AreOrdered(params string[] lines)
    // {
    //     var model = Parser.BuildModel(lines).ToArray();
    //     Assert.Equal(-1, new NodeComparer().Compare(model[0], model[1]));
    // }

    // [Theory]
    // [InlineData("[1,1,5,1,1]", "[1,1,3,1,1]")]
    // [InlineData("[9]", "[[8,7,6]]")]
    // [InlineData("[7,7,7,7]", "[7,7,7]")]
    // [InlineData("[[[]]]", "[[]]")]
    // [InlineData("[1,[2,[3,[4,[5,6,7]]]],8,9]", "[1,[2,[3,[4,[5,6,0]]]],8,9]")]
    // public void Inputs_AreNotOrdered(params string[] lines)
    // {
    //     var model = Parser.BuildModel(lines).ToArray();
    //     Assert.Equal(1, new NodeComparer().Compare(model[0], model[1]));
    // }

    // [Theory]
    // [SampleFileData("sample.txt", 13)]
    // public void SampleFile_Problem1_MatchesGivenSolution(string[] lines, int expected)
    // {
    //     var model = Parser.BuildModel(lines).ToArray();
    //     var received = new Problem1().Solve(model);
    //     Assert.Equal(expected, received);
    // }

    // [Theory]
    // [SampleFileData("sample.txt", 140)]
    // public void SampleFile_Problem2_MatchesGivenSolution(string[] lines, int expected)
    // {
    //     var model = Parser.BuildModel(lines).ToArray();
    //     var received = new Problem2().Solve(model);
    //     Assert.Equal(expected, received);
    // }

    public class SampleFileDataAttribute : DataAttribute
    {
        private readonly string _filePath;
        private readonly int _solution;

        public SampleFileDataAttribute(string filePath, int solution)
        {
            _filePath = filePath;
            _solution = solution;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { (object)(File.ReadAllLines(_filePath)), _solution };
        }
    }
}