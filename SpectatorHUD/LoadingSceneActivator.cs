using UnityEngine;

namespace SpectatorHUD
{
    public class LoadingSceneActivator : MonoBehaviour
    {
        public LoadingSceneActivator(IntPtr ptr) : base(ptr)
        {
        }

        private void OnDestroy()
        {
            // Set the HUD as active.
            HudBootstrap bootstrap = Resources.FindObjectsOfTypeAll<HudBootstrap>().First(); // forgive me lord
            bootstrap.hud?.SetActive(true);
        }
    }
}