using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBunnyDialogueCriteria {}

public class BunnyDialogueCriteria
{
  public List<BunnyFactEntry> Facts;

  public bool IsFulfilled()
  {
    foreach (BunnyFactEntry fact in Facts)
    {
    }
    return false;
  }
}
