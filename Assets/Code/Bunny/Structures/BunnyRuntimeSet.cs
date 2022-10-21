using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyRuntimeSet<T> : ScriptableObject
{
    public List<T> items = new List<T>();

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

    public void RemoveFromList(T val)
    {
        if(items.Contains(val))
        {
            items.Remove(val);
        }
    }
}
