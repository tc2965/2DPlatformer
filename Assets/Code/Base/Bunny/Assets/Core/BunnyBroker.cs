using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBrokerMessage : BunnyMessage<Action> {
    public string Name { get; set; }

    public BunnyBrokerMessage(string name, Action cb, Action pl, BunnyMessagePriority prio = BunnyMessagePriority.LOW, float limit = 3)
    {
        Name = name;
        payload = pl;
        priority = prio;
        lifetime = limit;
    }

    public override Action GetValue()
    {
        return payload;
    }
}

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

    public void Subscribe(BunnyBrokerMessage message)
    {
        if(!EventLibrary.ContainsKey(message.Name))
        {
            EventLibrary.Add(message.Name, new List<Action>());
        }
        EventLibrary[message.Name].Add((Action) message.GetValue());
    }

    public void Publish(BunnyBrokerMessage message)
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
