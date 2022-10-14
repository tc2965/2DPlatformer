using Unity.Collections;
using UnityEngine;

// May have to look to building a policy object for enforcing things like a lifetime policy.

public enum BunnyMessagePriority
{
    LOW,
    MEDIUM,
    HIGH
}

public struct BunnyMessage
{
    public string Name;
    public BunnyEntity sender;
    public BunnyEntity receiver;
    public float lifetime;
    public object value;

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