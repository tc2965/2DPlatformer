using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class BunnyDialogueChoiceBehaviour : PlayableBehaviour 
{
    [SerializeField]
    public List<string> Options;

    [SerializeField]
    public Vector2 offset;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {

    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {

    }
}

