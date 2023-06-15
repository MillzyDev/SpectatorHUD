using UnityEngine;

namespace SpectatorHUD
{
    public class HudAsset
    {
        public readonly HudManifest HudManifest;
        public readonly GameObject HudRoot;

        public HudAsset(HudManifest hudManifest, GameObject hudRoot)
        {
            HudManifest = hudManifest;
            HudRoot = hudRoot;
        }
    }
}
