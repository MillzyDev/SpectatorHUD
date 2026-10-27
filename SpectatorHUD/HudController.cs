/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This software is licensed under the terms of the Functional Source License,
 *      Version 1.1, ALv2 Future License.  You are permitted to use, copy, modify,
 *      create derivative works, publicly display, and redistribute it under the the
 *      terms of the license.
 *
 *      THE SOFTWARE IS PROVIDED "AS IS" AND WITHOUT WARRANTIES OF ANY KIND, EXPRESS OR
 *      IMPLIED, INCLUDING WITHOUT LIMITATION WARRANTIES OF FITNESS FOR A PARTICULAR
 *      PURPOSE, MERCHANTABILITY, TITLE OR NON-INFRINGEMENT.
 *
 *      You should have received a copy of the Functional Source License, Version 1.1,
 *      ALv2 Future License along with this program.  If not, see https://fsl.software/.
 */

using Il2CppSLZ.Marrow;
using Il2CppTMPro;
using SpectatorHUD.Expressions;
using UnityEngine;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace SpectatorHUD;

public class HudController(IntPtr ptr) : MonoBehaviour(ptr)
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

    private void Awake()
    {
        this._healthModifierExpression =
            new LinearExpression<float>(this.healthModifier.multiplier, this.healthModifier.offset);
    }

    private void Start()
    {
        MetaInfo.HasLeftReserveCounter = this.leftHandReserveCounter != null;
        MetaInfo.HasLeftAmmoCounter = this.leftHandAmmoCounter != null;
        MetaInfo.HasTotalLeftAmmoCounter = this.leftHandTotalAmmoCounter != null;
        MetaInfo.HasRightReserveCounter = this.rightHandReserveCounter != null;
        MetaInfo.HasRightAmmoCounter = this.rightHandAmmoCounter != null;
        MetaInfo.HasTotalRightAmmoCounter = this.rightHandTotalAmmoCounter != null;
        MetaInfo.HasTotalLightAmmoCounter = this.totalLightAmmoCounter != null;
        MetaInfo.HasTotalMediumAmmoCounter = this.totalMediumAmmoCounter != null;
        MetaInfo.HasTotalHeavyAmmoCounter = this.totalHeavyAmmoCounter != null;
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

        if (this.leftHandAmmoCounter != null)
        {
            HudState.OnLeftAmmoChanged += this.UpdateLeftAmmoCounter;
        }

        if (this.rightHandAmmoCounter != null)
        {
            HudState.OnRightAmmoChanged += this.UpdateRightAmmoCounter;
        }

        if (this.leftHandReserveCounter != null)
        {
            HudState.OnLeftReserveChanged += this.UpdateLeftReserveCounter;
        }

        if (this.rightHandReserveCounter != null)
        {
            HudState.OnRightReserveChanged += this.UpdateRightReserveCounter;
        }

        if (this.leftHandTotalAmmoCounter != null)
        {
            HudState.OnLeftTotalAmmoChanged += this.UpdateLeftTotalAmmoCounter;
        }

        if (this.rightHandTotalAmmoCounter != null)
        {
            HudState.OnRightTotalAmmoChanged += this.UpdateRightTotalAmmoCounter;
        }

        if (this.totalLightAmmoCounter != null)
        {
            HudState.OnTotalLightAmmoChanged += this.UpdateTotalLightAmmoCounter;
        }

        if (this.totalMediumAmmoCounter != null)
        {
            HudState.OnTotalMediumAmmoChanged += this.UpdateTotalMediumAmmoCounter;
        }

        if (this.totalHeavyAmmoCounter != null)
        {
            HudState.OnTotalHeavyAmmoChanged += this.UpdateTotalHeavyAmmoCounter;
        }

        if (this.healthCounter != null)
        {
            HudState.OnHealthChanged += this.UpdateHealthCounter;
        }

        if (this.percentageHealthCounter)
        {
            HudState.OnHealthPercentageChanged += this.UpdateHealthPercentageCounter;
        }

        if (this.maxHealthCounter)
        {
            HudState.OnMaxHealthChanged += this.UpdateMaxHealthCounter;
        }

        HudState.ForceNotifierRepoll();
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
        HudState.OnLeftTotalAmmoChanged -= this.UpdateLeftTotalAmmoCounter;
        HudState.OnRightTotalAmmoChanged -= this.UpdateRightTotalAmmoCounter;
        HudState.OnTotalLightAmmoChanged -= this.UpdateTotalLightAmmoCounter;
        HudState.OnTotalMediumAmmoChanged -= this.UpdateTotalMediumAmmoCounter;
        HudState.OnTotalHeavyAmmoChanged -= this.UpdateTotalHeavyAmmoCounter;
        HudState.OnHealthChanged -= this.UpdateHealthCounter;
        HudState.OnHealthPercentageChanged -= this.UpdateHealthPercentageCounter;
        HudState.OnMaxHealthChanged -= this.UpdateMaxHealthCounter;
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

    private void UpdateLeftTotalAmmoCounter(int? ammo)
    {
        this.leftHandTotalAmmoCounter.text = ammo.ToString();
    }

    private void UpdateRightTotalAmmoCounter(int? ammo)
    {
        this.rightHandTotalAmmoCounter.text = ammo.ToString();
    }

    private void UpdateTotalLightAmmoCounter(int? ammo)
    {
        this.totalLightAmmoCounter.text = ammo.ToString();
    }

    private void UpdateTotalMediumAmmoCounter(int? ammo)
    {
        this.totalMediumAmmoCounter.text = ammo.ToString();
    }

    private void UpdateTotalHeavyAmmoCounter(int? ammo)
    {
        this.totalHeavyAmmoCounter.text = ammo.ToString();
    }

    private void UpdateHealthCounter(float? health)
    {
        this.healthCounter.text = $"{this._healthModifierExpression.Evaluate(health ?? 0f):0.}";
    }

    private void UpdateHealthPercentageCounter(float? percentage)
    {
        float? clamped = percentage == null ? null : Math.Max(percentage.Value, 0f);
        this.percentageHealthCounter.text = $"{clamped * 100:0.}";
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