using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResponseTest {
    public bool val = true;
}

public class BunnyExampleSubscriber : MonoBehaviour
{
    public Action<BunnyBrokerMessage<ResponseTest>> act;

    private void Awake()
    {
        act = testNewStuff;
        BunnyEventManager.Instance.RegisterEvent("PrintTrue", this);
        BunnyEventManager.Instance.OnEventRaised<ResponseTest>("PrintTrue", act);
    }

    public void testNewStuff(BunnyBrokerMessage<ResponseTest>  msg)
    {
        Debug.Log("Got invoked to show " + msg.payload.val);
    }

    private void OnDestroy()
    {
        BunnyEventManager.Instance.Disconnect("PrintTrue");
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
