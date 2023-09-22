using System;
using System.Collections.Generic;
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

        private AssetBundle _currentBundle = null!;
        private GameObject _hudRoot = null!;

        private HudManager()
        {
        }

        public static HudManager Instance
        {
            get => s_lazy.Value;
        }

        private static string HudsDirectory
        {
            get => s_hudsDirectory ??= Path.Combine(MelonUtils.UserDataDirectory, "SpectatorHUD", "huds");
        }

        public IEnumerable<string> GetHuds()
        {
            return Directory.GetFiles(HudsDirectory, "*.hud");
        }

        public void LoadHud(RigManager rigManager)
        {
            HudValueWatcher.Instance.OnAmmo += (left, right, bothHands) =>
                MelonLogger.Msg($"Ammo Changed: Left-{left} Right-{right} TwoHands-{bothHands}");

            // TODO: Load HUD
            // TODO: Watch values
            
            HudValueWatcher.Instance.BeginWatch(rigManager);
        }
    }
}
