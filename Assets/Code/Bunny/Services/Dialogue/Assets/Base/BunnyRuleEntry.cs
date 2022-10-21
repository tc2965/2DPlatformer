using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BunnyRulePriority
{
    Low,
    Medium,
    High
}

public class BunnyRuleEntry : BunnyBaseEntry
{
    public BunnyEvent TriggeredBy;
    public BunnyEvent Triggers;
    public List<BunnyDialogueCriteria> Criteria;
    private BunnyRulePriority _priority;

    public void Execute()
    {

    }

    public BunnyRulePriority GetPriority()
    {
        return _priority;
    }
}
