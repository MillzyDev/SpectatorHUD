using System;
using System.Collections.Generic;
using SpectatorHUD.Addons;
using UnityEngine;

namespace SpectatorHUD;

public class HudV2(IntPtr ptr) : ScriptableObject(ptr)
{
    public string hudName;
    public int hudVersion;
    public string hudAuthor;

    public HudVersion spectatorHudVersion;
    public List<Addon> requiredAddons;

    public GameObject hudAsset;
}