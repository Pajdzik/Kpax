using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Kpax.Core.Collections
{
    public class TwoWayDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private Dictionary<TKey, TValue> forwardDictionary;
        private Dictionary<TValue, TKey> reverseDictionary;

        public TwoWayDictionary()
        {
            this.forwardDictionary = new Dictionary<TKey, TValue>();
            this.reverseDictionary = new Dictionary<TValue, TKey>();
        }

        public TValue this[TKey key]
        {
            get => this.forwardDictionary[key];
            set
            {
                if (key == null || value == null)
                {
                    throw new ArgumentNullException("Key or value cannot be null");
                }

                this.forwardDictionary[key] = value;
                this.reverseDictionary[value] = key;
            }
        }

        public TKey this[TValue key]
        {
            get => this.reverseDictionary[key];
            set
            {
                if (key == null || value == null)
                {
                    throw new ArgumentNullException("Key or value cannot be null");
                }

                this.reverseDictionary[key] = value;
                this.forwardDictionary[value] = key;
            }
        }

        public ICollection<TKey> Keys => this.forwardDictionary.Keys;

        public ICollection<TValue> Values => this.forwardDictionary.Values;

        public int Count => this.forwardDictionary.Count;

        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<TKey, TValue>>)this.forwardDictionary).IsReadOnly
            || ((ICollection<KeyValuePair<TValue, TKey>>)this.reverseDictionary).IsReadOnly;

        public void Add(TKey key, TValue value)
        {
            this.AtomicAdd(key, value);
        }

        public void AddReverse(TValue value, TKey key)
        {
            this.AtomicAdd(key, value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.AtomicAdd(item.Key, item.Value);
        }

        private void AtomicAdd(TKey key, TValue value)
        {
            if (key == null || value == null)
            {
                throw new ArgumentNullException("Key or value cannot be null");
            }

            if (this.forwardDictionary.ContainsKey(key) || this.reverseDictionary.ContainsKey(value))
            {
                throw new ArgumentException("An element with the same key already exists");
            }

            try
            {
                this.forwardDictionary.Add(key, value);
            }
            catch (Exception ex)
            {
                throw this.CreateArgumentException(ex);
            }

            try
            {
                this.reverseDictionary.Add(value, key);
            }
            catch (Exception ex)
            {
                this.forwardDictionary.Remove(key);
                throw this.CreateArgumentException(ex);
            }
        }

        public void Clear()
        {
            this.forwardDictionary.Clear();
            this.reverseDictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)this.forwardDictionary).Contains(item);
        }

        public bool ContainsReverse(KeyValuePair<TValue, TKey> item)
        {
            return ((ICollection<KeyValuePair<TValue, TKey>>)this.reverseDictionary).Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return this.forwardDictionary.ContainsKey(key);
        }

        public bool ContainsReverseKey(TValue value)
        {
            return this.reverseDictionary.ContainsKey(value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)this.forwardDictionary).CopyTo(array, arrayIndex);
        }

        public void CopyReverseTo(KeyValuePair<TValue, TKey>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TValue, TKey>>)this.reverseDictionary).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.forwardDictionary.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TValue, TKey>> GetReverseEnumerator()
        {
            return this.reverseDictionary.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("Key or value cannot be null");
            }

            if (!this.forwardDictionary.ContainsKey(key))
            {
                return false;
            }

            TValue value = this.forwardDictionary[key];
            bool forwardSuccessful = this.forwardDictionary.Remove(key);

            if (forwardSuccessful)
            {
                bool reverseSuccessful = this.reverseDictionary.Remove(value);
                return reverseSuccessful;
            }    

            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.Remove(item.Key);
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return this.forwardDictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.forwardDictionary.GetEnumerator();
        }

        private ArgumentException CreateArgumentException([CallerMemberName] string methodName = "")
        {
            return this.CreateArgumentException(new Exception(), methodName);
        }

        private ArgumentException CreateArgumentException(Exception ex, [CallerMemberName] string methodName = "")
        {
            return new ArgumentException(
                $"{methodName} method failed for the forward dictionary."
                + "The instance is consistent state and can is safe to use.", ex);
        }
    }
}