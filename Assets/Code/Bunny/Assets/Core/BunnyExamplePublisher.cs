using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyExamplePublisher : MonoBehaviour
{
    public void PublishBoolean(bool nval)
    {
        // @mohammedajao - Our arguments (ResponseTest) is wrapped in an wrapper
        // All our necessary data and arguments are inside this wrapper
        // To send your own event, make an class of the expected arguments
        // Wrap that class like this: BunnyBrokerMessage<YourNewclass>
        // Fire while specifying the type through the EventManager

        // The following will error because we do not accept duplicate event names
        // BunnyEventManager.Instance.RegisterEvent("PrintTrue", this);
        BunnyBrokerMessage<ResponseTest> res = new BunnyBrokerMessage<ResponseTest>(
            new ResponseTest() {
                val = nval
            },
            this
        );
        BunnyEventManager.Instance.Fire<ResponseTest>("PrintTrue", res);
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
