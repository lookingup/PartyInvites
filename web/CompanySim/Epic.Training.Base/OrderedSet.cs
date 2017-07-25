using System.Collections.Generic;

namespace Epic.Training.Base
{
	public delegate void DuplicateAddAttemptedEventHandler(object sender, DuplicateAddAttemptedEventArgs args);

	public class OrderedSet<T> : IList<T>
	{
		private List<T> _list = new List<T>();

		public void Add(T item)
		{
			if (!_list.Contains(item))
				_list.Add(item);
			else RaiseDuplicateAddAttempted(item);
		}

		public bool Remove(T item)
		{
			return _list.Remove(item);
		}

		public T this[int index]
		{
			get { return _list[index]; }
			set 
			{
				if (!_list.Contains(value))
					_list[index] = value;
				else RaiseDuplicateAddAttempted(value);
			}
		}

		private DuplicateAddAttemptedEventHandler _duplicateAddAttempted;

		public event DuplicateAddAttemptedEventHandler DuplicateAddAttempted
		{
			add
			{
				_duplicateAddAttempted += value;
			}

			remove
			{
				_duplicateAddAttempted -= value;
			}
		}

		private void RaiseDuplicateAddAttempted(T duplicate) 
		{
			DuplicateAddAttemptedEventHandler eventHandlers = _duplicateAddAttempted;
			if (eventHandlers != null) { 
				eventHandlers(this, new DuplicateAddAttemptedEventArgs(duplicate));
			}
		}
		

		#region Explicitly implemented members from IList<T>
		int IList<T>.IndexOf(T item)
		{
			return _list.IndexOf(item);
		}

		void IList<T>.Insert(int index, T item)
		{
			if (!_list.Contains(item))
				_list.Insert(index, item);
			else RaiseDuplicateAddAttempted(item);
		}

		void IList<T>.RemoveAt(int index)
		{
			_list.RemoveAt(index);
		}

		void ICollection<T>.Clear()
		{
			_list.Clear();
		}

		bool ICollection<T>.Contains(T item)
		{
			return _list.Contains(item);
		}

		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			_list.CopyTo(array, arrayIndex);
		}

		int ICollection<T>.Count
		{
			get { return _list.Count; }
		}

		bool ICollection<T>.IsReadOnly
		{
			get { return ((ICollection<T>)_list).IsReadOnly; }
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _list.GetEnumerator();
		}
		#endregion
	}
}
