using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnySpeakerComponent : MonoBehaviour
{
    public DialogueSpeakerMap Map;
    public BunnyFactEntry speaker;
    //public BunnyFactEntry or maybe a descendant of it that's the SO
    private void OnEnable()
    {
        // Map.A -- Add SO here
    }

    private void OnDisable()
    {

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
