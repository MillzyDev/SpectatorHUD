using Il2CppSLZ.Marrow;
using UnityEngine;

namespace SpectatorHUD
{
    public class HudBootstrap : MonoBehaviour
    {
        private RigManager? _rigManager;
        public GameObject? hud;

        public HudBootstrap(IntPtr ptr) : base(ptr)
        {
        }

        public void Start()
        {
            this._rigManager = this.GetComponent<RigManager>();
            this.LoadHudFromConfig();
        }

        private void LoadHudFromConfig()
        {
            if (Config.Instance.Hud is null)
            {
                return;
            }
            
            this.LoadHudFromFile(Config.Instance.Hud);
        }

        private void LoadHudFromFile(string filePath)
        {
            Logger.Msg("Loading bundle at: {0}", filePath);
            AssetBundle assetBundle = AssetBundle.LoadFromFile(filePath);
            if (assetBundle is null)
            {
                Logger.Error("Failed to load asset bundle.");
                //Destroy(this);
                return;
            }
            this.LoadHudFromAssetBundle(assetBundle);
            assetBundle.Unload(false);
        }

        private void LoadHudFromAssetBundle(AssetBundle assetBundle)
        {
            var hudVersion = assetBundle.LoadAsset("Hud.Version.asset").TryCast<HudVersion>();
            if (hudVersion is null)
            {
                Logger.Error("Hud Version asset was not found in asset bundle.");
                //Destroy(this);
                return;
            }

            switch (hudVersion.version)
            {
                case 1:
                    this.LoadHudV1(assetBundle);
                    break;
                default:
                    Logger.Error("Unsupported HUD version. You may need to update the mod.");
                    Destroy(this);
                    break;
            }
        }

        private void LoadHudV1(AssetBundle assetBundle)
        {
            var hudInfo = assetBundle.LoadAsset("Hud.asset").TryCast<HudV1>();
            if (hudInfo is null)
            {
                Logger.Error("Hud.asset could not be loaded from asset bundle.");
                //Destroy(this);
                return;
            }
            
            Logger.Msg("Loaded {0} v{1} by {2}", hudInfo.hudName, hudInfo.hudVersion, hudInfo.hudAuthor);
            this.hud = GameObject.Instantiate(hudInfo.hudAsset);
            this.hud.name = "SpectatorHUD UI";
            
            var hudManager = this.hud.GetComponent<HudManager>();
            hudManager.rigManager = this._rigManager;
            hudManager.hud = this.hud;
        }
    }
}