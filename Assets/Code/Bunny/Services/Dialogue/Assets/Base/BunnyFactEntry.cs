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

    public BunnyFactEntry(int ID, BunnyFactEntryScope newScope = BunnyFactEntryScope.TEMPORARY)
    {
        this.ID = ID;
        this.scope = newScope;
    }

    // public static BunnyFactEntry operator +(BunnyFactEntry entry, int b)
    // {
    //    return new BunnyFactEntry(entry.ID + b, entry.Scope);
    // }

    // public static BunnyFactEntry operator -(BunnyFactEntry entry, int b)
    // {
    //    return new BunnyFactEntry(entry.ID - b, entry.Scope);
    // }

    // public static BunnyFactEntry operator *(BunnyFactEntry entry, int b)
    // {
    //    return new BunnyFactEntry(entry.ID * b, entry.Scope);
    // }

    // public static BunnyFactEntry operator /(BunnyFactEntry entry, int b)
    // {
    //     return new BunnyFactEntry(entry.ID / b, entry.Scope);
    // }

    // public static bool operator >(BunnyFactEntry entry, int b)
    // {
    //     return entry.ID > b;
    // }

    // public static bool operator <(BunnyFactEntry entry, int b)
    // {
    //     return entry.ID < b;
    // }
}
