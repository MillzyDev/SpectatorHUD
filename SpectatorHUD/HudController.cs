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
using SpectatorHUD.Expressions;
using UnityEngine;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace SpectatorHUD;

public class HudController : MonoBehaviour
{
    public TMP_Text leftHandReserveCounter;
    public TMP_Text leftHandAmmoCounter;
    public TMP_Text rightHandReserveCounter;
    public TMP_Text rightHandAmmoCounter;
    public TMP_Text leftHandTotalAmmoCounter;
    public TMP_Text rightHandTotalAmmoCounter;

    public TMP_Text totalLightAmmoCounter;
    public TMP_Text totalMediumAmmoCounter;
    public TMP_Text totalHeavyAmmoCounter;

    public TMP_Text healthCounter;
    public TMP_Text maxHealthCounter;
    public TMP_Text percentageHealthCounter;

    public LinearParameters healthModifier;

    public List<GameObject> activeWithGunHeldInLeftHand;
    public List<GameObject> activeWithGunHeldInRightHand;

    private LinearExpression<float> _healthModifierExpression;

    public HudController(IntPtr ptr) : base(ptr)
    {
    }

    private void Awake()
    {
        this._healthModifierExpression =
            new LinearExpression<float>(this.healthModifier.multiplier, this.healthModifier.offset);
    }

    private void Start()
    {
        MetaInfo.HasLeftReserveCounter = this.leftHandReserveCounter != null;
        MetaInfo.HasLeftAmmoCounter = this.leftHandAmmoCounter != null;
        MetaInfo.HasRightReserveCounter = this.rightHandReserveCounter != null;
        MetaInfo.HasRightAmmoCounter = this.rightHandAmmoCounter != null;
        MetaInfo.HasHealthCounter = this.healthCounter != null;
        MetaInfo.HasMaxHealthCounter = this.maxHealthCounter != null;
        MetaInfo.HasHealthPercentageCounter = this.percentageHealthCounter != null;

        MetaInfo.ActiveWithGunInLeftHand = this.activeWithGunHeldInLeftHand.Count;
        MetaInfo.ActiveWithGunInRightHand = this.activeWithGunHeldInRightHand.Count;
    }

    private void OnEnable()
    {
        HudState.OnLeftGunChanged += this.OnLeftGunChanged;
        HudState.OnRightGunChanged += this.OnRightGunChanged;

        if (this.leftHandAmmoCounter != null) HudState.OnLeftAmmoChanged += this.UpdateLeftAmmoCounter;

        if (this.rightHandAmmoCounter != null) HudState.OnRightAmmoChanged += this.UpdateRightAmmoCounter;

        if (this.leftHandReserveCounter != null) HudState.OnLeftReserveChanged += this.UpdateLeftReserveCounter;

        if (this.rightHandReserveCounter != null) HudState.OnRightReserveChanged += this.UpdateRightReserveCounter;

        if (this.healthCounter != null) HudState.OnHealthChanged += this.UpdateHealthCounter;

        if (this.percentageHealthCounter) HudState.OnHealthPercentageChanged += this.UpdateHealthPercentageCounter;

        if (this.maxHealthCounter) HudState.OnMaxHealthChanged += this.UpdateMaxHealthCounter;

        // Animation events
        HudState.ForceUpdateCounts();
    }

    private void OnDisable()
    {
        HudState.OnLeftGunChanged -= this.OnLeftGunChanged;
        HudState.OnRightGunChanged -= this.OnRightGunChanged;

        // Null checks not needed here; no errors if callbacks not present
        HudState.OnLeftAmmoChanged -= this.UpdateLeftAmmoCounter;
        HudState.OnRightAmmoChanged -= this.UpdateRightAmmoCounter;
        HudState.OnLeftReserveChanged -= this.UpdateLeftReserveCounter;
        HudState.OnRightReserveChanged -= this.UpdateRightReserveCounter;
        HudState.OnHealthChanged -= this.UpdateHealthCounter;
        HudState.OnHealthPercentageChanged -= this.UpdateHealthPercentageCounter;
    }

    private void UpdateLeftAmmoCounter(int? ammo)
    {
        this.leftHandAmmoCounter.text = ammo.ToString();
    }

    private void UpdateRightAmmoCounter(int? ammo)
    {
        this.rightHandAmmoCounter.text = ammo.ToString();
    }

    private void UpdateLeftReserveCounter(int? ammo)
    {
        this.leftHandReserveCounter.text = ammo.ToString();
    }

    private void UpdateRightReserveCounter(int? ammo)
    {
        this.rightHandReserveCounter.text = ammo.ToString();
    }

    private void UpdateHealthCounter(float? health)
    {
        this.healthCounter.text = $"{this._healthModifierExpression.Evaluate(health ?? 0f):0.}";
    }

    private void UpdateHealthPercentageCounter(float? percentage)
    {
        this.percentageHealthCounter.text = $"{percentage * 100:0.}";
    }

    private void UpdateMaxHealthCounter(float? maxHealth)
    {
        this.maxHealthCounter.text = $"{this._healthModifierExpression.Evaluate(maxHealth ?? 0f):0.}";
    }

    private void OnLeftGunChanged(Gun? gun)
    {
        Logger.Debug("HudManagerV1: Left gun changed");

        if (gun == null) // no gun in hand
        {
            foreach (GameObject go in this.activeWithGunHeldInLeftHand)
            {
                go.active = false;
                Logger.Debug("HudManagerV1: disabled " + go.name);
            }

            return;
        }

        foreach (GameObject go in this.activeWithGunHeldInLeftHand)
        {
            go.active = true;
            Logger.Debug("HudManagerV1: enabled " + go.name);
        }
    }

    private void OnRightGunChanged(Gun? gun)
    {
        Logger.Debug("HudManagerV1: Right gun changed");

        if (gun == null) // no gun in hand
        {
            foreach (GameObject go in this.activeWithGunHeldInRightHand)
            {
                Logger.Debug("HudManagerV1: disabled " + go.name);
                go.active = false;
            }

            return;
        }

        foreach (GameObject go in this.activeWithGunHeldInRightHand)
        {
            Logger.Debug("HudManagerV1: enabled " + go.name);
            go.active = true;
        }
    }
}