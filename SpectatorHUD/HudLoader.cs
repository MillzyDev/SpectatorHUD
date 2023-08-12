using System;
using System.IO;
using MelonLoader;
using UnityEngine;

namespace SpectatorHUD
{
    internal class HudLoader
    {
        private static readonly Lazy<HudLoader> s_lazy = new(
            () => new HudLoader()
            );

        private static string? s_hudsDirectory;
        
        private GameObject _hudRoot = null!;

        private HudLoader()
        {
        }

        public static HudLoader Instance
        {
            get => s_lazy.Value;
        }

        public static string HudsDirectory
        {
            get => s_hudsDirectory ??= Path.Combine(MelonUtils.UserDataDirectory, "SpectatorHUD", "HUDs");
        }
    }
}
