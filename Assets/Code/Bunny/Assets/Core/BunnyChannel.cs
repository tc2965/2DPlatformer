using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Channel/Topic for PubSub
Broker will go to a channel 
*/

public enum BunnyChannelType {
    AudioChannel,
    DefaultChannel
}

public enum BunnyChannelPriortity {
    Low,
    Medium,
    High
}

public class BunnyChannel
{
    public BunnyChannelType type = BunnyChannelType.DefaultChannel;
    public delegate void Publisher();
    private string name;

    public Dictionary<string, List<Delegate>> EventLibrary = new Dictionary<string, List<Delegate>>();

    public BunnyChannel(string name)
    { 
        this.name = name;
    }
}
