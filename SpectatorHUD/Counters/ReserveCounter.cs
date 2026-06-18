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
using Il2CppSLZ.Marrow.Data;
using Il2CppTMPro;
using Il2CppUltEvents;
using UnityEngine;

namespace SpectatorHUD.Counters
{
    public class ReserveCounter : MonoBehaviour
    {
        [NonSerialized] public RigManager? rigManager;
        [NonSerialized] public Gun? heldGun;
        [NonSerialized] public UltEvent<float>? onChange;
        
        private TMP_Text? _counterText;
        private int _lastObserved = -1;
        
        public ReserveCounter(IntPtr ptr) : base(ptr)
        {
        }

        public void Start()
        {
            this._counterText = this.GetComponent<TMP_Text>();
        }

        public void Update()
        {
            CartridgeData? cartridgeData = this.heldGun?.defaultCartridge;
            int cartridgeCount = this.heldGun?._ammoInventory.GetCartridgeCount(cartridgeData) ?? 0;

            if (cartridgeCount == this._lastObserved)
            {
                return;
            }
            
            int reserve = cartridgeCount - (this.heldGun?.AmmoCount() ?? 0);
            
            Logger.Debug("Reserve changed: {0} -> {1}", this._lastObserved, reserve);
            this._lastObserved = reserve;
            this._counterText?.SetText(reserve.ToString());
            // this.onChange?.Invoke(); TODO
        }
    }
}