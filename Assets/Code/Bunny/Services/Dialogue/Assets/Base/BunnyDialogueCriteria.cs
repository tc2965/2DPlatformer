using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyDialogueCriteria
{
  public List<BunnyFactEntry> Facts;

<<<<<<< HEAD:Assets/Code/Services/Dialogue/Assets/Base/BunnyDialogueCriteria.cs
  public bool IsFulfilled()
  {
    foreach (BunnyFactEntry fact in Facts)
    {
    }
=======
  public bool IsSatisfied()
  {
    // Will search DB tables for current fact status
>>>>>>> 5b66e9d0cd913d7d34565b9097e8314304bd2d84:Assets/Code/Bunny/Services/Dialogue/Assets/Base/BunnyDialogueCriteria.cs
    return false;
  }
}
