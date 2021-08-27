using System;
using System.Collections.Generic;

namespace Clones
{
    internal class TreeNode<T>
    {
        public T Value { get; }
        public TreeNode<T> Parent { get; }

        public TreeNode(T value, TreeNode<T> parent)
        {
            Value = value;
            Parent = parent;
        }
    }

    public class Clone
    {
        private TreeNode<int> learntPrograms;
        private TreeNode<int> rollbackHistory;

        public Clone() { }
        
        public Clone(Clone parent)
        {
            learntPrograms = parent.learntPrograms;
            rollbackHistory = parent.rollbackHistory;
        }

        public string Check() => (learntPrograms is null)
            ? "basic"
            : learntPrograms.Value.ToString();

        public void Learn(int program)
        {
            rollbackHistory = null;
            learntPrograms = new TreeNode<int>(program, learntPrograms);
        }

        public void Rollback()
        {
            rollbackHistory = new TreeNode<int>(learntPrograms.Value, rollbackHistory);
            learntPrograms = learntPrograms.Parent;
        }

        public void Relearn()
        {
            learntPrograms = new TreeNode<int>(rollbackHistory.Value, learntPrograms);
            rollbackHistory = rollbackHistory.Parent;
        }
    }

    public class CloneVersionSystem : ICloneVersionSystem
    {
        private readonly List<Clone> clones;

        public CloneVersionSystem() => clones = new List<Clone> { new Clone() };

        public string Execute(string query)
        {
            var queryArguments = query.Split();
            var command = queryArguments[0];
            var cloneIndex = int.Parse(queryArguments[1]) - 1;
            var clone = clones[cloneIndex];
            switch (command)
            {
                case "clone":
                    clones.Add(new Clone(clone));
                    return null;
                case "learn":
                    var programIndex = int.Parse(queryArguments[2]);
                    clone.Learn(programIndex);
                    return null;
                case "rollback":
                    clone.Rollback();
                    return null;
                case "relearn":
                    clone.Relearn();
                    return null;
                case "check":
                    return clone.Check();
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}