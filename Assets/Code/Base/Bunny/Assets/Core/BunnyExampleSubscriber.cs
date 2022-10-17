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
        // BunnyMessageBroker.Instance.Subscribe<ResponseTest>(act, BunnyChannelType.DefaultChannel);
        BunnyMessageBroker.Instance.testEvent.OnEventRaised<ResponseTest>(act);
        // act = testStuff;
        // BunnyBroker.Instance.Subscribe(new BunnyMessage(
        //     EventName,
        //     act,
        //     this,
        //     BunnyChannelType.DefaultChannel
        // ));
    }

    public void testNewStuff(BunnyBrokerMessage<ResponseTest>  msg)
    {
        Debug.Log("Got invoked to show " + msg.payload.val);
    }

    private void OnDestroy()
    {
        // act = testNewStuff;
        BunnyMessageBroker.Instance.Unsubscribe<ResponseTest>(act, BunnyChannelType.DefaultChannel);
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
