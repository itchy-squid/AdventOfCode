using AdventOfCode.Solutions.Day21;
using AdventOfCode.Solutions.Tools;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day21Tests
    {
        const string _input = @"Player 1 starting position: 4
Player 2 starting position: 8";

        [Fact]
        public void ProgramSolve_Problem1()
        {
            var result = Program.Solve(_input.SplitLines());
            Assert.Equal(739785, result);
        }
    }
}
