using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDSFactory
{
    [Serializable]
    public class MyList<T>   :IList<T> 
    { 
        private List<T> _list = new List<T>();
        private object _sync = new object();
        protected bool _IsReadOnly;


        public virtual T this[int index]
        {
            get
            {
                lock (_sync)
                {
                    return (T)_list[index];
                }
            }
            set
            {
                lock (_sync)
                {
                    _list[index] = value;
                }
            }
        }

     

        public void Add(T value)
        {
            lock (_sync)
            {
                _list.Add(value);
              
            }
        }

        public void AddRange(IEnumerable<T> items)
        {
            lock (_sync)
            {
                //foreach (var item in items)
                    _list.AddRange(items);
                 
            } 
        }


        public bool IsReadOnly
        {
            get
            {
                lock (_sync)
                {
                    return _IsReadOnly;
                }
            } 
        }

        public int Count 
        {
            get
            {
                lock (_sync)
                {
                    return _list.Count;
                }
            }
        }

        public bool Contains(T item)
        {
            lock (_sync)
            {
                return _list.Contains(item);
            }
        } 

        public T Find(Predicate<T> predicate)
        {
            lock (_sync)
            {
                return _list.Find(predicate);
            }
        }
        public List<T> FindAll(Predicate<T> predicate)
        {
            lock (_sync)
            {
                return _list.FindAll(predicate);
            }
        }
        public T FirstOrDefault()
        {
            lock (_sync)
            {
                return _list.FirstOrDefault();
            }
        }

        public int RemoveAll(Predicate<T> predicate)
        {
            lock (_sync)
            {
                return _list.RemoveAll(predicate);
            }
        }

        public bool Remove(T value)
        {
            lock (_sync)
            {
                return _list.Remove(value);
            }
        }


        public void RemoveAt(int index)
        {
            lock (_sync)
            {
                _list.RemoveAt(index);
            }
        }

        public int IndexOf(T item)
        {
            lock (_sync)
            {
              return  _list.IndexOf(item);
            }
        }

        public void Insert(int index, T item)
        {
            lock (_sync)
            {
                _list.Insert(index, item);
            }
        }


        public void CopyTo(T[] array, int index)
        {
            lock (_sync)
            {
                _list.CopyTo(array, index);
            }
        }

        public void Clear()
        {
            lock (_sync)
            {
                _list.Clear();
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            lock (this._sync)
            {
                return NewEnumerator();
            }
        }

        private IEnumerator<T> NewEnumerator()
        {
            return new ThreadSafeEnumerator(this);
        }
        //IEnumerator<T> IEnumerable<T>.GetEnumerator()
        //{
        //     instead of returning an usafe enumerator,
        //     we wrap it into our thread-safe class
        //    return new SafeEnumerator<T>(_list.GetEnumerator(), _sync);
        //}

        public IEnumerator<T> GetEnumerator()
        {
            lock (_sync)
                return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (_sync)
                return this.GetEnumerator();
        }


        IEnumerable<T> Where(Predicate<T> predicate)
        {
            lock (_sync)
                return this.Where(predicate);
        }


     
        //internal void Remove(global::EDSCloudComputing.SocketCommunication.ClientInfo ci)
        //{
        //    throw new NotImplementedException();
        //}



        private class ThreadSafeEnumerator : IEnumerator<T>
        {
            /// <summary>
            /// Snapshot to enumerate.
            /// </summary>
            private readonly MyList<T> collection;

            /// <summary>
            /// Internal enumerator of the snapshot.
            /// </summary>
            private readonly IEnumerator<T> enumerator;

            /// <summary>
            /// Initializes a new instance of the ThreadSafeEnumerator class, creating a snapshot of the given collection.
            /// </summary>
            /// <param name="collection">List to snapshot.</param>
            public ThreadSafeEnumerator(MyList<T> collection)
            {
                lock (collection._sync)
                {
                    // Make snapshot of passed in collection.
                    this.collection = new MyList<T>();
                    this.collection.AddRange(collection);

                    // Wrapped enumerator.
                    enumerator = this.collection._list.GetEnumerator();
                }
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <returns>The element in the collection at the current position of the enumerator.</returns>
            public T Current
            {
                get
                {
                    lock (this.collection._sync)
                    {
                        return enumerator.Current;
                    }
                }
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <returns>The element in the collection at the current position of the enumerator.</returns>
            object IEnumerator.Current
            {
                get
                {
                    lock (this.collection._sync)
                    {
                        return enumerator.Current;
                    }
                }
            }

            /// <summary>
            /// Disposes the underlying enumerator.  Does not set collection or enumerator to null so calls will still
            /// proxy to the disposed instance (and throw the proper exception).
            /// </summary>
            public void Dispose()
            {
                lock (this.collection._sync)
                {
                    enumerator.Dispose();
                }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false
            /// if the enumerator has passed the end of the collection.
            /// </returns>
            /// <exception cref="System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
            public bool MoveNext()
            {
                lock (this.collection._sync)
                {
                    return enumerator.MoveNext();
                }
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element
            /// in the collection.
            /// </summary>
            /// <exception cref="System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
            public void Reset()
            {
                lock (this.collection._sync)
                {
                    enumerator.Reset();
                }
            }
        }
    }
}
