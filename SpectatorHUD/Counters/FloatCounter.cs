using System;
using MelonLoader;
using TMPro;
using UnityEngine;

namespace SpectatorHUD.Counters
{
    [RegisterTypeInIl2Cpp]
    public abstract class FloatCounter : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        protected TextMeshProUGUI? text;

        protected FloatCounter(IntPtr ptr) : base(ptr) { }

        public float Value { get; set; }

        public void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        public void Update()
        {
            UpdateCounter();
        }

        protected abstract void UpdateCounter();
    }
}
