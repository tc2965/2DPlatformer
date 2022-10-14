using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Will be used for the PubSub system which will be a higher order pattern.
Broker is currently designed as a singleton with thread-safety.
We'll be using threads assigned to different channels/topics for events.

For example:
    * SoundChannel => new Thread = Events[...]
    * GameFlowChannel => new Thread = GameEvents[...]
    * PhysicsChannel => new Thread = PhysicsEvents[...]

I doubt the game will be so complex for multiple channels of varying complexity.
*/

public interface IMessenger
{
    void Subscribe<T>(Action<T> callback, Predicate<T> predicate = null);
    void Unsubscribe<T>(Action<T> callback);
    void Publish<T>(T payload);
}

public interface IBunnyListener
{
    void _subscribe_(BunnyChannel channel);
    void _unsubscribe_(BunnyChannel channel);
}

public class BunnyBroker : BunnyEntity
{
    private static BunnyBroker _instance = null;
    public static readonly object padlock = new object();

    // private Queue<BunnyMessage> _messages;
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
    // public void Submit(BunnyMessage message)
    // {
    //     _messages.Enqueue(message);
    // }
    
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
