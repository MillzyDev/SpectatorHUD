using UnityEngine;

namespace SpectatorHUD.Animation
{
    [Serializable]
    public struct AnimationFloatReference
    {
        public Animator animator;
        public string parameterName;
    }
}