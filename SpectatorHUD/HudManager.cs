using System;
using System.IO;
using MelonLoader;
using SLZ.Rig;
using UnityEngine;

namespace SpectatorHUD
{
    internal class HudManager
    {
        private static readonly Lazy<HudManager> s_lazy = new(
            () => new HudManager()
            );

        private static string? s_hudsDirectory;
        
        private GameObject _hudRoot = null!;

        private HudManager()
        {
        }

        public static HudManager Instance
        {
            get => s_lazy.Value;
        }

        public static string HudsDirectory
        {
            get => s_hudsDirectory ??= Path.Combine(MelonUtils.UserDataDirectory, "SpectatorHUD", "HUDs");
        }

        public void LoadHud(RigManager rigManager)
        {
            
        }
    }
}
