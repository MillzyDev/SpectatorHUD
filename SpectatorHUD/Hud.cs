using System;
using UnityEngine;

namespace SpectatorHUD
{
    public class Hud : MonoBehaviour
    {
        public HudConfigSO hudConfig = null!;

        public Hud(IntPtr ptr) : base(ptr)
        {
        }
    }
}
