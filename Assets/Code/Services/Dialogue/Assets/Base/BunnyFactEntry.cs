using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Used to get facts of the world.
EG player_visited_npc_count = 0
*/
public enum BunnyFactEntryScope 
{
    GLOBAL,
    AREA,
    SCENE,
    TEMPORARY
}
public class BunnyFactEntry : BunnyBaseEntry
{
    private BunnyFactEntryScope scope;
    public BunnyFactEntryScope Scope => scope;

    public BunnyFactEntry(BunnyFactEntryScope newScope)
    {
        scope = newScope;
    }
}
