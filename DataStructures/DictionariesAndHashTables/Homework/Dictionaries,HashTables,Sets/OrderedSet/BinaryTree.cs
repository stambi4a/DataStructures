namespace OrderedSet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BinaryTree<T> : IEnumerable where T : IComparable
    {
        public BinaryTree(T value)
        {
            this.Value = value;
            this.LeftBranch = null;
            this.RightBranch = null;
            this.Parent = null;
            this.Count = 0;
        } 
        public T Value { get; set; }

        public BinaryTree<T> LeftBranch { get; set; }

        public BinaryTree<T> RightBranch { get; set; }

        public BinaryTree<T> Parent { get; set; } 

        public int Count { get; set; }

        internal void AddBranch(T value)
        {
            var currentBranch = this;
            while (true)
            {
                if (value.CompareTo(currentBranch.Value) < 0)
                {
                    if (currentBranch.LeftBranch != null)
                    {
                        currentBranch = currentBranch.LeftBranch;
                    }
                    else
                    {
                        currentBranch.LeftBranch = new BinaryTree<T>(value) {Parent = currentBranch};
                        this.Count++;
                        return;
                    }
                    
                }
                else if(value.CompareTo(currentBranch.Value) > 0 )
                {
                    if (currentBranch.RightBranch != null)
                    {
                        currentBranch = currentBranch.RightBranch;
                    }
                    else
                    {
                        currentBranch.RightBranch = new BinaryTree<T>(value) {Parent = currentBranch};
                        this.Count++;
                        return;
                    }
                }
                return;
            }

        }

        internal bool ContainsKey(T value)
        {
            var currentBranch = this;
            while (true)
            {
                if (value.CompareTo(currentBranch.Value) < 0)
                {
                    if (currentBranch.LeftBranch != null)
                    {
                        currentBranch = currentBranch.LeftBranch;
                    }
                    else
                    {
                        return false;
                    }

                }
                else if (value.CompareTo(currentBranch.Value) > 0)
                {
                    if (currentBranch.RightBranch != null)
                    {
                        currentBranch = currentBranch.RightBranch;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        internal void RemoveBranch(T value)
        {
            var currentBranch = this;
            while (true)
            {
                if (value.CompareTo(currentBranch.Value) < 0)
                {
                    if (currentBranch.LeftBranch != null)
                    {
                        currentBranch = currentBranch.LeftBranch;
                    }
                    else
                    {
                        return;
                    }

                }
                else if (value.CompareTo(currentBranch.Value) > 0)
                {
                    if (currentBranch.RightBranch != null)
                    {
                        currentBranch = currentBranch.RightBranch;
                    }
                    else
                    {                        
                        return;
                    }
                }
                else
                {
                    BinaryTree<T> newBranch = new BinaryTree<T>(currentBranch.Parent.Value);
                    ISet<T> values = new HashSet<T>();
                    this.TraverseBfsBinaryTree(values, currentBranch);
                    this.TraverseBfsBinaryTree(values, currentBranch.Parent.LeftBranch);
                    foreach (var element in values)
                    {
                        newBranch.AddBranch(element);
                    }
                    if (currentBranch.Parent.RightBranch == currentBranch)
                    {
                        currentBranch.Parent = newBranch;
                    }

                    this.Count--;
                }
            }
        }

        private void TraverseBfsBinaryTree(ISet<T> values, BinaryTree<T> branch)
        {
            while (branch!= null)
            {
                if (branch.RightBranch != null)
                {
                    values.Add(branch.RightBranch.Value);
                    branch = branch.RightBranch;
                }
                
                this.TraverseBfsBinaryTree(values, branch);
                if (branch.LeftBranch != null)
                {
                    values.Add(branch.LeftBranch.Value);
                    branch = branch.LeftBranch;
                }

                this.TraverseBfsBinaryTree(values, branch);
            }
        }

        internal IEnumerator<T> GetEnumerator()
        {
            var values = new List<T>();
            this.TraverseTreeAscendingOrder(values, this);
            foreach (var value in values)
            {
                yield return value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void TraverseTreeAscendingOrder(ICollection<T> values, BinaryTree<T> currentTree)
        {
            if (currentTree.LeftBranch != null)
            {
                currentTree = currentTree.LeftBranch;
                this.TraverseTreeAscendingOrder(values, currentTree);
            }

            values.Add(currentTree.Value);

            if (currentTree.RightBranch != null)
            {
                currentTree = currentTree.RightBranch;
                this.TraverseTreeAscendingOrder(values, currentTree);
            }
        }
    }
}
