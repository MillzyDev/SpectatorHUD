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
    public class HudDispatcher : MonoBehaviour
    {
        private static readonly string DefaultHud = "SpectatorHUD.Resources.TestHUD.hud";
        
        private RigManager? _rigManager;
        public GameObject? hud;

        private Il2CppSystem.Action<HandReciever>? _onLeftHandAttached;
        private Il2CppSystem.Action<HandReciever>? _onRightHandAttached;
        private Il2CppSystem.Action<HandReciever>? _onLeftHandDetached;
        private Il2CppSystem.Action<HandReciever>? _onRightHandDetached;

        public HudDispatcher(IntPtr ptr) : base(ptr)
        {
            // Create IL2CPP converted delegates for this instance
            this._onLeftHandAttached =
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(this.LeftHandAttached);
            this._onRightHandAttached =
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(this.RightHandAttached);
            this._onLeftHandDetached =
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(this.LeftHandDetached);
            this._onRightHandDetached =
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(this.RightHandDetached);
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
            
            //var hudManager = this.hud.GetComponentInChildren<HudManagerV1>();
            
            this.SubscribeToRigEvents();
        }

        private void OnDestroy()
        {
            // Unity can deal with the HUD
            this.UnsubscribeFromRigEvents(); // must clean up
        }

        private void SubscribeToRigEvents()
        {
            Player.LeftHand.onRecieverAttached += this._onLeftHandAttached;
            Player.RightHand.onRecieverAttached += this._onRightHandAttached;
            Player.LeftHand.onRecieverDetached += this._onLeftHandDetached;
            Player.RightHand.onRecieverDetached += this._onRightHandDetached;
        }

        private void UnsubscribeFromRigEvents()
        {
            Player.LeftHand.onRecieverAttached -= this._onLeftHandAttached;
            Player.RightHand.onRecieverAttached -= this._onRightHandAttached;
            Player.LeftHand.onRecieverDetached -= this._onLeftHandDetached;
            Player.RightHand.onRecieverDetached -= this._onRightHandDetached;
        }
        
        private void LeftHandAttached(HandReciever handReciever)
        {
            Grip? grip = handReciever.TryCast<Grip>();

            // confirm the receiver is a grip (I think this is always the case but doesn't hurt to be safe)
            if (grip == null)
            {
                HudEvents.OnLeftHeldItemChanged(true, null, false);
                return;
            }
            
            // Search for gun component
            Gun? gun = grip.GetComponentInParent<Gun>();
            if (gun == null)
            {
                HudEvents.OnLeftHeldItemChanged(true, null, false);
                return;
            }
            
            // Holding a gun, check that we're holding the trigger grip for the event
            HudEvents.OnLeftHeldItemChanged(true, gun, gun.triggerGrip == grip);
            Logger.Debug("Holding gun in left hand:" + gun.name);
        }

        private void RightHandAttached(HandReciever handReciever)
        {
            // See LeftHandAttached
            Grip? grip = handReciever.TryCast<Grip>();

            if (grip == null)
            {
                HudEvents.OnRightHeldItemChanged(true, null, false);
                return;
            }
            
            Gun? gun = grip.GetComponentInParent<Gun>();
            if (gun == null)
            {
                HudEvents.OnRightHeldItemChanged(true, null, false);
                return;
            }
            
            HudEvents.OnRightHeldItemChanged(true, gun, gun.triggerGrip == grip);
            Logger.Debug("Holding gun in right hand:" + gun.name);
        }

        private void LeftHandDetached(HandReciever _)
        {
            HudEvents.OnLeftHeldItemChanged(false, null, false);
            Logger.Debug("Released item in left hand.");
        }

        private void RightHandDetached(HandReciever _)
        {
            HudEvents.OnRightHeldItemChanged(false, null, false);
            Logger.Debug("Released item in right hand.");
        }
    }
}