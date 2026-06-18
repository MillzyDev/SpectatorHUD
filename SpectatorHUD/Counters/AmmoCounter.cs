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
    public class AmmoCounter : MonoBehaviour
    {
        public RigManager? rigManager;
        public Gun? heldGun;
        public UltEvent<float> onChange;
        
        private TMP_Text? _counterText;
        private int _lastObserved = -1;
        
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
            //this.onChange.Invoke(); TODO
        }
    }
}