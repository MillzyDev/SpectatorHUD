using UnityEngine;

namespace SpectatorHUD.Animation
{
    [Serializable]
    public struct AnimationTriggerReference
    {
        public Animator animator;
        public string parameterName;
    }
}