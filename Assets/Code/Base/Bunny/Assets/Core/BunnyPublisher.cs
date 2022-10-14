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
        BunnyBroker.GetInstance().Publish(new BunnyMessage(
            EventName,
            act,
            this,
            BunnyChannelType.DefaultChannel
        ));
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
