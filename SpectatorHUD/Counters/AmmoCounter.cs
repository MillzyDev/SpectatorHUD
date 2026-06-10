using Il2CppInterop.Runtime;
using Il2CppSLZ.Marrow;
using Il2CppTMPro;
using Il2CppUltEvents;
using UnityEngine;

namespace SpectatorHUD.Counters
{
    public class AmmoCounter : MonoBehaviour
    {
        public RigManager? rigManager;
        public Gun? heldGun;
        public UltEvent<float> onChange;
        
        private TMP_Text? _counterText;
        private int _lastObserved = 0;
        
        
        public AmmoCounter(IntPtr ptr) : base(ptr)
        {
        }

        public void Start()
        {
            this._counterText = this.GetComponent<TMP_Text>();
        }

        public void Update()
        {
            int ammoCount = this.heldGun?.AmmoCount() ?? 0;
            
            if (this._lastObserved == ammoCount)
            {
                return;
            }
            
            Logger.Debug("Ammo changed: {0} -> {1}", this._lastObserved, ammoCount);
            this._lastObserved = ammoCount;
            this._counterText?.SetText(ammoCount.ToString());
            //this.onChange.Invoke();
        }
    }
}