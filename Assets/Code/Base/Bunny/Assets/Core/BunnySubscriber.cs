using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResponseTest {
    public bool val = true;
    public ResponseTest(bool val) {
        this.val = val;
    }

    public ResponseTest getVal()
    {
        return this;
    }
}

public class BunnySubscriber : MonoBehaviour
{

    public string EventName;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> {}
    public BoolEvent OnBooleanMessageReceive;
    public event Action<ResponseTest> act;
    public ResponseTest stuff = new ResponseTest(false);

    private void Awake()
    {
        act = testStuff;
        BunnyBroker.Instance.Subscribe(new BunnyMessage(
            EventName,
            act,
            this,
            BunnyChannelType.DefaultChannel
        ));
    }

    public void testStuff(ResponseTest test) {
        Debug.Log("Got parms " + test.val);
    }


    public void MessageReceived(BunnyMessage message)
    {
        ResponseTest data = message.GetValue<ResponseTest>();
        Debug.Log("Got data back which is" + data.val);
    }

    private void OnDestroy()
    {
        act = testStuff;
        BunnyBroker.Instance.Unsubscribe(new BunnyMessage(
            EventName,
            act,
            this,
            BunnyChannelType.DefaultChannel
        ));
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
