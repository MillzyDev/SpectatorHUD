using System;
using UnityEngine;

namespace SpectatorHUD.Counters
{
    public class HealthCounter : MonoBehaviour
    {
        public float value;
        public float multiplier = 10f;
        public int decimalPrecision;

        public bool useCustomFormatting;
        public string format = "{0.f}";

        public HealthCounter(IntPtr ptr) : base(ptr)
        {
        }
    }
}
