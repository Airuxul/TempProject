using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace TimelineExpand
{
    public class DialogBehaviour : PlayableBehaviour
    {
        private PlayableDirector _director;
        private bool _isEnterClip;
        private const string PrefabAssetPath = "Assets/Art/Prefab/Cube.prefab";
        private GameObject _handleGo;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (!_isEnterClip)
            {
                _handleGo = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(PrefabAssetPath));
                _director =  playable.GetGraph().GetResolver() as PlayableDirector;
                _isEnterClip = true;
                Debug.Log ($@"OnClipStart");
            }
        }
        
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (_isEnterClip && info.effectivePlayState == PlayState.Paused)
            {
                _director.Pause();
            }
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            if (_handleGo != null)
            {
                Object.Destroy(_handleGo);
            }
        }
    }
}
