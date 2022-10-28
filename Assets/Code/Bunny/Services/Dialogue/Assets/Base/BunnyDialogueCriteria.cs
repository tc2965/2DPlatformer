using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BunnyDialogueCriteria
{
  public BunnyBaseEntry[] Facts;

  public virtual bool IsSatisfied()
  {
    // Will search DB tables for current fact status
    return false;
  }
}
