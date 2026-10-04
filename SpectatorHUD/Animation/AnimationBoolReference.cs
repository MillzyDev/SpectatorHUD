using UnityEngine;

namespace SpectatorHUD.Animation
{
    [Serializable]
    public struct AnimationBoolReference
    {
        public Animator animator;
        public string parameterName;
    }
}