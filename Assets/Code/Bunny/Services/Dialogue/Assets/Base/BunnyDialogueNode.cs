using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBunnyDialogue{}

public class BunnyDialogueNode
{
    public string sentence;
    public static BunnyDialogueNode None 
    {
        get
        {
            return new BunnyDialogueNode("");
        }
    }

    public BunnyDialogueNode(string sentence)
    {
        this.sentence = sentence;
    }
}
