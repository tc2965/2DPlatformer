using Unity.Collections;
using UnityEngine;

// May have to look to building a policy object for enforcing things like a lifetime policy.

public enum BunnyMessagePriority
{
    Low,
    Medium,
    High
}

public class BunnyMessage<T> : MonoBehaviour
{

    [ReadOnly] public BunnyEntity to;
    [ReadOnly] public BunnyEntity from;
    [ReadOnly] public BunnyMessagePriority priority;
    public float lifetime;
    public object _payload;

    public T GetValue<T>()
    {
        if(_payload is T) {
            return (T)_payload;
        }
        return default(T);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
