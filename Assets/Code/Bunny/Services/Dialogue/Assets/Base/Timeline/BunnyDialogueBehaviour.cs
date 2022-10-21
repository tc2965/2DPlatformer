using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum ChatBubbleDirection
{
    TopLeft,
    TopRight
}

public class BunnyDialogueBehaviour : PlayableBehaviour
{
    [SerializeField]
    public string Text;

    [SerializeField]
    public Vector2 offset;


    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {

    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        
    }
}
