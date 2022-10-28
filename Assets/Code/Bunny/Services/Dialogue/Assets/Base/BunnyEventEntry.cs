using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ID represents the amount of times event was triggered

public class BunnyEventEntry : BunnyBaseEntry
{
    [SerializeField] public string Key;
    [SerializeField] public bool Once;
    private BunnyEvent ClientEvent;
    // public BunnyDialogueRule Next;
    // public BunnyDialogueRule Previous;
    // public Dictionary<string, BunnyDialogueRule> Branches;
    private static BunnyEventEntry _none;

    public static BunnyEventEntry None
    {
        get
        {
            if(_none == null)
                _none = new BunnyEventEntry();
            return _none;
        }
    }

    public void Raise<T>(BunnyBrokerMessage<T> payload)
    {
        if(this.Once)
            return;
        this.ID += 1;
        ClientEvent.Fire<T>(payload);
    }

    public void Bind<T>(Action<BunnyBrokerMessage<T>> callback)
    {
        ClientEvent.OnEventRaised(callback);
    }

    public void Unbind<T>(Action<BunnyBrokerMessage<T>> callback)
    {
        ClientEvent.Disconnect<T>(callback);
    }

    public void OnDisable()
    {
        ClientEvent.Disable();
    }
}
