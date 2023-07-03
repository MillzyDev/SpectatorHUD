using System;
using System.IO;
using MelonLoader;
using Newtonsoft.Json;
using Ninject;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SpectatorHUD.Managers
{
    public class HudManager : IInitializable, IDisposable
    {
        private static Tuple<string, HudManifest, AssetBundle>? s_currentAssetBundle;
        private readonly Config _config;

        private readonly IKernel _kernel;

        private GameObject? _hud;

        [Inject]
        public HudManager(Config config, IKernel kernel)
        {
            _config = config;
            _kernel = kernel;
        }

        public void Dispose()
        {
            Object.Destroy(_hud);
        }

        public void Initialize()
        {
            string selectedHud = _config.SelectedHud;

            if (s_currentAssetBundle == null || s_currentAssetBundle.Item1 != selectedHud)
            {
                string selectedHudPath = Path.Combine(Config.HUDsDirectoryPath, selectedHud);

                if (!File.Exists(selectedHudPath))
                {
                    MelonLogger.Error("Selected HUD does not exist. Please make sure the config value is correct.");
                    return;
                }

                AssetBundle assetBundle = AssetBundle.LoadFromFile(selectedHudPath);
                assetBundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;

                if (!assetBundle)
                {
                    MelonLogger.Error("Failed to load asset bundle from disk.");
                    return;
                }
            
                var manifestAsset = new TextAsset(assetBundle.LoadAsset("manifest.json").Pointer);
                var manifest = JsonConvert.DeserializeObject<HudManifest>(manifestAsset.text);

                if (manifest == null)
                {
                    MelonLogger.Error(
                        $"Failed to deserialize the manifest for {_config.SelectedHud}.\nText asset value: {manifestAsset}.");
                    return;
                }

                s_currentAssetBundle =
                    new Tuple<string, HudManifest, AssetBundle>(selectedHud, manifest, assetBundle);
            }

            string prefabAsset = s_currentAssetBundle.Item2.PrefabAsset;
            var prefab = new GameObject(s_currentAssetBundle.Item3.LoadAsset(prefabAsset).Pointer);
            _hud = Object.Instantiate(prefab);
            
            _hud.SetActive(false);
            var hudRoot = _hud.GetComponent<HudRoot>();
            _kernel.Inject(hudRoot);
            _hud.SetActive(true);
        }
    }
}
