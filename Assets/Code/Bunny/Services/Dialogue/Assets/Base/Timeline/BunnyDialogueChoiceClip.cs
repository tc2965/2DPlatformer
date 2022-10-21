using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class BunnyDialogueChoiceClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField]
    public BunnyDialogueChoiceBehaviour template = new BunnyDialogueChoiceBehaviour();

    public ClipCaps clipCaps
    {
        get
        {
            return ClipCaps.None;
        }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<BunnyDialogueChoiceBehaviour>.Create(graph, template);
    }
}
