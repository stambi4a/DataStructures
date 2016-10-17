namespace Hierarchy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private Node<T> root;
        private Dictionary<T, Node<T>> nodes;
        public Hierarchy(T root)
        {
            this.root = new Node<T>(root);
            this.nodes = new Dictionary<T, Node<T>>{ {root, this.root} };
        }

        public int Count => this.nodes.Count;
        
        public void Add(T element, T child)
        {
            if (!this.Contains(element))
            {
                throw new ArgumentException("This element does not exists.");
            }

            if (this.nodes.ContainsKey(child))
            {
                throw new ArgumentException("Child already exists.");
            }
           
            Node<T> childNode = new Node<T>(child, this.nodes[element]);
            this.nodes.Add(child, childNode);
            this.nodes[element].Children.Add(childNode);
        }

        public void Remove(T element)
        {
            if (element.Equals(this.root.Value))
            {
                throw new InvalidOperationException("Can not remove root element.");
            }

            if (!this.nodes.ContainsKey(element))
            {
                throw new ArgumentException("Element does not exists.");
            }

            T parent = this.GetParent(element);
            if (!parent.Equals(default(T)))
            {
                var children = this.GetChildren(element);
                foreach (var child in children)
                {
                    this.nodes[child].Parent = this.nodes[parent];
                    this.nodes[parent].Children.Add(this.nodes[child]);
                }

                this.nodes[parent].Children.Remove(this.nodes[element]);
            }

            this.nodes.Remove(element);
        }

        public IEnumerable<T> GetChildren(T item)
        {
            return this.nodes[item].Children.Select(x => x.Value);
        }

        public T GetParent(T item)
        {
            if (!this.nodes.ContainsKey(item))
            {
                throw new ArgumentException("Item does not exists.");
            }

            if (this.nodes[item].Equals(this.root))
            {
                return default(T);
            }

            return this.nodes[item].Parent.Value;
        }

        public bool Contains(T value)
        {
            return this.nodes.ContainsKey(value);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            return this.nodes.Keys.Intersect(other.nodes.Keys);
        } 

        public IEnumerator<T> GetEnumerator()
        {
            if (this.Count == 0)
                yield break;

            var queue = new Queue<Node<T>>();
            queue.Enqueue(this.root);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                yield return current.Value;

                foreach (var subordinate in current.Children)
                {
                    queue.Enqueue(subordinate);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}