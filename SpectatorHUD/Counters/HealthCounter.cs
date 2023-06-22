using System;
using MelonLoader;
using TMPro;
using UnityEngine;

namespace SpectatorHUD.Counters
{
    [RegisterTypeInIl2Cpp]
    public class HealthCounter : MonoBehaviour
    {
        private TextMeshProUGUI _text = null!;
        
        public HealthCounter(IntPtr ptr) : base(ptr)
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
            Text.SetText($"{value}");
            
            Text.ForceMeshUpdate(true, true);
        }
    }
}
