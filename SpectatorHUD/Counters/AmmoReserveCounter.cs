using System;
using UnityEngine;

namespace SpectatorHUD.Counters
{
    public class AmmoReserveCounter : MonoBehaviour
    {
        public int value;
        public Reserve reserve = Reserve.Medium;

        public bool useCustomFormatting;
        public string format = "{0}";

        public AmmoReserveCounter(IntPtr ptr) : base(ptr)
        {
        }
        
        public enum Reserve
        {
            Small,
            Medium,
            Heavy
        }
    }
}
