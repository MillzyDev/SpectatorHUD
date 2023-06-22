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

        private TextMeshProUGUI Text
        {
            get
            {
                if (!_text)
                    _text = GetComponent<TextMeshProUGUI>();
                return _text;
            }
        }

        public void HealthUpdated(float value)
        {
            float modifiedValue = value * 10;
            Text.SetText($"{modifiedValue:0.}");
            
            Text.ForceMeshUpdate(true, true);
        }
    }
}
