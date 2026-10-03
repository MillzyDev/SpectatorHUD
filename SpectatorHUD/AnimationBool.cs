using UnityEngine;

namespace SpectatorHUD
{
    [Serializable]
    public struct AnimationBool
    {
        public Animator animator;
        public string parameterName;
        public bool value;
    }
}