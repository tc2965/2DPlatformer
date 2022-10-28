using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnySpeakerComponent : MonoBehaviour
{
    public DialogueSpeakerMap Map;
    public BunnyFactEntry speaker;
    //public BunnyFactEntry or maybe a descendant of it that's the SO

    private void Awake()
    {
        Map = BunnyDialogueManager.Instance.Speakers;
        speaker = new BunnyFactEntry(0, this.gameObject.name);
    }

    private void OnEnable()
    {
        // Later we want to only add to it if the entity is near the player
        // In other words, if the player is within the interaction region of the entity, the runtimeset includes it
        Map.Add(speaker, this);
    }

    private void OnDisable()
    {
        Map.Remove(speaker);
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
