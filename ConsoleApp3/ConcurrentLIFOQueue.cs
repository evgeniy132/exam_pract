using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class ConcurrentLIFOQueue<T>
    {
        private T[] _array;
        private int _length;

        public ConcurrentLIFOQueue(int size)
        {
            _array = new T[size];
            _length = 0;
        }

        public int Count()
        {
            return _length;
        }
        public int Length
        {
            get
            {
                return _array.Length;
            }
        }

        public bool TryDequeue(out T result)
        {
            lock (_array)
            {
                if (Length > 0)
                {
                    var index = _array.Length - _length;
                    result = _array[index];
                    _array[index] = default(T);
                    _length--;
                    return true;
                }
                else
                {
                    result = default(T);
                    return false;
                }
            }
        }

        public void Enqueue(T item)
        {
            lock (_array)
            {
                if (_length < _array.Length)
                {
                    _array[_array.Length - _length - 1] = item;
                    _length++;
                }
            }
        }
    }
}
