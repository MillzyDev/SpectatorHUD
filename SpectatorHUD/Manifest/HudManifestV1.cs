using System.Collections.Generic;
using SpectatorHUD.Addons;
using UnityEngine;

namespace SpectatorHUD.Manifest;

public class HudManifestV1(HudV1 hudInfo) : IHudManifest
{
    public string GetHudName() => hudInfo.hudName;

    public int GetHudVersion() => hudInfo.hudVersion;

    public string GetHudAuthor() => hudInfo.hudAuthor;

    public HudVersion GetSpectatorHudVersion() => hudInfo.spectatorHudVersion;

    public IReadOnlyList<Addon> GetRequiredAddons() => [];

    public GameObject GetHudAsset() => hudInfo.hudAsset;
}