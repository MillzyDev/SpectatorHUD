using System;
using UnityEngine;

namespace SpectatorHUD.Counters
{
    public class AmmoCounter : MonoBehaviour
    {
        public int value;
        public DisplayMode displayMode = DisplayMode.Combined;

        public bool useCustomFormatting;
        public string format = "{0}";

        public AmmoCounter(IntPtr ptr) : base(ptr)
        {
        }

        public enum DisplayMode
        {
            Combined,
            Dominant,
            LeftHand,
            RightHand
        }
    }
}
