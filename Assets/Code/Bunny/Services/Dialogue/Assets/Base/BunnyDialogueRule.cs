using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basics for CAD (Context-Aware Dialogue)
// youtube.com/watch?v=tAbBID3N64A

// Responsible for displaying a dialog on trigger and triggering the next dialogue

public class BunnyDialogueRule : BunnyBaseEntry
{
    public BunnyEvent response;
    public Dictionary<string, BunnyDialogueRule> choices;
    public BunnyDialogueRule next;
    public BunnyDialogueNode speech;

    public void Execute()
    {

    }
}
