using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;


namespace _ArrayList
{
    public class _ArrayList:IEnumerable
    {
        private Object[] _items;
        private int _size;
        private const int _defaultCapacity = 4;
        private int _version;
        private static readonly object[] emptyArray = new object[0];

        public _ArrayList()
        {
            _items = emptyArray;
        }
        public _ArrayList(int capacity)
        {
            if(capacity<0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if(capacity==0)
            {
                _items = emptyArray;
            }
            else
            {
                _items = new object[capacity]; 
            }
        }
        public virtual int Count
        {
            get
            {
                return _size;
            }
        }
        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= _size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return _items[index];  
            }
            set
            {
                if(index<0 || index >= _size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _items[index] = value;
                _version++;
            }
        }
        public int Capacity
        {
            get
            {
                return _items.Length;
            }
            set
            {
                if (value < _size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        object[] newItems = new object[value];  
                        if(_size>0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _size);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = new object[_defaultCapacity];
                    }
                }
            }
        }

        public int Add(object value)
        {
            if(_size==_items.Length)
            {
                EnsureCapacity(_size + 1);
            }
            _items[_size] = value;
            _version++;
            return _size++;
        }
        public virtual void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(_items, 0, _size);
                _size = 0;
            }
        }
        public virtual void CopyTo(Array array, int arrayIndex)
        {
            if (array != null && array.Rank != 1)
                throw new ArgumentException();
            Array.Copy(_items, 0, array, arrayIndex, _size);
        }
        public virtual object Clone()
        {
            _ArrayList arrayList = new _ArrayList(_size);
            arrayList._size = _size;
            arrayList._version = _version; 
            Array.Copy(_items, 0, arrayList._items, 0, _size);
            return arrayList;
        }
        private void EnsureCapacity(int min)
        {
            if(_items.Length<min)
            {
                int newCapacity;
                if(_items.Length==0)
                {
                    newCapacity = _defaultCapacity;
                }
                else
                {
                    newCapacity = _items.Length * 2;
                }
                Capacity = newCapacity;
            }
        }

        public virtual IEnumerator GetEnumerator()
        {
            return new ArrayListEnumeratorSimple(this);
        }
        public sealed class ArrayListEnumeratorSimple : IEnumerator, ICloneable
        {
            private _ArrayList list;
            private int index;
            private int version;
            private Object currentElement;
            [NonSerialized]
            private bool isArrayList;
            static Object dummyObject = new Object();

            internal ArrayListEnumeratorSimple(_ArrayList list)
            {
                this.list = list;
                this.index = -1;
                version = list._version;
                isArrayList = (list.GetType() == typeof(ArrayList));
                currentElement = dummyObject;
            }

            public Object Clone()
            {
                return MemberwiseClone();
            }

            public bool MoveNext()
            {
                if (version != list._version)
                {
                    throw new InvalidOperationException();
                }

                if (isArrayList)
                {
                    if (index < list._size - 1)
                    {
                        currentElement = list._items[++index];
                        return true;
                    }
                    else
                    {
                        currentElement = dummyObject;
                        index = list._size;
                        return false;
                    }
                }
                else
                {
                    if (index < list.Count - 1)
                    {
                        currentElement = list[++index];
                        return true;
                    }
                    else
                    {
                        index = list.Count;
                        currentElement = dummyObject;
                        return false;
                    }
                }
            }

            public Object Current
            {
                get
                {
                    object temp = currentElement;
                    if (dummyObject == temp)
                    {
                        if (index == -1)
                        {
                            throw new InvalidOperationException();
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }
                    }

                    return temp;
                }
            }

            public void Reset()
            {
                if (version != list._version)
                {
                    throw new InvalidOperationException();
                }

                currentElement = dummyObject;
                index = -1;
            }

        }
    }
}
