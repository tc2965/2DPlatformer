using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BunnyRulePriority
{
    Low,
    Medium,
    High
}

// Core building block of dialogue
// When an event is dispatched, all rules listening to it are gathered 1 by 1
// The rule that matches with the highest specificity (most criteria matched) runs
// Algorithm should have a sorted collection for rules as the algorithm should stop at the first rule found.

// Events are also facts under the hood can be considered within the criterion.
// Conversations that have several rules will use the same event.
// They'll have criteria that requires the event entry to be their position in the dialogue sequence.

public class BunnyRuleEntry : BunnyBaseEntry
{
    public BunnyDialogueCriteria[] criterion;
    public string Key;
    public bool Once;
    public bool IsCancellable;
    public float Padding;
    public float Delay;
    public BunnyEventEntry TriggeredBy;
    public BunnyEventEntry Triggers;
    public BunnyFactEntry Speaker;
    public string Text;

    public BunnyRuleEntry(
        BunnyDialogueCriteria[] criteria,
        BunnyEventEntry trigger,
        BunnyFactEntry speaker,
        string dialogue
    )
    {
        criterion = criteria;
        TriggeredBy = trigger;
        this.Speaker = speaker;
        Text = dialogue;
    }

    public BunnyRuleEntry(
        BunnyDialogueCriteria[] criteria,
        BunnyEventEntry trigger,
        BunnyFactEntry speaker,
        string dialogue,
        BunnyEventEntry next
    )
    {
        criterion = criteria;
        TriggeredBy = trigger;
        this.Speaker = speaker;
        Text = dialogue;
        Triggers = next;
    }

    public bool Validate()
    {
        for(var i=0; i < criterion.Length; i++)
        {
            if(!criterion[i].IsSatisfied()) 
                return false;
        }
        return true;
    }

    // Int passed will be event entry ID
    public void Execute(BunnyBrokerMessage<int> message)
    {
        for(var i=0; i < criterion.Length; i++)
        {
            if(!criterion[i].IsSatisfied()) 
                return;  
        }
    }

    public int GetPriority()
    {
        return criterion.Length;
    }
}
