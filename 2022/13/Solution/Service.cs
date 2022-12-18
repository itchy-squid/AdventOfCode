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

        public static ListNode BuildModel(string line)
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
                        history.Push(curr);
                        curr = new ListNode();
                        history.Peek()!.Values.Add(curr);
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

            return (root.Values[0] as ListNode)!;
        }
    }

    public class Solver
    {
        public int Solve(IEnumerable<Model> models)
        {
            var modelsArray = models.ToArray();
            var sum = 0;
            for (var i = 0; i < modelsArray.Length; i++)
            {
                var m = modelsArray[i];
                // Console.Clear();
                // Console.WriteLine($" == {i} == ");
                // Console.WriteLine($"left: {m.Left.ToString()}");
                // Console.WriteLine($"right: {m.Right.ToString()}");
                // Console.WriteLine();

                var isOrdered = IsOrdered(m);
                if (isOrdered == true)
                {
                    //Console.WriteLine("Model is ordered.");
                    sum += (i + 1);
                }
                // else
                // {
                //     Console.WriteLine("Model is not ordered.");
                // }

                // Console.WriteLine();
                // Console.ReadLine();
            }

            return sum;
        }

        public bool? IsOrdered(Model m)
        {
            return -1 == Compare(m.Left, m.Right);
            // INode? left = m.Left;
            // INode? right = m.Right;

            // int leftIdx = 0;
            // int rightIdx = 0;

            // var leftHistory = new Stack<(INode?, int)>();
            // var rightHistory = new Stack<(INode?, int)>();

            // leftHistory.Push((null, 0));
            // rightHistory.Push((null, 0));

            // while (left != null)
            // {
            //     Console.WriteLine($"left index: {leftIdx}");
            //     Console.WriteLine($"right index: {rightIdx}");
            //     Console.WriteLine($"Comparing {left} to {right}");
            //     Console.WriteLine();

            //     if (right == null) return false;

            //     // If both values are integers, the lower integer should come first. If the left integer is lower than the right integer, the inputs 
            //     // are in the right order. If the left integer is higher than the right integer, the inputs are not in the right order. Otherwise, the 
            //     // inputs are the same integer; continue checking the next part of the input.
            //     if (left is IntNode lInt && right is IntNode rInt)
            //     {
            //         if (lInt.Value < rInt.Value) return true;
            //         if (lInt.Value > rInt.Value) return false;

            //         (left, leftIdx) = leftHistory.Pop();
            //         (right, rightIdx) = rightHistory.Pop();
            //         leftIdx++;
            //         rightIdx++;
            //     }

            //     // If both values are lists, compare the first value of each list, then the second value, and so on. If the left list runs out of items 
            //     // first, the inputs are in the right order. If the right list runs out of items first, the inputs are not in the right order. If the lists 
            //     // are the same length and no comparison makes a decision about the order, continue checking the next part of the input.
            //     else if (left is ListNode lList && right is ListNode rList)
            //     {
            //         var atRightEnd = rightIdx >= rList.Values.Count;
            //         var atLeftEnd = leftIdx >= lList.Values.Count;

            //         if (atLeftEnd && !atRightEnd) return true;
            //         else if (atRightEnd && !atLeftEnd) return false;
            //         else if (atLeftEnd && atRightEnd)
            //         {
            //             (left, leftIdx) = leftHistory.Pop();
            //             (right, rightIdx) = rightHistory.Pop();
            //             leftIdx++;
            //             rightIdx++;
            //         }
            //         else
            //         {
            //             leftHistory.Push((left, leftIdx));
            //             rightHistory.Push((right, rightIdx));
            //             left = lList.Values[leftIdx];
            //             right = rList.Values[rightIdx];
            //         }
            //     }

            //     // If exactly one value is an integer, convert the integer to a list which contains that integer as its only value, then retry the comparison.
            //     // For example, if comparing [0,0,0] and 2, convert the right value to [2] (a list containing 2); the result is then found by instead 
            //     // comparing [0,0,0] and [2].
            //     else if (left is IntNode leftI && right is ListNode rightL)
            //     {
            //         var temp = new ListNode();
            //         temp.Values.Add(leftI);
            //         leftHistory.Push((temp, leftIdx));
            //         left = temp;
            //         leftIdx = 0;
            //     }

            //     else if (right is IntNode rightI && left is ListNode leftL)
            //     {
            //         var temp = new ListNode();
            //         temp.Values.Add(rightI);
            //         rightHistory.Push((temp, rightIdx));
            //         right = temp;
            //         rightIdx = 0;
            //     }
            // }

            // throw new InvalidDataException("Indeterminate case?");
        }

        public int Compare(INode left, INode right)
        {
            // If both values are integers, the lower integer should come first. If the left integer is lower than the right integer, the inputs 
            // are in the right order. If the left integer is higher than the right integer, the inputs are not in the right order. Otherwise, the 
            // inputs are the same integer; continue checking the next part of the input.
            if (left is IntNode lInt && right is IntNode rInt)
            {
                return Math.Sign(lInt.Value - rInt.Value);
            }

            // If both values are lists, compare the first value of each list, then the second value, and so on. If the left list runs out of items 
            // first, the inputs are in the right order. If the right list runs out of items first, the inputs are not in the right order. If the lists 
            // are the same length and no comparison makes a decision about the order, continue checking the next part of the input.
            else if (left is ListNode lList && right is ListNode rList)
            {
                for (var i = 0; i < lList.Values.Count; i++)
                {
                    if (i >= rList.Values.Count) return 1;
                    var comparison = Compare(lList.Values[i], rList.Values[i]);
                    if (comparison != 0) return comparison;
                }

                return Math.Sign(lList.Values.Count - rList.Values.Count);
            }

            // If exactly one value is an integer, convert the integer to a list which contains that integer as its only value, then retry the comparison.
            // For example, if comparing [0,0,0] and 2, convert the right value to [2] (a list containing 2); the result is then found by instead 
            // comparing [0,0,0] and [2].
            else if (left is IntNode leftI && right is ListNode rightL)
            {
                var temp = new ListNode();
                temp.Values.Add(leftI);
                return Compare(temp, rightL);
            }

            else if (right is IntNode rightI && left is ListNode leftL)
            {
                var temp = new ListNode();
                temp.Values.Add(rightI);
                return Compare(left, temp);
            }

            throw new InvalidOperationException($"{left}\r\n{right}\r\n");
        }
    }
}