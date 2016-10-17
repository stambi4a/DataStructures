namespace PlayWithTrees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T>  where T : IComparable
    {
        private static Dictionary<T, Tree<T>> nodeByValue = new Dictionary<T, Tree<T>>(); 

        public T Value { get; set; }

        public Tree<T> Parent { get; set; } 

        public IList<Tree<T>> Children { get; private set; }

        public Tree(T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = new List<Tree<T>>();
            foreach (var child in children)
            {
                this.Children.Add(child);
                child.Parent = this;
                nodeByValue.Add(child.Value, child);
            }   
        }

        public static Tree<T> GetTreeByNodeValue(T value)
        {
            if (!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree<T>(value);
            }

            return nodeByValue[value];
        }

        public static Tree<T> FindRootNode()
        {
            return nodeByValue.Values.FirstOrDefault(x => x.Parent == null);
        }

        public static IEnumerable<Tree<T>> FindMiddleValues()
        {
            var middleNodesValues = nodeByValue.Values.Where(node => node.Children.Count > 0 && node.Parent != null).ToList();
            return middleNodesValues;
        }

        public static IEnumerable<Tree<T>> FindLeafValues()
        {
            var leafNodesValues = nodeByValue.Values.Where(node => node.Children.Count == 0 && node.Parent != null).ToList();

            return leafNodesValues;
        }

        public static ICollection<Tree<T>> FindLongestPath()
        {
            var leafNodes = FindLeafValues();
            IList<Tree<T>> longestPath = new List<Tree<T>>();
            foreach (var node in leafNodes)
            {
                IList<Tree<T>> comparePath = new List<Tree<T>>();
                comparePath.Add(node);
                var pathNode = node;
                while (pathNode.Parent != null)
                {
                    comparePath.Add(pathNode.Parent);
                    pathNode = pathNode.Parent;
                }
                if (comparePath.Count > longestPath.Count)
                {
                    longestPath = comparePath;
                }
            }

            return longestPath.Reverse().ToList();
        }
    }
}
