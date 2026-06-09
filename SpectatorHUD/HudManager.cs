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

using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Attributes;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSLZ.Marrow;
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

        public GameObject? hud = null;
        public RigManager? rigManager = null;

        public HudManager(IntPtr ptr) : base(ptr)
        {
        }

        public void Start()
        {
            
        }

        [HideFromIl2Cpp]
        private Gun? GetGun(Hand hand)
        {
            if (hand == null)
            {
                return null;
            }

            GameObject heldObject = hand.m_CurrentAttachedGO;
            if (heldObject == null)
            {
                return null;
            }

            var gun = this.GetComponentInParent<Gun>();
            return gun == null ? this.GetComponentInChildren<Gun>() : gun;
        }
    }
}