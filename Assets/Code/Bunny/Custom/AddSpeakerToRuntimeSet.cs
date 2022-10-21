using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpeakerToRuntimeSet : MonoBehaviour
{
    public BunnySpeakerRuntimeSet runtimeSet;

    private void OnEnable()
    {
        runtimeSet.AddToList(this.gameObject);
    }

    private void OnDisable()
    {
        runtimeSet.AddToList(this.gameObject);
    }
}
