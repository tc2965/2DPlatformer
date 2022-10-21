using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class BunnyDialogueClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField]
    public BunnyDialogueBehaviour template = new BunnyDialogueBehaviour();
    
    public ClipCaps clipCaps
    {
        get
        {
            return ClipCaps.None;
        }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<BunnyDialogueBehaviour>.Create(graph, template);
    }
}
