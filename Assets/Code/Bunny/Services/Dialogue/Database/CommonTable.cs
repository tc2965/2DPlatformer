using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// [TODO] @mohammedajao - Add a custom editor for the table's data to be viewable in the editor
[CreateAssetMenu(menuName = "BDS/Data/Common")]
public class CommonTableSO: ScriptableObject
{
    public static Dictionary<string, BunnyFactEntry> Data = new Dictionary<string, BunnyFactEntry>{
        {"enemies_killed",  new BunnyFactEntry(0, "enemies_killed", BunnyFactEntryScope.GLOBAL)},
        {"player_deaths", new BunnyFactEntry(0, "player_deaths", BunnyFactEntryScope.GLOBAL)}
    };

    public CommonTableSO()
    {
        Data["enemies_killed"] = new BunnyFactEntry(0, "enemies_killed", BunnyFactEntryScope.GLOBAL);
        Data["player_deaths"] = new BunnyFactEntry(0, "player_deaths", BunnyFactEntryScope.GLOBAL);
    }

    public BunnyFactEntry InsertNewFact(string FactName, string key, int value)
    {
        if(Data.ContainsKey(FactName))
        {
            Debug.LogWarning($"Key:{FactName} is being over-written for the Global Scope. This behaviour is discouraged.");
        }
        BunnyFactEntry fact = new BunnyFactEntry(value, key, BunnyFactEntryScope.GLOBAL);
        Data.Add(FactName, fact);
        return fact;
    }

    public void RemoveFact(string FactName)
    {
        Data.Remove(FactName);
    }

    public BunnyFactEntry GetFact(string FactName)
    {
        return Data[FactName];
    }


}