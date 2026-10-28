/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This software is licensed under the terms of the Functional Source License,
 *      Version 1.1, ALv2 Future License.  You are permitted to use, copy, modify,
 *      create derivative works, publicly display, and redistribute it under the the
 *      terms of the license.
 *
 *      THE SOFTWARE IS PROVIDED "AS IS" AND WITHOUT WARRANTIES OF ANY KIND, EXPRESS OR
 *      IMPLIED, INCLUDING WITHOUT LIMITATION WARRANTIES OF FITNESS FOR A PARTICULAR
 *      PURPOSE, MERCHANTABILITY, TITLE OR NON-INFRINGEMENT.
 *
 *      You should have received a copy of the Functional Source License, Version 1.1,
 *      ALv2 Future License along with this program.  If not, see https://fsl.software/.
 */

using System.Reflection;
using Il2CppSLZ.Marrow;
using UnityEngine;

namespace SpectatorHUD;

public class HudBootstrap(IntPtr ptr) : MonoBehaviour(ptr)
{
    public const string DefaultHud = "SpectatorHUD.Resources.Half-Life 2.hud";
    public GameObject? hud;

    private RigManager? _rigManager;
    public DateTime lastLoad;

    public void Start()
    {
        this._rigManager = this.GetComponent<RigManager>();
        this.LoadHudFromConfig();
    }

    private void OnDestroy()
    {
    }

    public void UnloadCurrentHud()
    {
        if (this.hud == null)
        {
            return;
        }

        Destroy(this.hud);
    }

    public void ReloadHudFromConfig()
    {
        this.UnloadCurrentHud();
        this.LoadHudFromConfig();
        this.hud?.SetActive(true);
    }

    public void LoadHudFromConfig()
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

    public void LoadHudFromFile(string filePath)
    {
        Logger.Msg("Loading bundle at: {0}", filePath);
        AssetBundle assetBundle = AssetBundle.LoadFromFile(filePath);
        if (assetBundle == null)
        {
            Logger.Error("Failed to load asset bundle.");
            //Destroy(this);
            return;
        }

        this.LoadHudFromAssetBundle(assetBundle);
        assetBundle.Unload(false); // free asset bundle resource
    }

    public void LoadHudFromEmbeddedResource(string resource)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream(resource);

        if (stream == null)
        {
            Logger.Error("Unable to open stream to embedded resource");
            return;
        }

        MemoryStream memoryStream = new();
        stream.CopyTo(memoryStream);

        AssetBundle assetBundle = AssetBundle.LoadFromMemory(memoryStream.ToArray());
        this.LoadHudFromAssetBundle(assetBundle);
    }

    public void LoadHudFromAssetBundle(AssetBundle assetBundle)
    {
        HudVersion? hudVersion = assetBundle.LoadAsset("Hud.Version.asset").TryCast<HudVersion>();
        if (hudVersion == null)
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
                return;
        }

        MetaInfo.HUDCompatibility = hudVersion.version;
        this.lastLoad = DateTime.Now;
    }

    private void LoadHudV1(AssetBundle assetBundle)
    {
        HudV1? hudInfo = assetBundle.LoadAsset("Hud.asset").TryCast<HudV1>();
        if (hudInfo == null)
        {
            Logger.Error("Hud.asset could not be loaded from asset bundle.");
            //Destroy(this);
            return;
        }

        Logger.Msg("Loaded {0} v{1} by {2}", hudInfo.hudName, hudInfo.hudVersion, hudInfo.hudAuthor);
        this.hud = Instantiate(hudInfo.hudAsset);
        this.hud.name = "SpectatorHUD UI";

        MetaInfo.HUDName = hudInfo.hudName;
        MetaInfo.HUDAuthor = hudInfo.hudAuthor;
        MetaInfo.HUDVersion = hudInfo.hudVersion;

        this.HudRun();

        this.hud.SetActive(false); // gets set active when the loading scene disappears
    }

    private void HudRun()
    {
        if (this._rigManager == null)
        {
            Logger.Error("FUCKKK WHERES THE RIG MANAGER");
            return;
        }

        HudState.RigManager = this._rigManager;
    }
}