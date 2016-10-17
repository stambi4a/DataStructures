namespace PlayWithTrees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        private static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            for (int i = 1; i < nodesCount; i++)
            {
                string[] edge = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int parentValue = int.Parse(edge[0]);
                Tree<int> parentNode = Tree<int>.GetTreeByNodeValue(parentValue);

                int childValue = int.Parse(edge[1]);
                Tree<int> childNode = Tree<int>.GetTreeByNodeValue(childValue);
                parentNode.Children.Add(childNode);
                childNode.Parent = parentNode;
            }

            int sumPath = int.Parse(Console.ReadLine());
            int subtreeSum = int.Parse(Console.ReadLine());

            var rootNode = Tree<int>.FindRootNode();
            Console.WriteLine("Root node: {0}", rootNode.Value);

            var leafNodesValues = Tree<int>.FindLeafValues().Select(tree => tree.Value).OrderBy(value => value);
            Console.WriteLine("Leaf nodes: {0}", string.Join(",", leafNodesValues));

            var middleNodesValues = Tree<int>.FindMiddleValues().Select(tree => tree.Value).OrderBy(value => value);
            Console.WriteLine("Middle nodes: {0}", string.Join(",", middleNodesValues));

            var longestPath = Tree<int>.FindLongestPath();
            var length = longestPath.Count;
            Console.WriteLine(
                "Longest path: {0} (length = {1})",
                string.Join(" -> ", longestPath.Select(tree => tree.Value)),
                length);

            ICollection<IList<Tree<int>>> pathsWithGivenSum = FindAllPathsWithGivenSum(sumPath);
            PrintPathsWithGivenSum(pathsWithGivenSum, sumPath);

            ICollection<Tree<int>> subtreesWithGivenSum = new List<Tree<int>>();
            FindSubtreesWithGivenSum(subtreeSum, rootNode, subtreesWithGivenSum);
            PrintSubtreesWithGivenSum(subtreeSum, subtreesWithGivenSum);
        }

        public static ICollection<IList<Tree<int>>> FindAllPathsWithGivenSum(int sum)
        {
            ICollection<IList<Tree<int>>> pathsWithGivenSum = new List<IList<Tree<int>>>();
            var leafNodes = Tree<int>.FindLeafValues();
            foreach (var node in leafNodes)
            {
                IList<Tree<int>> pathWithGivenSum = new List<Tree<int>>();
                var pathNode = node;
                pathWithGivenSum.Add(pathNode);
                while (pathNode.Parent != null)
                {
                    pathWithGivenSum.Add(pathNode.Parent);
                    pathNode = pathNode.Parent;
                }

                var pathSum = pathWithGivenSum.Sum(x => x.Value);
                if (pathSum == sum)
                {
                    pathsWithGivenSum.Add(pathWithGivenSum.Reverse().ToList());
                }
            }

            return pathsWithGivenSum;
        }

        public static void PrintPathsWithGivenSum(IEnumerable<IList<Tree<int>>> pathsWithGivenSum, int sum)
        {
            Console.WriteLine("Paths of sum {0}:", sum);
            foreach (var path in pathsWithGivenSum)
            {
                var pathValues = path.Select(x => x.Value);
                Console.WriteLine(string.Join(" -> ", pathValues));
            }
        }

        public static int FindTreeSum(Tree<int> traverseTree)
        {
            int sum = traverseTree.Value;
            if (traverseTree.Children.Count == 0)
            {
                return sum;
            }

            sum += traverseTree.Children.Sum(x => FindTreeSum(x));

            return sum;
        }

        public static void FindSubtreesWithGivenSum(int sum, Tree<int> traverseTree, ICollection<Tree<int>> subtrees)
        {
            var sumSubtree = FindTreeSum(traverseTree);
            if (sumSubtree == sum)
            {
                subtrees.Add(traverseTree);
            }

            foreach (var subtree in traverseTree.Children)
            {
                FindSubtreesWithGivenSum(sum, subtree, subtrees);
            }
        }

        public static void FindTreeValues(Tree<int> tree, ICollection<int> treeValues)
        {
            treeValues.Add(tree.Value);
            foreach (var child in tree.Children)
            {
                FindTreeValues(child, treeValues);
            }
        }

        public static void PrintTree(Tree<int> tree)
        {
            ICollection<int> treeValues = new List<int>();
            FindTreeValues(tree, treeValues);
            Console.WriteLine(string.Join(" + ", treeValues));
        }

        public static void PrintSubtreesWithGivenSum(int sum, ICollection<Tree<int>> subtrees)
        {
            Console.Write("Subtrees of sum :{0}", sum);
            if (subtrees.Count == 0)
            {
                Console.WriteLine(" - None");
            }
            else
            {
                Console.WriteLine();
            }

            foreach (var subtree in subtrees)
            {
                PrintTree(subtree);
            }
        }
    }
}