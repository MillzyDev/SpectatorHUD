using Il2CppSLZ.Marrow;
using UnityEngine;

namespace SpectatorHUD.Watchers
{
    public sealed class PlayerHealthWatcher : MonoBehaviour
    {
        private RigManager? _rigManager;
        private Health? _health;

        private float? _lastHealth;
        
        public PlayerHealthWatcher(IntPtr ptr) : base(ptr)
        {
        }

        public void Start()
        {
            this._rigManager = this.GetComponent<RigManager>();
            this._health = this._rigManager.health;
        }

        public void Update()
        {
            float? currentHealth = this._health?.curr_Health;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (this._lastHealth == currentHealth)
            {
                return;
            }

            this._lastHealth = currentHealth;
            HudState.OnHealthUpdated(currentHealth);
        }
    }
}