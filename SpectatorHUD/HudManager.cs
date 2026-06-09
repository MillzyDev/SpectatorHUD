using Il2CppTMPro;
using Il2CppUltEvents;
using UnityEngine;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace SpectatorHUD
{
    public class HudManager : MonoBehaviour
    {
        public TMP_Text leftHandReserveCounter;
        public TMP_Text leftHandAmmoCounter;
        public TMP_Text rightHandReserveCounter;
        public TMP_Text rightHandAmmoCounter;
        public TMP_Text healthCounter;
        public TMP_Text maxHealthCounter;

        public UltEvent<float> leftHandReserveCounterChanged;
        public UltEvent<float> leftHandAmmoCounterChanged;
        public UltEvent<float> rightHandReserveCounterChanged;
        public UltEvent<float> rightHandAmmoCounterChanged;
        public UltEvent<float> healthCounterChanged;
        public UltEvent<float> maxHealthCounterChanged;

        public HudManager(IntPtr ptr) : base(ptr)
        {
        }
    }
}