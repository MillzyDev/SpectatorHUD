/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This program is free software: you can redistribute it and/or modify
 *      it under the terms of the GNU Lesser General Public License as published by
 *      the Free Software Foundation, either version 3 of the License, or
 *      (at your option) any later version.
 *
 *      This program is distributed in the hope that it will be useful,
 *      but WITHOUT ANY WARRANTY; without even the implied warranty of
 *      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *      GNU General Public License for more details.
 *
 *      You should have received a copy of the GNU Lesser General Public License
 *      along with this program.  If not, see https://www.gnu.org/licenses/.
 */

using System.Reflection;
using BoneLib;
using Il2CppInterop.Runtime;
using Il2CppSLZ.Marrow;
using UnityEngine;

namespace SpectatorHUD
{
    public class HudBootstrap : MonoBehaviour
    {
        private static readonly string DefaultHud = "SpectatorHUD.Resources.TestHUD.hud";
        
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
            if (string.IsNullOrEmpty(Config.Instance.Hud))
            {
                // No hud set in melon prefs, attempt loading embedded default
                this.LoadHudFromEmbeddedResource(DefaultHud);
                return;
            }
            
            // Hud path set, attempt to load from file
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
            assetBundle.Unload(false); // free asset bundle resource
        }

        private void LoadHudFromEmbeddedResource(string resource)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream? stream = assembly.GetManifestResourceStream(resource))
            {
                if (stream == null)
                {
                    // TODO: Error
                }
                
                var memoryStream = new MemoryStream();
                stream?.CopyTo(memoryStream);

                AssetBundle assetBundle = AssetBundle.LoadFromMemory(memoryStream.ToArray());
                this.LoadHudFromAssetBundle(assetBundle);
            }
        }

        private void LoadHudFromAssetBundle(AssetBundle assetBundle)
        {
            var hudVersion = assetBundle.LoadAsset("Hud.Version.asset").TryCast<HudVersion>();
            if (hudVersion is null)
            {
                // Version SO is required but not present,
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

            HudState.OpenState(Player.RigManager);
        }

        private void OnDestroy()
        {
            HudState.CloseState();
        }
    }
}