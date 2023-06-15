using System;
using MelonLoader;
using TMPro;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace SpectatorHUD.Counters
{
    [RegisterTypeInIl2Cpp]
    public abstract class FloatCounter : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        protected TextMeshProUGUI? text;

        public FloatCounter(IntPtr ptr) : base(ptr)
        {
        }

        public float Value { get; set; }

        public void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        public void Update()
        {
            UpdateCounter();
        }

        [HideFromIl2Cpp]
        protected abstract void UpdateCounter();
    }
}
