using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private int itemLimit;
        private LinkedList<T> stackList;
        public LimitedSizeStack(int limit)
        {
            stackList = new LinkedList<T>();
            itemLimit = limit;
        }
        
        public int Count => stackList.Count;

        public void Push(T item)
        {
            stackList.AddFirst(item);

            if (stackList.Count > itemLimit)
                stackList.RemoveLast();
        }

        public T Pop()
        {
            if (this.Count == 0)
                throw new InvalidOperationException("Stack is empty");
            var result = stackList.First.Value;
            stackList.RemoveFirst();
            return result;
        }
    }
}
