using UnityEngine;
using UnityEngine.Playables;

namespace TimelineExpand
{
    public class DialogClip : PlayableAsset
    {
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return ScriptPlayable<DialogBehaviour>.Create(graph);
        }
    }
}