using UnityEngine;

namespace SpectatorHUD.Animation
{
    [Serializable]
    public struct AnimationIntReference
    {
        public Animator animator;
        public string parameterName;
    }
}