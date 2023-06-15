using MelonLoader;
using Ninject;
using UnityEngine;

namespace SpectatorHUD.Managers
{
    public class HudManager : MonoBehaviour
    {
        private HudAssetContainer _hudAssetContainer = null!;
        private HudValueManager _hudValueManager = null!;

        [Inject]
        public void Inject(HudAssetContainer hudAssetContainer, HudValueManager hudValueManager)
        {
            _hudAssetContainer = hudAssetContainer;
            _hudValueManager = hudValueManager;
        }

        private void Start()
        {
#pragma warning disable
            if (_hudAssetContainer.LoadHudAsset(out HudAsset hudAsset))
#pragma warning restore CS8600
            {
                GameObject hudRootPrefab = hudAsset.HudRoot;
                GameObject hudRoot = Instantiate(hudRootPrefab);
                hudRoot.SetActive(false);
                hudRoot.GetComponent<HudRoot>().HudValueManager = _hudValueManager;
                hudRoot.SetActive(true);
            } else
                MelonLogger.Error("Failed to load HUD.");
        }
    }
}
