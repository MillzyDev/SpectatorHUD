using System.Collections.Generic;
using SpectatorHUD.Addons;
using UnityEngine;

namespace SpectatorHUD.Manifest;

public class HudManifestV2(HudV2 hudInfo) : IHudManifest
{
    public string GetHudName() => hudInfo.hudName;

    public int GetHudVersion() => hudInfo.hudVersion;

    public string GetHudAuthor() => hudInfo.hudAuthor;

    public HudVersion GetSpectatorHudVersion()  => hudInfo.spectatorHudVersion;

    public IReadOnlyList<Addon> GetRequiredAddons() => hudInfo.requiredAddons;

    public GameObject GetHudAsset() => hudInfo.hudAsset;
}