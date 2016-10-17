namespace AvlTreeLab
{
    using System;
    
    public class AvlTree<T> where T : IComparable
    {
        private Node<T> root;

        public int Count { get; private set; }
        
        public void Add(T item)
        {
            var inserted = true;
            if (this.root == null)
            {
                this.root = new Node<T>(item);
            }
            else
            {
                inserted = this.InsertInternal(this.root, item);
            }
            if (inserted)
            {
                this.Count++;
            }
        }

        public bool Contains(T item)
        {
            var currentNode = this.root;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(item))
                {
                    return true;
                }

                else if (currentNode.Value.CompareTo(item) > 0)
                {
                    currentNode = currentNode.LeftChild;
                }

                else
                {
                    currentNode = currentNode.RightChild;
                }
            }

            return false;
        }

        public void ForeachDfs(Action<int, T> action)
        {
            if (this.Count == 0)
            {
                return;
            }
            
            this.InOrderDfs(this.root, 1, action);
        }

        private void InOrderDfs(Node<T> node, int depth, Action<int, T> action)
        {
            if (node.LeftChild != null)
            {
                this.InOrderDfs(node.LeftChild, depth + 1, action);
            }

            action(depth, node.Value);

            if (node.RightChild != null)
            {
                this.InOrderDfs(node.RightChild, depth + 1, action);
            }
        }

        private bool InsertInternal(Node<T> node, T value)
        {
            var currentNode = node;
            var newNode = new Node<T>(value);
            var shouldRetrace = false;
            while (true)
            {
                if (currentNode.Value.CompareTo(value) > 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.BalanceFactor++;

                        newNode.Parent = currentNode;
                        currentNode.LeftChild = newNode;
                        break;
                    }
                    else
                    {
                        currentNode = currentNode.LeftChild;
                    }                    
                }
                else if (currentNode.Value.CompareTo(value) < 0)
                {
                    if (currentNode.RightChild == null)
                    {
                        currentNode.BalanceFactor--;

                        newNode.Parent = currentNode;
                        currentNode.RightChild = newNode;
                        break;
                    }
                    else
                    {
                        currentNode = currentNode.RightChild;
                    }
                }
                else
                {
                    return false;
                }
            }

            if (currentNode != this.root)
            {
                if (currentNode.IsLeftChild && currentNode.Parent.BalanceFactor == 1)
                {
                    if (currentNode.BalanceFactor != 0)
                    {
                        currentNode.Parent.BalanceFactor = 2;
                    }
                }
                else if (currentNode.IsRightChild && currentNode.Parent.BalanceFactor == 1)
                {
                    if (currentNode.BalanceFactor != 0)
                    {
                        currentNode.Parent.BalanceFactor = 0;
                    }
                }
                else if (currentNode.IsRightChild && currentNode.Parent.BalanceFactor == -1)
                {
                    if (currentNode.BalanceFactor != 0)
                    {
                        currentNode.Parent.BalanceFactor = -2;
                    }
                }

                else if (currentNode.IsLeftChild && currentNode.Parent.BalanceFactor == -1)
                {
                    if (currentNode.BalanceFactor != 0)
                    {
                        currentNode.Parent.BalanceFactor = 0;
                    }
                }

                else if (currentNode.Parent.BalanceFactor == 0)
                {
                    if (currentNode.BalanceFactor != 0)
                    {
                        currentNode.Parent.BalanceFactor += currentNode.BalanceFactor;
                    }
                }

                if (Math.Abs(currentNode.BalanceFactor) == 2)
                {
                    shouldRetrace = true;
                }
            }

            if (shouldRetrace)
            {
                if (currentNode.BalanceFactor == 1 && currentNode.Parent.BalanceFactor == 2)
                {
                    currentNode.RightChild = currentNode.Parent;
                    currentNode.Parent = currentNode.Parent.Parent;
                    currentNode.RightChild.Parent = currentNode;
                    currentNode.RightChild.LeftChild = null;
                }
                if (currentNode.BalanceFactor == -1 && currentNode.Parent.BalanceFactor == 2)
                {
                    currentNode.RightChild.Parent = currentNode.Parent.Parent;
                    currentNode.RightChild.LeftChild = currentNode;
                    currentNode.RightChild.RightChild = currentNode.Parent;
                    currentNode.Parent.Parent = currentNode.RightChild;
                    currentNode.Parent = currentNode.RightChild;
                    currentNode.RightChild = null;                   
                }
                if (currentNode.BalanceFactor == -1 && currentNode.Parent.BalanceFactor == -2)
                {
                    currentNode.LeftChild = currentNode.Parent;
                    currentNode.Parent = currentNode.Parent.Parent;
                    currentNode.LeftChild.Parent = currentNode;
                    currentNode.LeftChild.RightChild = null;
                }
                if (currentNode.BalanceFactor == 1 && currentNode.Parent.BalanceFactor == -2)
                {
                    currentNode.LeftChild.Parent = currentNode.Parent.Parent;
                    currentNode.LeftChild.LeftChild = currentNode.Parent;
                    currentNode.LeftChild.RightChild = currentNode;
                    currentNode.Parent.Parent = currentNode.LeftChild;
                    currentNode.Parent = currentNode.LeftChild;
                    currentNode.LeftChild = null;
                }
            }

            return true;
        }
    }
}
