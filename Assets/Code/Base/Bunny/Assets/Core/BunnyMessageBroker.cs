using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IMessageBroker : IDisposable
{
    void Publish<T>(BunnyBrokerMessage<T> message);
    void Subscribe<T>(Action<BunnyBrokerMessage<T>> subscription, BunnyChannelType channel);
    void Unsubscribe<T>(Action<BunnyBrokerMessage<T>> subscription, BunnyChannelType channel);
}

public class BunnyMessageBroker : MonoBehaviour, IMessageBroker
{
    
    private static GameObject _instance;
    public static readonly object padlock = new object();
    private readonly Dictionary<BunnyChannelType, Dictionary<Type, List<Delegate>>> _subscribers;

    // Establish singleton design pattern
    private BunnyMessageBroker() {
        _subscribers = new Dictionary<BunnyChannelType, Dictionary<Type, List<Delegate>>>();
        foreach(BunnyChannelType channel in Enum.GetValues(typeof(BunnyChannelType)).Cast<BunnyChannelType>())
        {
            Debug.Log(channel);
            _subscribers.Add(channel, new Dictionary<Type, List<Delegate>>());
        }
    }

    public static BunnyMessageBroker Instance 
    {
        get
        {
            lock(padlock)
            {
                if(_instance == null)
                {
                    _instance = new GameObject("BunnyMessageBroker");
                    _instance.AddComponent<BunnyMessageBroker>();
                }
                return _instance.GetComponent<BunnyMessageBroker>();
            }
        }
    }

    public void Publish<T>(BunnyBrokerMessage<T> message) 
    {
        if (message == null || message.payload == null)
            return;
        
        if(!_subscribers.ContainsKey(message.channel))
            return;

        if(!_subscribers[message.channel].ContainsKey(typeof(T)))
            return;
        
        var delegates = _subscribers[message.channel][typeof(T)];
        if (delegates == null || delegates.Count == 0) return;

        foreach(var handler in delegates.Select(item => item as Action<BunnyBrokerMessage<T>>))
        {
            Task.Factory.StartNew(() => handler?.Invoke(message));
        }

    }

    public void Subscribe<T>(Action<BunnyBrokerMessage<T>> subscription, BunnyChannelType channel) 
    {
        var delegates = _subscribers[channel].ContainsKey(typeof(T)) ? 
                        _subscribers[channel][typeof(T)] : new List<Delegate>();
        if(!delegates.Contains(subscription))
        {
            delegates.Add(subscription);
        }
        _subscribers[channel][typeof(T)] = delegates;
    }

    public void Unsubscribe<T>(Action<BunnyBrokerMessage<T>> subscription, BunnyChannelType channel)
    {
        if(!_subscribers[channel].ContainsKey(typeof(T)))
            return;
        var delegates = _subscribers[channel][typeof(T)];
        if (delegates.Contains(subscription))
            delegates.Remove(subscription);
        if (delegates.Count == 0)
            _subscribers[channel].Remove(typeof(T));
    }

    public void Dispose()
    {
        _subscribers?.Clear();
    }
}
