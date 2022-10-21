using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basics for CAD (Context-Aware Dialogue)
// youtube.com/watch?v=tAbBID3N64A

// Responsible for displaying a dialog on trigger and triggering the next dialogue

public class BunnyDialogueRule
{
    public BunnyDialogueCriteria[] criterion;
    public bool Once;
    public bool IsCancellable;
    public float Padding;
    public float Delay;
    public BunnyEventEntry TriggeredBy;
    public BunnyEventEntry Triggers;
    public BunnyFactEntry Speaker;
    public string Text;

    public BunnyEvent response;
    public Dictionary<string, BunnyDialogueRule> choices;
    public BunnyDialogueRule next;
    public BunnyDialogueNode speech;
    public bool isChoice;

    public void Execute()
    {

    }
}
