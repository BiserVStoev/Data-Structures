using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private const int DefaultCapacity = 16;

    private const float LoadFactor = 0.75f;

    private LinkedList<KeyValue<TKey, TValue>>[] slots;

    public int Count { get; private set; }

    public int Capacity
    {
        get
        {
            return this.slots.Length;

        }
    }

    public HashTable(int capacity = DefaultCapacity)
    {
        this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        this.Count = 0;
    }

    public void Add(TKey key, TValue value)
    {
        this.CheckForResize();
        int slotNumber = this.FindSlotNumber(key);
        if (slots[slotNumber] == null)
        {
            slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
        }
        foreach (var element in slots[slotNumber])
        {
            if (element.Key.Equals(key))
            {
                throw new ArgumentException("Key already exists: " + key);
            }
        }
        slots[slotNumber].AddLast(new KeyValue<TKey, TValue>(key, value));
        this.Count++;
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        this.CheckForResize();
        int slotNumber = this.FindSlotNumber(key);
        if (slots[slotNumber] == null)
        {
            slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
        }
        foreach (var element in slots[slotNumber])
        {
            if (element.Key.Equals(key))
            {
                element.Value = value;
                return false;
            }
        }
        slots[slotNumber].AddLast(new KeyValue<TKey, TValue>(key, value));
        this.Count++;
        return true;
    }

    public TValue Get(TKey key)
    {
        var element = Find(key);
        if (element == null)
        {
            throw new KeyNotFoundException("The specified key was not found in the collection: " + key);
        }
        return element.Value;
    }

    public TValue this[TKey key]
    {
        get
        {
            return Get(key);
        }
        set
        {
            this.AddOrReplace(key, value);

        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var element = Find(key);
        if (element == null)
        {
            value = default(TValue);
            return false;
        }
        value = element.Value;
        return true;
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        int slotNumber = this.FindSlotNumber(key);
        var elements = this.slots[slotNumber];
        if (elements != null)
        {
            foreach (var element in elements)
            {
                if (element.Key.Equals(key))
                {
                    return element;
                }
            }
        }
        return null;
    }

    public bool ContainsKey(TKey key)
    {
        if (Find(key) == null)
        {
            return false;
        }
        return true;
    }

    public bool Remove(TKey key)
    {
        int slotNumber = this.FindSlotNumber(key);
        var list = this.slots[slotNumber];
        if (list == null)
        {
            return false;
        }
        var current = list.First;

        while (current != null)
        {
            if (current.Value.Key.Equals(key))
            {
                list.Remove(current);
                this.Count--;
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    public void Clear()
    {
        this.slots = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
        this.Count = 0;
    }

    public IEnumerable<TKey> Keys
    {
        get
        {
            return this.Select(x => x.Key);
        }
    }

    public IEnumerable<TValue> Values
    {
        get
        {
            return this.Select(x => x.Value);

        }
    }

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var elements in this.slots)
        {
            if (elements != null)
            {
                foreach (var element in elements)
                {
                    yield return element;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private void CheckForResize()
    {
        if (((float)this.Count + 1) / this.Capacity > LoadFactor)
        {
            this.Resize();
        }
    }

    private void Resize()
    {
        var newHashTable = new HashTable<TKey, TValue>(this.Capacity * 2);
        foreach (var element in this)
        {
            newHashTable.Add(element.Key, element.Value);
        }
        this.slots = newHashTable.slots;
        this.Count = newHashTable.Count;
    }

    private int FindSlotNumber(TKey key)
    {
        int slotNumber = Math.Abs(key.GetHashCode() % this.slots.Length);
        return slotNumber;
    }
}
