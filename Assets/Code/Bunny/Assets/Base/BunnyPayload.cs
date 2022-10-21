using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BunnyPayload<T>
{
    [SerializeField] private T payload;
    public T Payload  => payload;

    public BunnyPayload(T initialPayload)
    {
        payload = initialPayload;
    }
}