using System.Collections.Generic;
using System.Linq;

namespace rocket_bot
{
    public class Channel<T> where T : class
    {
        private object channelLock = new object();
        private List<T> channelList = new List<T>();
        
        /// <summary>
        /// Возвращает элемент по индексу или null, если такого элемента нет.
        /// При присвоении удаляет все элементы после.
        /// Если индекс в точности равен размеру коллекции, работает как Append.
        /// </summary>
        public T this[int index]
        {
            get
            {
                lock (channelLock)
                {
                    if (channelList.Count > index && index >= 0)
                        return channelList[index];
                    return null;
                }
            }
            set
            {
                lock (channelLock)
                {
                    channelList.RemoveRange(index, channelList.Count - index);
                    channelList.Add(value);
                }
            }
        }

        /// <summary>
        /// Возвращает последний элемент или null, если такого элемента нет
        /// </summary>
        public T LastItem()
        {
            lock (channelLock)
            {
                return channelList.LastOrDefault();
            }
        }

        /// <summary>
        /// Добавляет item в конец только если lastItem является последним элементом
        /// </summary>
        public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
        {
            lock (channelLock)
            {
                if (Equals(channelList.LastOrDefault(), knownLastItem))
                    channelList.Add(item);
            }
        }

        /// <summary>
        /// Возвращает количество элементов в коллекции
        /// </summary>
        public int Count
        {
            get
            {
                lock (channelLock)
                {
                    return channelList.Count;
                }
            }
        }
    }
}