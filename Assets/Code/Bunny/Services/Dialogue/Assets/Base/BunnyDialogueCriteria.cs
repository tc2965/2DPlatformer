using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyDialogueCriteria
{
  public List<BunnyFactEntry> Facts;

  public bool IsSatisfied()
  {
    // Will search DB tables for current fact status
    return false;
  }
}
