using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyDialogueManager
{
    public static BunnyDialogueDatabase Database = (BunnyDialogueDatabase) ScriptableObject.CreateInstance("BunnyDialogueDatabase");
    public DialogueSpeakerMap Speakers = (DialogueSpeakerMap) ScriptableObject.CreateInstance("DialogueSpeakerMap");
    private static BunnyDialogueManager _instance;
    private static readonly object padlock = new object();
    private BunnyDialogueManager()
    {
        // CommonTableSO commonTable = new CommonTableSO();
        // speakers = new DialogueSpeakerMap();
        BunnyDialogueManager.EnterFacts(CommonTableSO.Data, Database);

    }
    public static BunnyDialogueManager Instance
    {
        get
        {
            if(_instance == null)
                _instance = new BunnyDialogueManager();
            return _instance;
        }
    }

    public static void EnterFacts(Dictionary<string, BunnyFactEntry> data, BunnyDialogueDatabase db)
    {
        foreach(KeyValuePair<string, BunnyFactEntry> entry in data)
        {
            db.Add(entry.Key, entry.Value);
        }
    }

}
