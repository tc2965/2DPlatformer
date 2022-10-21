using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basics for CAD (Context-Aware Dialogue)
// youtube.com/watch?v=tAbBID3N64A

public class BunnyDialogueRule
{
    public BunnyDialogueCriteria[] criterion;
    public BunnyEvent response;
    public Dictionary<string, BunnyDialogueRule> choices;
    public BunnyDialogueRule next;
    public BunnyDialogueNode speech;
    public bool isChoice;
    public BunnyFactEntry speaker;
}
