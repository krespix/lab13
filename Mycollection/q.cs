using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Laba11;

namespace Laba12
{
   public class Queue : IEnumerable
    {
        public Point firstPoint; // головной/первый элемент
        public Point lastPoint; // последний/хвостовой элемент
        int count;
        public int capacity;

        public Queue()
        {
            firstPoint = null;
            lastPoint = null;
            count = 0;
        }

        public Queue(int capacity)
        {
            firstPoint = null;
            lastPoint = null;
            this.capacity = capacity;
        }

        public Queue(Queue collection)
        {
            Point temp = collection.firstPoint;
            count = collection.count;
            while (temp != null)
            {
                Enqueue(temp.Data);
                temp = temp.Next;
            }
        }
        
        // добавление в очередь
        public void Enqueue(Goods data)
        {
            Point node = new Point(data);
            Point tempNode = lastPoint;
            lastPoint = node;
            if (count == 0)
                firstPoint = lastPoint;
            else
                tempNode.Next = lastPoint;
            count++;
        }

        public void Enqueue(params Goods[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Enqueue(data[i]);
            }
        }

        // удаление из очереди
        public Goods Dequeue()
        {
            if (count == 0)
                throw new InvalidOperationException();
            Goods output = firstPoint.Data;
            firstPoint = firstPoint.Next;
            count--;
            return output;
        }

        public void Dequeue(int count)
        {
            if (count > Count)
            {
                throw new InvalidCastException();
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    Dequeue();
                }
            }
        }
        
        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }
 
        public void Clear()
        {
            firstPoint = null;
            lastPoint = null;
            count = 0;
        }

        public bool Contains(Goods data, out Goods result)
        {
            result = firstPoint.Data;
            Point current = firstPoint;
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    result = current.Data;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public override string ToString()
        {
            string result = "";
            foreach (var item in this)
            {
                result += item.ToString();
                result += "\n";
            }

            return result;
        }

        public Goods this[int index]
        {
            get
            {
                if (index >= 0 && index < capacity)
                {
                    Point temp = firstPoint;
                    for (int i = 0; i < capacity; i++)
                    {
                        if (i == index)
                        {
                            return temp.Data;
                        }

                        temp = temp.Next;
                    }

                    return null;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (index >= 0 && index < count)
                {
                    Point temp = firstPoint;
                    int i = 0;
                    while (i != index)
                    {
                        temp = temp.Next;
                    }

                    temp.Data = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            Point current = firstPoint;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}