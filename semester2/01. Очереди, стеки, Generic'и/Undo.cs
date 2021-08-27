using System;
using System.Collections.Generic;

namespace TodoApplication
{
    enum ActionType
    {
        Add,
        Remove
    }

    internal class Action<TItem>
    {
        public ActionType Type { get; }
        public int Index { get; }
        public TItem Item { get; }
		
        public Action(ActionType type, int index, TItem item)
        {
            this.Type = type;
            this.Index = index;
            this.Item = item;
        }
    }

    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        private LimitedSizeStack<Action<TItem>> lastActions;

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            lastActions = new LimitedSizeStack<Action<TItem>>(limit);
        }

        public void AddItem(TItem item)
        {
            lastActions.Push(new Action<TItem>(ActionType.Add, Items.Count, default(TItem)));
            Items.Add(item);
        }

        public void RemoveItem(int index)
        {
            lastActions.Push(new Action<TItem>(ActionType.Remove, index, Items[index]));
            Items.RemoveAt(index);
        }

        public bool CanUndo() => lastActions.Count > 0;

        public void Undo()
        {
            var lastAction = lastActions.Pop();
            switch (lastAction.Type)
            {
                case ActionType.Add:
                    Items.RemoveAt(lastAction.Index);
                    break;
                case ActionType.Remove:
                    Items.Insert(lastAction.Index, lastAction.Item);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lastAction.Type), lastAction.Type, null);
            }
        }
    }
}