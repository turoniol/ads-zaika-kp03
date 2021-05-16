using System;

namespace BinaryTrees
{
    public class RBTree
    {
        public Node Root;

        public bool IsEmpty() => Root == null;

        public bool HaveLeft() => Root.Left != null;

        public bool HaveRight() => Root.Right != null;

        public void Insert(int value)
        {
            Node node = new Node
            {
                Value = value,
                Color = NodeColor.Red,
            };
            InsertAsLeaf(node); 
            FixNode(node);
        }

        private void InsertAsLeaf(Node node)
        {
            if (Root == null)
            {
                Root = node;
                return;
            }

            node.Parent = Root;

            var value = node.Value;
            var rvalue = Root.Value;

            if (value == rvalue)
            {
                Root.Value = node.Value;
            }
            else if (value > rvalue)
            {
                if (Root.Right == null)
                {
                    Root.Right = new RBTree();
                }
                Root.Right.InsertAsLeaf(node);
            }
            else if (value < rvalue)
            {
                if (Root.Left == null)
                {
                    Root.Left = new RBTree();
                }
                Root.Left.InsertAsLeaf(node);
            }
        }
    
        private void FixNode(Node node)
        {
            if (IsEmpty()) throw new Exception("The tree is empty");

            var parent = node.Parent;

            if (parent == null)
            {
                node.Color = NodeColor.Black;
                Console.WriteLine($"Change root[{node.Value}] color to black.");
                return;
            }
            
            if (parent.IsRed())
            {
                var grandpa = parent.Parent;
                if (grandpa == null)
                {
                    return;
                }
                switch (grandpa.ChildCount())
                {
                    case 1:
                        OnRotating(node);
                        break;
                    case 2:
                        On2Child(node);
                        break;
                }
            }
        }

        private void OnRotating(Node node)
        {
            var parent = node.Parent;
            if (parent == null) return;

            var grandpa = parent.Parent;
            if (grandpa == null) return;

            if (grandpa.Left != null && grandpa.LeftNode().IsRed())
            {
                if (parent.Left != null && parent.LeftNode().IsRed())
                {
                    RotateLeft(grandpa);
                }
                else if (parent.Right != null && parent.RightNode().IsRed())
                {
                    DoubleRotateLeft(grandpa);
                }
            }
            else if (grandpa.RightNode().IsRed())
            {
                if (parent.Right != null && parent.RightNode().IsRed())
                {
                    RotateRight(grandpa);
                }
                else if (parent.Left != null && parent.LeftNode().IsRed())
                {
                    DoubleRotateRight(grandpa);
                }
            }
        }

        private void On2Child(Node node)
        {
            var parent = node.Parent;
            var grandpa = parent.Parent;

            if (grandpa.LeftNode().IsRed() && grandpa.RightNode().IsRed())
            {
                SwapColoring(grandpa);
            }
            else if (grandpa.ChildMulticolored())
            {
                OnRotating(node);
            }

            FixNode(node.Parent);
        }

        private void RotateLeft(Node node)
        {
            Console.WriteLine($"Left rotate node [{node.Value}]");
            var child = node.LeftNode();

            var right = node.Right;
            var childLeft = child.Left;
            node.Right = new RBTree {
                Root = new Node {
                    Value = node.Value, 
                    Color = NodeColor.Red, 
                    Parent = node,
                    Right = right,
                    Left = child.Right,
                    },
            };
            node.Left = childLeft;

            // parents
            if (right != null)
            {
                right.Root.Parent = node.RightNode();
            }
            if (child.Right != null)
            {
                child.Right.Root.Parent = node.Right.Root;
            }
            if (childLeft != null)
            {
                childLeft.Root.Parent = node;
            }

            node.Value = child.Value;
            node.Color = NodeColor.Black;
        }

        private void RotateRight(Node node)
        {
            Console.WriteLine($"Right rotate node [{node.Value}]");
            var child = node.RightNode();

            var left = node.Left;
            var childRight = child.Right;
            node.Left = new RBTree() {
                Root = new Node {
                    Value = node.Value, 
                    Color = NodeColor.Red, 
                    Parent = node,
                    Left = left,
                    Right = child.Left,
                },
            };
            node.Right = childRight;

            // parents
            if (left != null)
            {
                left.Root.Parent = node.RightNode();
            }
            if (child.Left != null)
            {
                child.Left.Root.Parent = node.Left.Root;
            }
            if (childRight != null)
            {
                childRight.Root.Parent = node;
            }

            node.Value = child.Value;
            node.Color = NodeColor.Black;
        }
    
        private void DoubleRotateLeft(Node node)
        {
            Console.WriteLine($"Double left rotate node [{node.Value}]");
            var child = node.LeftNode();
            var childChild = child.RightNode();

            var rchild = node.Right;
            node.Right = new RBTree {
                Root = new Node { 
                    Value = node.Value, 
                    Color = NodeColor.Red, 
                    Parent = node,
                    Right = rchild,
                    Left = childChild.Right,
                    },
            };

            child.Right = childChild.Left;

            // parents
            if (rchild != null)
            {
                rchild.Root.Parent = node;
            }
            if (childChild.Right != null)
            {
                childChild.Right.Root.Parent = node.Right.Root;
            }
            if (childChild.Left != null)
            {
                childChild.Left.Root.Parent = child;
            }

            node.Color = NodeColor.Black;
            node.Value = childChild.Value;
        }

        private void DoubleRotateRight(Node node)
        {
            Console.WriteLine($"Double right rotate node [{node.Value}]");
            var child = node.RightNode();
            var childChild = child.LeftNode();

            var lchild = node.Left;
            node.Left = new RBTree {
                Root = new Node {
                    Value = node.Value, 
                    Color = NodeColor.Red, 
                    Parent = node,
                    Left = lchild,
                    Right = childChild.Left,
                    },
            };
            child.Left = childChild.Right;

            // parents
            if (lchild != null)
            {
                lchild.Root.Parent = node;
            }
            if (childChild.Left != null)
            {
                childChild.Left.Root.Parent = node.Left.Root;
            }
            if (childChild.Right != null)
            {
                childChild.Right.Root.Parent = child;
            }

            node.Color = NodeColor.Black;
            node.Value = childChild.Value;
        }
    
        private void SwapColoring(Node node)
        {
            var lchild = node.Left;
            var rchild = node.Right;

            node.Color = node.Color == NodeColor.Black ? NodeColor.Red : NodeColor.Black;
            if (node.Parent == null) node.Color = NodeColor.Black;
            Console.WriteLine($"Change coloring nodes [{node.Value}], [{lchild.Root.Value}], [{rchild.Root.Value}]");
            if (lchild != null)
            {
                lchild.Root.Color = lchild.Root.Color == NodeColor.Black ? NodeColor.Red : NodeColor.Black;
            }
            if (rchild != null)
            {
                rchild.Root.Color = rchild.Root.Color == NodeColor.Black ? NodeColor.Red : NodeColor.Black;
            }
        }
    }
}