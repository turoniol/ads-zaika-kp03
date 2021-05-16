
namespace BinaryTrees
{
    public enum NodeColor { Black, Red }

    public class Node
    {
        public int Value { get; set; }
        public NodeColor Color { get; set; }
        public Node Parent { get; set; }
        public RBTree Left { get; set; }
        public RBTree Right { get; set; }

        public int ChildCount()
        {
            int counter = 0;
            if (Left != null) counter += 1;
            if (Right != null) counter += 1;
            return counter;
        }
    
        public Node LeftNode() => this.Left.Root;

        public Node RightNode() => this.Right.Root;

        public bool IsBlack() => this.Color == NodeColor.Black;

        public bool IsRed() => this.Color == NodeColor.Red;

        public bool ChildMulticolored()
        {
            if (Left == null || Right == null)
            {
                return false;
            }
            var left = LeftNode();
            var right = RightNode();

            return ((left.IsBlack() && right.IsRed()) || (left.IsRed() && right.IsBlack()));
        }
    }
}