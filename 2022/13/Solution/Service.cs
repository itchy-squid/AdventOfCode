namespace AdventOfCode.Day13
{
    public class Parser
    {
        public static IEnumerable<Model> BuildModel(string[] lines)
        {
            for (int i = 0; i < lines.Length; i += 3)
            {
                yield return new Model(BuildModel(lines[i]), BuildModel(lines[i + 1]));
            }
        }

        public static INode BuildModel(string line)
        {
            var root = new ListNode();
            var curr = root;

            var history = new Stack<ListNode?>();
            history.Push(null);

            for (var i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case '[':
                        var temp = new ListNode();
                        curr!.Values.Add(temp);
                        curr = temp;
                        break;

                    case ',': break;

                    case ']':
                        curr = history.Pop();
                        break;

                    default:
                        var value = new String(line.Substring(i).TakeWhile(c => char.IsDigit(c)).ToArray());
                        curr!.Values.Add(new IntNode(Int32.Parse(value)));
                        break;
                }
            }

            return root.Values[0];
        }
    }

    public class Solver
    {
        public int Solve(IEnumerable<Model> models)
        {
            foreach (var m in models)
            {
                Console.WriteLine(m.Left.ToString());
                Console.WriteLine(m.Right.ToString());
                Console.WriteLine();
            }

            return models.Select((m, i) => IsOrdered(m) ? i + 1 : 0).Sum();
        }

        public bool IsOrdered(Model m)
        {
            INode? left = m.Left;
            INode? right = m.Right;

            int leftIdx = 0;
            int rightIdx = 0;

            var leftHistory = new Stack<(INode?, int)>();
            var rightHistory = new Stack<(INode?, int)>();

            leftHistory.Push((null, 0));
            rightHistory.Push((null, 0));

            while (left != null)
            {
                Console.WriteLine($"left index: {leftIdx}");
                Console.WriteLine($"right index: {rightIdx}");
                Console.WriteLine($"Comparing {left} to {right}");
                Console.WriteLine();

                if (right == null) return false;

                // If both values are integers, the lower integer should come first. If the left integer is lower than the right integer, the inputs 
                // are in the right order. If the left integer is higher than the right integer, the inputs are not in the right order. Otherwise, the 
                // inputs are the same integer; continue checking the next part of the input.
                if (left is IntNode lInt && right is IntNode rInt)
                {
                    if (rInt.Value < lInt.Value) return false;

                    (left, leftIdx) = leftHistory.Pop();
                    (right, rightIdx) = rightHistory.Pop();
                    leftIdx++;
                    rightIdx++;
                }

                // If both values are lists, compare the first value of each list, then the second value, and so on. If the left list runs out of items 
                // first, the inputs are in the right order. If the right list runs out of items first, the inputs are not in the right order. If the lists 
                // are the same length and no comparison makes a decision about the order, continue checking the next part of the input.
                else if (left is ListNode lList && right is ListNode rList)
                {
                    var atRightEnd = rightIdx >= rList.Values.Count;
                    var atLeftEnd = leftIdx >= lList.Values.Count;

                    if (atRightEnd && !atLeftEnd) return false;

                    if (atLeftEnd)
                    {
                        (left, leftIdx) = leftHistory.Pop();
                        (right, rightIdx) = rightHistory.Pop();
                        leftIdx++;
                        rightIdx++;
                    }
                    else
                    {
                        leftHistory.Push((left, leftIdx));
                        rightHistory.Push((right, rightIdx));
                        left = lList.Values[leftIdx];
                        right = rList.Values[rightIdx];
                    }
                }

                // If exactly one value is an integer, convert the integer to a list which contains that integer as its only value, then retry the comparison.
                // For example, if comparing [0,0,0] and 2, convert the right value to [2] (a list containing 2); the result is then found by instead 
                // comparing [0,0,0] and [2].
                else
                {
                    if (left is IntNode leftI && right is not IntNode)
                    {
                        var temp = new ListNode();
                        temp.Values.Add(leftI);
                        leftHistory.Push((temp, 0));
                        left = temp;
                    }
                    else if (right is IntNode rightI && left is not IntNode)
                    {
                        var temp = new ListNode();
                        temp.Values.Add(rightI);
                        rightHistory.Push((temp, 0));
                        right = temp;
                    }
                }
            }

            return true;
        }
    }
}