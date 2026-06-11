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

            Logger.Debug("Health changed: {0} -> {1}", this._lastObserved, this._health?.curr_Health ?? 0);
            this._lastObserved = this._health?.curr_Health ?? 0.0f;
            this._counterText?.SetText($"{this._lastObserved * 10f:0}");
            this.onChange.Invoke(this._lastObserved);
        }
    }
}