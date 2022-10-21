using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BunnyRuntimeSet<T> : ScriptableObject
{
    private List<T> items = new List<T>();

    public void Initialize()
    {
        items.Clear();
    }

    public T GetItemIndex(int index)
    {
        return items[index];
    }

    public void AddToList(T val)
    {
        if(!items.Contains(val))
        {
            items.Add(val);
        }
    }

    public void RemmoveFromList(T val)
    {
        if(items.Contains(val))
        {
            items.Remove(val);
        }
    }
}