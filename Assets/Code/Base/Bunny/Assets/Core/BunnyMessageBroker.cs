using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IMessageBroker : IDisposable
{
    void Publish<T>(BunnyBrokerMessage<T> message);
    void Subscribe<T>(Action<BunnyBrokerMessage<T>> subscription);
    void Unsubscribe<T>(Action<BunnyBrokerMessage<T>> subscription);
}

public class BunnyMessageBroker : MonoBehaviour, IMessageBroker
{
    
    private static GameObject _instance;
    public static readonly object padlock = new object();
    private readonly Dictionary<BunnyChannelType, Dictionary<Type, List<Delegate>>> _subscribers;

    // Establish singleton design pattern
    private BunnyMessageBroker() {
        _subscribers = new Dictionary<BunnyChannelType, Dictionary<Type, List<Delegate>>>();
        foreach(BunnyChannelType channel in Enum.GetValues(typeof(BunnyChannelType)))
        {
            _subscribers.Add(channel, new Dictionary<Type, List<Delegate>>());
        }
    }

    public static BunnyMessageBroker GetInstance 
    {
        get
        {
            lock(padlock)
            {
                if(_instance == null)
                {
                    _instance = new GameObject("BunnyMessageBroker");
                } else {
                    Destroy(_instance);
                }
                return _instance.AddComponent<BunnyMessageBroker>();
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

    public void Subscribe<T>(Action<BunnyBrokerMessage<T>> subscription) 
    {

    }

    public void Unsubscribe<T>(Action<BunnyBrokerMessage<T>> subscription)
    {

    }

    public void Dispose()
    {
        _subscribers?.Clear();
    }
}
