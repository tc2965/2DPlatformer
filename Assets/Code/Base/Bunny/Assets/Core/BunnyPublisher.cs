using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyPublisher : MonoBehaviour
{
    public string EventName;
    public event Action<ResponseTest> act;
    public void PublishBoolean(bool val)
    {
        act = testStuff;
        BunnyBroker.GetInstance().Publish(new BunnyMessage(
            EventName,
            act,
            this,
            BunnyChannelType.DefaultChannel
        ));
    }


    public void testStuff(ResponseTest test) {
        Debug.Log("Got parms " + test.val);
    }

    private void Awake() {
    }
    // Start is called before the first frame update
    void Start()
    {
        PublishBoolean(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
