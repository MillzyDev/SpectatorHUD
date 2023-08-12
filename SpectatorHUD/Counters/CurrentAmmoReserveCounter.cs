using System;
using UnityEngine;

namespace SpectatorHUD.Counters
{
    public class CurrentAmmoReserveCounter : MonoBehaviour
    {
        public int value;
        public DisplayMode displayMode = DisplayMode.Combined;

        public bool useCustomFormatting;
        public string format = "{0}";

        public CurrentAmmoReserveCounter(IntPtr ptr) : base(ptr)
        {
        }

        public enum DisplayMode
        {
            Combined,
            Dominant,
            Left,
            Right
        }
    }
}
