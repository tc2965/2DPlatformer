using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBroker : BunnyEntity
{
    private static BunnyBroker _instance = null;
    public static readonly object padlock = new object();

    private Dictionary<string, List<Action>> EventLibrary  = new Dictionary<string, List<Action>>();
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

    public void Subscribe(BunnyMessage message)
    {
        if(!EventLibrary.ContainsKey(message.Name))
        {
            EventLibrary.Add(message.Name, new List<Action>());
        }
        EventLibrary[message.Name].Add(message.GetValue<Action>());
    }

    public void Unsubscribe(BunnyMessage message)
    {
        if(!EventLibrary.ContainsKey(message.Name))
        {
            return;
        }
        EventLibrary[message.Name].Remove(message.GetValue<Action>());
    }

    public void Publish(BunnyMessage message)
    {
        if(!EventLibrary.ContainsKey(message.Name)) {
            Debug.LogWarning($"BunnyBroker: Event Name '{message.Name}' not found");
        }
        foreach(var callback in EventLibrary[message.Name])
        {
            try
            {
                callback.Invoke();
            } catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}
