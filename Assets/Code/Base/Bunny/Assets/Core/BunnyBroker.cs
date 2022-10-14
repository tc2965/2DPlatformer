using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMessenger
{
    void Subscribe<T>(Action<T> callback, Predicate<T> predicate = null);
    void Unsubscribe<T>(Action<T> callback);
    void Publish<T>(T payload);
}

public class BunnyBroker : BunnyEntity
{
    private static BunnyBroker _instance = null;
    public static readonly object padlock = new object();

    private Dictionary<string, List<Action<object>>> EventLibrary  = new Dictionary<string, List<Action<object>>>();
    private BunnyBroker() {}
    public static BunnyBroker Instance
    {
        get
        {
            lock(padlock)
            {
                if(_instance == null)
                {
                    _instance = new BunnyBroker();
                }
                return _instance;
            }
        }
    }

    public static BunnyBroker GetInstance()
    {
        return _instance;
    } 

    public void Subscribe<T>(BunnyMessage message)
    {
        if(!EventLibrary.ContainsKey(message.Name))
        {
            EventLibrary.Add(message.Name, new List<Action<object>>());
        }
        EventLibrary[message.Name].Add(message.GetValue<Action<object>>());
    }

    public void Unsubscribe(BunnyMessage message)
    {
        if(!EventLibrary.ContainsKey(message.Name))
        {
            return;
        }
        EventLibrary[message.Name].Remove(message.GetValue<Action<object>>());
    }

    public void Publish<T>(BunnyMessage message)
    {
        if(!EventLibrary.ContainsKey(message.Name)) {
            Debug.LogWarning($"BunnyBroker: Attempt made to publish to nonexistent event'{message.Name}'. Please check where this call is being made.");
        }
        foreach(var callback in EventLibrary[message.Name])
        {
            try
            {
                callback.Invoke(message.GetValue<T>());
            } catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}
