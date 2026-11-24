using System.Collections.Generic;
using SpectatorHUD.Addons;
using UnityEngine;

namespace SpectatorHUD.Manifest;

public interface IHudManifest
{
    string GetHudName();
    int GetHudVersion();
    string GetHudAuthor();
        
    HudVersion GetSpectatorHudVersion();
    IReadOnlyList<Addon> GetRequiredAddons();
        
    public GameObject GetHudAsset();
}