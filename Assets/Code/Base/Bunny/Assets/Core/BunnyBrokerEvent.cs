using System;
using Unity.Collections;
using UnityEngine;

public class BunnyBrokerEvent<T>
{
    public string EventName;
    [ReadOnly] 
    public BunnyChannelType BunnyChannel;
    public Action<BunnyBrokerMessage<T>> BunnyEvent;

    public BunnyBrokerEvent(string Name, Action<BunnyBrokerMessage<T>> del, BunnyChannelType channel = BunnyChannelType.DefaultChannel)
    {
        EventName = Name;
        BunnyEvent = del;
        BunnyChannel = channel;
    }

    public void OnEvent(Action<BunnyBrokerMessage<T>> del, BunnyChannelType channel = BunnyChannelType.DefaultChannel)
    {
        BunnyMessageBroker.Instance.Subscribe<T>(del, channel);
    }

    public void Disconnect(Action<BunnyBrokerMessage<T>> del, BunnyChannelType channel = BunnyChannelType.DefaultChannel)
    {
        BunnyMessageBroker.Instance.Unsubscribe<T>(del, channel);
    }

    public void Fire(BunnyBrokerMessage<T> message)
    {
        BunnyMessageBroker.Instance.Publish<T>(message);
    }
}