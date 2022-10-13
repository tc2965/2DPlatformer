using Unity.Collections;
using UnityEngine;

// May have to look to building a policy object for enforcing things like a lifetime policy.

public enum BunnyMessagePriority
{
    LOW,
    MEDIUM,
    HIGH
}

public abstract class BunnyMessage<T> where T : class
{

    public BunnyEntity to;
    public BunnyEntity from;
    public BunnyMessagePriority priority;
    public float lifetime;
    public T payload;

    public abstract T GetValue();
}
