using System;

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine;

/*
Bad attempt from @Mohammedajao to visualize the events in Unity
*/

public interface IBunnyEvent {
    public void OnEventRaised<T>(Action<BunnyBrokerMessage<T>> del);
    public void Disconnect<T>(Action<BunnyBrokerMessage<T>> subscription);
    public void Fire<T>(BunnyBrokerMessage<T> message);
}

public interface IBunnyEventArgs {}

public abstract class IBunnyBrokerEventHandler {

}

[System.Serializable]
public class BunnyEvent : IBunnyEvent
{
    [SerializeField] private string eventName;
    public String EventName => eventName;
    public object Sender;
    private Dictionary<Type, List<Delegate>> _subscribers;

    public BunnyEvent(string name, object source)
    {
        eventName = name;
        Sender = source;
        _subscribers = new Dictionary<Type, List<Delegate>>();
    }

    public void OnEventRaised<T>(Action<BunnyBrokerMessage<T>> callback)
    {
        var payloadType = typeof(T);
        if(!_subscribers.ContainsKey(payloadType))
        {
            _subscribers.Add(payloadType, new List<Delegate>());
        }
        if(!_subscribers[payloadType].Contains(callback))
            _subscribers[payloadType].Add(callback);
    }

    public void Fire<T>(BunnyBrokerMessage<T> message)
    {
        var payloadType = typeof(T);
        if(!_subscribers.ContainsKey(payloadType))
            return;

        var delegates = _subscribers[typeof(T)];
        if (delegates == null || delegates.Count == 0) return;

        foreach(var handler in delegates.Select(item => item as Action<BunnyBrokerMessage<T>>))
        {
            handler?.Invoke(message);
        }
    }

    public void Disconnect<T>(Action<BunnyBrokerMessage<T>> subscription)
    {
        if(!_subscribers.ContainsKey(typeof(T)))
            return;
        var delegates = _subscribers[typeof(T)];
        if (delegates.Contains(subscription))
            delegates.Remove(subscription);
        if (delegates.Count == 0)
            _subscribers.Remove(typeof(T));
    }
}