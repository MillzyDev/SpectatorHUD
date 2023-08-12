using System;
using UnityEngine;

namespace SpectatorHUD
{
    // ReSharper disable once InconsistentNaming
    public class HudManifestSO : ScriptableObject
    {
        public string hudName = "Cool hud";
        public string author = "Me";
        public ushort version = 1;
        public string description = "A cool hud.";

        public HudManifestSO(IntPtr ptr) : base(ptr)
        {
        }
    }
}
