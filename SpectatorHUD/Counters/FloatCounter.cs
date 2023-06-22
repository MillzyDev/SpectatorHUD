using System;
using MelonLoader;
using TMPro;
using UnhollowerBaseLib.Attributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpectatorHUD.Counters
{
    [RegisterTypeInIl2Cpp]
    public class FloatCounter : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        protected TextMeshProUGUI? text;

        public FloatCounter(IntPtr ptr) : base(ptr)
        {
        }

        public void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        public virtual void UpdateCounter(float value)
        {
            
        }
    }
}
