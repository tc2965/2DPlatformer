using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

// May have to look to building a policy object for enforcing things like a lifetime policy.

public enum BunnyMessagePriority
{
    LOW,
    MEDIUM,
    HIGH
}

public interface IMessage {}

public interface IMessagePublisher
{
    Task PublishAsync<T>(string topic, T message) where T : IMessage;
}

public interface IMessageSubscriber
{
    Task SubscribeAsync<T>(string topic, Action<T> handler) where T : IMessage;
}

public class BunnyBrokerMessage<T> : IMessage
{
    public BunnyChannelType channel = BunnyChannelType.DefaultChannel;
    public BunnyMessagePriority priority =  BunnyMessagePriority.LOW;
    public T payload;
    public object sender;
    public float lifetime = 3.0f;

    public BunnyBrokerMessage()
    {
        payload = default(T);
        sender = BunnyMessageBroker.Instance;
        channel = BunnyChannelType.DefaultChannel;
        priority =  BunnyMessagePriority.LOW;
    }

    public BunnyBrokerMessage(
        T value,
        object source
    ) 
    {
        payload  = value;
        sender = source;
        channel = BunnyChannelType.DefaultChannel;
        priority =  BunnyMessagePriority.LOW;
    }

    public BunnyBrokerMessage(
        T value,
        object source,
        BunnyChannelType channelType,
        BunnyMessagePriority prio 
    ) 
    {
        payload  = value;
        sender = source;
        channel = channelType;
        priority =  prio;
    }
}

public class BunnyMessage : IMessage
{
    public string Name;
    public BunnyChannelType channel;
    public object sender;
    public float lifetime;
    public object value;

    public BunnyMessage(string name, object payload, object source, BunnyChannelType channel) {
        this.Name = name;
        this.value = payload;
        this.sender = source;
        this.channel = channel;
    }

    public T GetValue<T>()
    {
        if(value is T)
        {
            return (T) value;
        }
        return default(T);
    }

    public Component GetSenderAsComponent()
    {
        if(sender is Component) return (Component)sender;
        return null;
    }
}