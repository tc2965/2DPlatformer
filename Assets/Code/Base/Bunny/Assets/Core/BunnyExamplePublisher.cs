using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyExamplePublisher : MonoBehaviour
{
    public void PublishBoolean(bool nval)
    {
        BunnyBrokerMessage<ResponseTest> res = new BunnyBrokerMessage<ResponseTest>(
            new ResponseTest() {
                val = nval
            },
            this
        );
        BunnyMessageBroker.Instance.Publish<ResponseTest>(res);
    }

    private void Awake() {
    }
    // Start is called before the first frame update
    void Start()
    {
         PublishBoolean(false);
        // StartCoroutine(ExampleCoroutine());
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}
