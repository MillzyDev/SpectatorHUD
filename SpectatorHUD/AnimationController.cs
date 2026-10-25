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
using SpectatorHUD.Animation;
using UnityEngine;

namespace SpectatorHUD;

public class AnimationController : MonoBehaviour
{
    public List<AnimationBoolReference> animationLeftGunHeld;
    public List<AnimationBoolReference> animationRightGunHeld;
    public List<AnimationTriggerReference> animationLeftGunHeldChanged;
    public List<AnimationTriggerReference> animationRightGunHeldChanged;

    public List<AnimationIntReference> animationLeftAmmo;
    public List<AnimationIntReference> animationRightAmmo;
    public List<AnimationTriggerReference> animationLeftAmmoChanged;
    public List<AnimationTriggerReference> animationRightAmmoChanged;

    public List<AnimationIntReference> animationLeftReserve;
    public List<AnimationIntReference> animationRightReserve;
    public List<AnimationTriggerReference> animationLeftReserveChanged;
    public List<AnimationTriggerReference> animationRightReserveChanged;

    public List<AnimationFloatReference> animationHealth;
    public List<AnimationFloatReference> animationMaxHealth;
    public List<AnimationFloatReference> animationHealthPercentage;
    public List<AnimationTriggerReference> animationHealthChanged;
    public List<AnimationTriggerReference> animationMaxHealthChanged;

    public AnimationController(IntPtr ptr) : base(ptr)
    {
    }

    private void Start()
    {
        MetaInfo.AnimationLeftGunHeld = this.animationLeftGunHeld.Count;
        MetaInfo.AnimationRightGunHeld = this.animationRightGunHeld.Count;
        MetaInfo.AnimationLeftGunHeldChanged = this.animationLeftGunHeldChanged.Count;
        MetaInfo.AnimationRightGunHeldChanged = this.animationRightGunHeldChanged.Count;

        MetaInfo.AnimationLeftAmmo = this.animationLeftAmmo.Count;
        MetaInfo.AnimationRightAmmo = this.animationRightAmmo.Count;
        MetaInfo.AnimationLeftAmmoChanged = this.animationLeftAmmoChanged.Count;
        MetaInfo.AnimationRightAmmoChanged = this.animationRightAmmoChanged.Count;

        MetaInfo.AnimationLeftReserve = this.animationLeftReserve.Count;
        MetaInfo.AnimationRightReserve = this.animationRightReserve.Count;
        MetaInfo.AnimationLeftReserveChanged = this.animationLeftReserveChanged.Count;
        MetaInfo.AnimationRightReserveChanged = this.animationRightReserveChanged.Count;

        MetaInfo.AnimationHealth = this.animationHealth.Count;
        MetaInfo.AnimationMaxHealth = this.animationMaxHealth.Count;
        MetaInfo.AnimationHealthPercentage = this.animationHealthPercentage.Count;
        MetaInfo.AnimationHealthChanged = this.animationHealthChanged.Count;
        MetaInfo.AnimationMaxHealthChanged = this.animationMaxHealthChanged.Count;

        this.ResolveAnimationParameterIds(this.animationLeftGunHeld);
        this.ResolveAnimationParameterIds(this.animationRightGunHeld);
        this.ResolveAnimationParameterIds(this.animationLeftGunHeldChanged);
        this.ResolveAnimationParameterIds(this.animationRightGunHeldChanged);
        this.ResolveAnimationParameterIds(this.animationLeftAmmo);
        this.ResolveAnimationParameterIds(this.animationRightAmmo);
        this.ResolveAnimationParameterIds(this.animationLeftAmmoChanged);
        this.ResolveAnimationParameterIds(this.animationRightAmmoChanged);
        this.ResolveAnimationParameterIds(this.animationLeftReserve);
        this.ResolveAnimationParameterIds(this.animationRightReserve);
        this.ResolveAnimationParameterIds(this.animationLeftReserveChanged);
        this.ResolveAnimationParameterIds(this.animationRightReserveChanged);
        this.ResolveAnimationParameterIds(this.animationHealth);
        this.ResolveAnimationParameterIds(this.animationMaxHealth);
        this.ResolveAnimationParameterIds(this.animationHealthPercentage);
        this.ResolveAnimationParameterIds(this.animationHealthChanged);
        this.ResolveAnimationParameterIds(this.animationMaxHealthChanged);
    }

    private void OnEnable()
    {
        HudState.OnLeftAmmoChanged += this.UpdateLeftAmmoAnimation;
        HudState.OnRightAmmoChanged += this.UpdateRightAmmoAnimation;
        HudState.OnLeftReserveChanged += this.UpdateLeftReserveAnimation;
        HudState.OnRightReserveChanged += this.UpdateRightReserveAnimation;
        HudState.OnHealthChanged += this.UpdateHealthAnimation;
        HudState.OnHealthPercentageChanged += this.UpdateHealthPercentageAnimation;
        HudState.OnMaxHealthChanged += this.UpdateMaxHealthAnimation;

        HudState.OnLeftGunChanged += this.OnLeftGunChanged;
        HudState.OnRightGunChanged += this.OnRightGunChanged;
    }

    private void OnDisable()
    {
        HudState.OnLeftAmmoChanged -= this.UpdateLeftAmmoAnimation;
        HudState.OnRightAmmoChanged -= this.UpdateRightAmmoAnimation;
        HudState.OnLeftReserveChanged -= this.UpdateLeftReserveAnimation;
        HudState.OnRightReserveChanged -= this.UpdateRightReserveAnimation;
        HudState.OnHealthChanged -= this.UpdateHealthAnimation;
        HudState.OnHealthPercentageChanged -= this.UpdateHealthPercentageAnimation;
        HudState.OnMaxHealthChanged -= this.UpdateMaxHealthAnimation;

        HudState.OnLeftGunChanged -= this.OnLeftGunChanged;
        HudState.OnRightGunChanged -= this.OnRightGunChanged;
    }

    private void ResolveAnimationParameterIds(List<AnimationBoolReference> parameters)
    {
        for (var i = 0; i < parameters.Count; i++)
        {
            AnimationBoolReference param = parameters[i];
            param.parameterId = Animator.StringToHash(param.parameterName);
            parameters[i] = param;
        }
    }

    private void ResolveAnimationParameterIds(List<AnimationFloatReference> parameters)
    {
        for (var i = 0; i < parameters.Count; i++)
        {
            AnimationFloatReference param = parameters[i];
            param.parameterId = Animator.StringToHash(param.parameterName);
            parameters[i] = param;
        }
    }

    private void ResolveAnimationParameterIds(List<AnimationIntReference> parameters)
    {
        for (var i = 0; i < parameters.Count; i++)
        {
            AnimationIntReference param = parameters[i];
            param.parameterId = Animator.StringToHash(param.parameterName);
            parameters[i] = param;
        }
    }

    private void ResolveAnimationParameterIds(List<AnimationTriggerReference> parameters)
    {
        for (var i = 0; i < parameters.Count; i++)
        {
            AnimationTriggerReference param = parameters[i];
            param.parameterId = Animator.StringToHash(param.parameterName);
            parameters[i] = param;
        }
    }

    private void UpdateLeftAmmoAnimation(int? ammo)
    {
        this.SetAnimationInts(this.animationLeftAmmo, ammo ?? 0);
        this.FireAnimationTriggers(this.animationLeftAmmoChanged);
    }

    private void UpdateRightAmmoAnimation(int? ammo)
    {
        this.SetAnimationInts(this.animationRightAmmo, ammo ?? 0);
        this.FireAnimationTriggers(this.animationRightAmmoChanged);
    }

    private void UpdateLeftReserveAnimation(int? ammo)
    {
        this.SetAnimationInts(this.animationLeftReserve, ammo ?? 0);
        this.FireAnimationTriggers(this.animationLeftReserveChanged);
    }

    private void UpdateRightReserveAnimation(int? ammo)
    {
        this.SetAnimationInts(this.animationRightReserve, ammo ?? 0);
        this.FireAnimationTriggers(this.animationRightReserveChanged);
    }

    private void UpdateHealthAnimation(float? health)
    {
        this.SetAnimationFloats(this.animationHealth, health ?? 0f);
        this.FireAnimationTriggers(this.animationHealthChanged);
    }

    private void UpdateHealthPercentageAnimation(float? percentage)
    {
        this.SetAnimationFloats(this.animationHealthPercentage, percentage ?? 0);
    }

    private void UpdateMaxHealthAnimation(float? maxHealth)
    {
        this.SetAnimationFloats(this.animationMaxHealth, maxHealth ?? 0f);
        this.FireAnimationTriggers(this.animationMaxHealthChanged);
    }

    private void OnLeftGunChanged(Gun? gun)
    {
        this.FireAnimationTriggers(this.animationLeftGunHeldChanged);

        if (gun == null)
        {
            this.SetAnimationBools(this.animationLeftGunHeld, false);
            return;
        }

        this.SetAnimationBools(this.animationLeftGunHeld, true);
    }

    private void OnRightGunChanged(Gun? gun)
    {
        this.FireAnimationTriggers(this.animationRightGunHeldChanged);

        if (gun == null)
        {
            this.SetAnimationBools(this.animationRightGunHeld, false);
            return;
        }

        this.SetAnimationBools(this.animationRightGunHeld, true);
    }

    private void FireAnimationTriggers(List<AnimationTriggerReference> triggers)
    {
        foreach (AnimationTriggerReference trigger in triggers)
        {
            trigger.animator.ResetTrigger(trigger.parameterId);
            trigger.animator.SetTrigger(trigger.parameterId);
        }
    }

    private void SetAnimationBools(List<AnimationBoolReference> bools, bool value)
    {
        foreach (AnimationBoolReference @bool in bools)
        {
            @bool.animator.SetBool(@bool.parameterId, value);
        }
    }

    private void SetAnimationFloats(List<AnimationFloatReference> floats, float value)
    {
        foreach (AnimationFloatReference @float in floats)
        {
            @float.animator.SetFloat(@float.parameterId, value);
        }
    }

    private void SetAnimationInts(List<AnimationIntReference> ints, int value)
    {
        foreach (AnimationIntReference @int in ints)
        {
            @int.animator.SetInteger(@int.parameterId, value);
        }
    }
}