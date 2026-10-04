/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This program is free software: you can redistribute it and/or modify
 *      it under the terms of the GNU Lesser General Public License as published by
 *      the Free Software Foundation, either version 3 of the License, or
 *      (at your option) any later version.
 *
 *      This program is distributed in the hope that it will be useful,
 *      but WITHOUT ANY WARRANTY; without even the implied warranty of
 *      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *      GNU General Public License for more details.
 *
 *      You should have received a copy of the GNU Lesser General Public License
 *      along with this program.  If not, see https://www.gnu.org/licenses/.
 */

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