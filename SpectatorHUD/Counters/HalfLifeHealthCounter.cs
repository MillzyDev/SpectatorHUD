using System;
using MelonLoader;
using TMPro;
using UnityEngine;

namespace SpectatorHUD.Counters
{
    [RegisterTypeInIl2Cpp]
    public class HalfLifeHealthCounter : MonoBehaviour
    {
        private TextMeshProUGUI _text = null!;

        public HalfLifeHealthCounter(IntPtr ptr) : base(ptr)
        {
        }

        public TextMeshProUGUI Text
        {
            get
            {
                if (!_text)
                    _text = GetComponent<TextMeshProUGUI>();
                return _text;
            }
        }

        public void UpdateCounter(float value)
        {
            float modifiedValue = value * 10;
            MelonLogger.Msg($"Modified value {value} to {modifiedValue:0.0f}");
            Text!.SetText($"{modifiedValue:0.}");
            Text.ForceMeshUpdate(true, true);
        }
    }
}
