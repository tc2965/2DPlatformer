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
    private static GameObject _instance = null;
    public static readonly object padlock = new object();

    // private Queue<BunnyMessage> _messages;
    public Dictionary<BunnyChannelType, List<KeyValuePair<string, Action<object>>>> EventLibrary  = new Dictionary<BunnyChannelType, List<KeyValuePair<string, Action<object>>>>();
    private BunnyBroker() {}
    public static BunnyBroker Instance
    {
        get
        {
            lock(padlock)
            {
                if(_instance == null)
                {
                    _instance = new GameObject("BunnyBroker");
                } else {
                    Destroy(_instance);
                }
                return _instance.AddComponent<BunnyBroker>();
            }
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static BunnyBroker GetInstance()
    {
        return BunnyBroker.Instance.GetComponent<BunnyBroker>();
    } 
    // public void Submit(BunnyMessage message)
    // {
    //     _messages.Enqueue(message);
    // }
    
    public void Subscribe(BunnyMessage message)
    {
        Debug.Log("Got sub first");
        if(!EventLibrary.ContainsKey(message.channel))
        {
            EventLibrary.Add(message.channel, new List<KeyValuePair<string, Action<object>>>());
        }
        EventLibrary[message.channel].Add(new KeyValuePair<string, Action<object>>(message.Name, message.GetValue<Action<object>>()));
    }

    public void Unsubscribe(BunnyMessage message)
    {
        if(!EventLibrary.ContainsKey(message.channel))
        {
            return;
        }
        EventLibrary[message.channel].Remove(new KeyValuePair<string, Action<object>>(message.Name, message.GetValue<Action<object>>()));
    }

    public void Publish(BunnyMessage message)
    {
        Debug.Log("Got a pub req");
        if(!EventLibrary.ContainsKey(message.channel)) {
            Debug.LogWarning($"BunnyBroker: Attempt made to publish to nonexistent event'{message.channel}'. Please check where this call is being made.");
            return;
        }
        foreach(var callback in EventLibrary[message.channel])
        {
            Debug.Log("Callback key is: " + callback.Key);
            Debug.Log("Callback key is: " + callback.Key + " " + callback.Value);


            try
            {
                Debug.Log("Callback key is: " + callback.Key + " " + callback.Value?.GetType());
                callback.Value.Invoke(message);
            } catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}
