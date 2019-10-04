using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreWCF.Service.Core.Store
{
	public class LockDictionary<TKey, TValue>
	{
		private Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();
		private ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();

		public LockDictionary()
		{
		}

		public LockDictionary(Dictionary<TKey, TValue> source)
		{
			_dictionary = source.ToDictionary(d => d.Key, d => d.Value);
		}

		public ICollection<TKey> Keys
		{
			get
			{
				List<TKey> keys = new List<TKey>();

				_locker.EnterReadLock();
				keys.AddRange(_dictionary.Keys);
				_locker.ExitReadLock();

				return keys;
			}
		}

		public ICollection<TValue> Values
		{
			get
			{
				List<TValue> values = new List<TValue>();

				_locker.EnterReadLock();
				values.AddRange(_dictionary.Values);
				_locker.ExitReadLock();

				return values;
			}
		}

		public bool ContainsKey(TKey key)
		{
			_locker.EnterReadLock();
			bool hasKey = _dictionary.ContainsKey(key);
			_locker.ExitReadLock();

			return hasKey;
		}

		public void AddOrUpdate(TKey key, TValue value)
		{
			_locker.EnterWriteLock();

			try
			{
				_dictionary[key] = value;
			}
			finally
			{
				_locker.ExitWriteLock();
			}
		}

		public void AddOrUpdate(IEnumerable<KeyValuePair<TKey, TValue>> keyedValues)
		{
			_locker.EnterWriteLock();

			try
			{
				foreach (KeyValuePair<TKey, TValue> item in keyedValues)
					_dictionary[item.Key] = item.Value;

			}
			finally
			{
				_locker.ExitWriteLock();
			}
		}

		public bool TryAdd(TKey key, TValue value)
		{
			bool added = false;

			_locker.EnterUpgradeableReadLock();

			if (!_dictionary.ContainsKey(key))
			{
				_locker.EnterWriteLock();

				try
				{
					_dictionary[key] = value;
					added = true;
				}
				finally
				{
					_locker.ExitWriteLock();
				}
			}

			_locker.ExitUpgradeableReadLock();

			return added;
		}

		public bool TryGetValue(TKey key, out TValue result)
		{
			_locker.EnterReadLock();
			bool exists = _dictionary.TryGetValue(key, out result);
			_locker.ExitReadLock();

			return exists;
		}

		public TValue this[TKey key]
		{
			get
			{
				TryGetValue(key, out TValue result);
				return result;
			}
			set { AddOrUpdate(key, value); }
		}

		public bool TryRemove(TKey key)
		{
			bool removed = false;

			_locker.EnterUpgradeableReadLock();
			if (_dictionary.ContainsKey(key))
			{
				_locker.EnterWriteLock();

				try
				{
					_dictionary.Remove(key);
				}
				finally
				{
					_locker.ExitWriteLock();
				}

				removed = true;
			}

			_locker.ExitUpgradeableReadLock();

			return removed;
		}

		public (bool Exists, TValue Value) GetValue(TKey key)
		{
			_locker.EnterReadLock();
			bool exists = _dictionary.TryGetValue(key, out var result);
			_locker.ExitReadLock();

			return (exists, result);
		}

		public IEnumerable<(bool Exists, TValue Value)> TryGetValues(IEnumerable<TKey> keys)
		{
			List<(bool Exists, TValue Value)> results = new List<(bool Exists, TValue Value)>();

			_locker.EnterReadLock();

			foreach (TKey key in keys)
			{
				bool exists = _dictionary.TryGetValue(key, out var result);

				results.Add((exists, result));
			}

			_locker.ExitReadLock();
			return results;
		}

		public IEnumerable<TValue> GetValuesOrDefaults(IEnumerable<TKey> keys)
		{
			List<TValue> results = new List<TValue>();

			_locker.EnterReadLock();
			foreach (TKey key in keys)
			{
				bool exists = _dictionary.TryGetValue(key, out var result);

				results.Add(exists ? result : default(TValue));
			}

			_locker.ExitReadLock();

			return results;
		}
	}
}
