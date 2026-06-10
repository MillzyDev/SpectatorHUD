using Il2CppSLZ.Marrow;
using Il2CppTMPro;
using Il2CppUltEvents;
using UnityEngine;

namespace SpectatorHUD.Counters
{
    public class HealthCounter : MonoBehaviour
    {
        public RigManager? rigManager;
        public UltEvent<float> onChange;

        private TMP_Text? _counterText;
        private Health? _health;
        private float _lastObserved;

        public HealthCounter(IntPtr ptr) : base(ptr)
        {
        }

        public void Start()
        {
            this._counterText = this.GetComponent<TMP_Text>();
            this._health = this.rigManager?.health;
        }

        public void Update()
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (this._lastObserved == this._health?.curr_Health)
            {
                return;
            }

            this._lastObserved = this._health?.curr_Health ?? 0.0f;
            this._counterText?.SetText($"{this._lastObserved * 10f:0}");
            this.onChange.Invoke(this._lastObserved);
        }
    }
}