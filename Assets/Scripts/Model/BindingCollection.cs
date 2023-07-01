using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class BindingCollection<TItem> : IList<TItem>, IBindingCollection<TItem>
{
    List<TItem> items = new List<TItem>();

    public TItem this[int index]
    {
        get => items[index];
        set => Insert(index, value);
    }

    public int Count => items.Count;
    public bool IsReadOnly => false;


    public event Action<int, TItem> ItemChanged;

    public void Add(TItem item)
    {
        items.Add(item);
        ItemChanged?.Invoke(Count-1, item);
    }

    public void Clear()
    {
        for(int i=0; i < items.Count; i++)
        {
            ItemChanged?.Invoke(i, default);
        }

        items.Clear();
    }

    public bool Contains(TItem item) => items.Contains(item);

    public void CopyTo(TItem[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);

    public IEnumerator<TItem> GetEnumerator() => items.GetEnumerator();

    public int IndexOf(TItem item) => items.IndexOf(item);

    public void Insert(int index, TItem item)
    {
        items.Insert(index, item);
        for(int i=index;i<items.Count;i++)
        {
            ItemChanged?.Invoke(i, items[i]);
        }
    }

    public bool Remove(TItem item)
    {
        var i = items.IndexOf(item);
        if(i<0)
        {
            return false;
        }

        items.RemoveAt(i);

        for(;i<items.Count;i++)
        {
            ItemChanged?.Invoke(i, items[i]);
        }

        return true;
    }

    public void RemoveAt(int index)
    {
        items.RemoveAt(index);

        for (; index < items.Count; index++)
        {
            ItemChanged?.Invoke(index, items[index]);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return items.GetEnumerator();
    }
}