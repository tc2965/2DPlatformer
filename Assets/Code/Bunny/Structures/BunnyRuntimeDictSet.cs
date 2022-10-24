using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class BunnyRuntimeDictSetException : Exception
{
    public BunnyRuntimeDictSetException() {}
    public BunnyRuntimeDictSetException(string message) {}
}

public abstract class BunnyRuntimeDictSet<T> : ScriptableObject
{
    public Dictionary<string, T> items = new Dictionary<string, T>();

    public void Initialize()
    {
        items.Clear();
    }

    public T GetItemIndex(string Key)
    {
        if(!items.ContainsKey(Key))
            throw new BunnyRuntimeDictSetException("[RuntimeDictSet] Key for fact does not exist");
        return items[Key];
    }

    public void Add(string Key, T val)
    {
        if(!items.ContainsKey(Key))
        {
            items[Key] = val;
        }
        Debug.LogWarning($"Override attempt on key: {Key}.");
    }

    public void Remove(string Key)
    {
        if(items.ContainsKey(Key))
        {
            items.Remove(Key);
        }
    }
}
