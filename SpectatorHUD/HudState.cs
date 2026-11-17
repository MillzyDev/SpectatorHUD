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

using System;
using Il2CppSLZ.Marrow;
using SpectatorHUD.Util;

namespace SpectatorHUD;

public sealed class HudState
{
    private static readonly Lazy<HudState> LazyInitialiser = new(() => new HudState());
    public static HudState Instance => LazyInitialiser.Value;
    
    public CartridgeContainer? LeftDefaultCartridge { get; set; }
    public CartridgeContainer? RightDefaultCartridge { get; set; }

    public RigManager? RigManager { get; set; }
    public AmmoInventory AmmoInventory
    {
        get
        {
            if (field != null)
            {
                return field;
            }

            field = AmmoInventory.Instance;
            this.LightAmmoKey = field.lightAmmoGroup.KeyName;
            this.MediumAmmoKey = field.mediumAmmoGroup.KeyName;
            this.HeavyAmmoKey = field.heavyAmmoGroup.KeyName;

            return field;
        }
    }
    public string? LightAmmoKey { get; private set; }
    public string? MediumAmmoKey { get; private set; }
    public string? HeavyAmmoKey { get; private set; }

    public event Action<Gun?>? OnLeftGunChanged;
    public event Action<Gun?>? OnRightGunChanged;
    public event Action<int?>? OnLeftAmmoChanged;
    public event Action<int?>? OnRightAmmoChanged;
    public event Action<int?>? OnLeftReserveChanged;
    public event Action<int?>? OnRightReserveChanged;
    public event Action<int?>? OnLeftTotalAmmoChanged;
    public event Action<int?>? OnRightTotalAmmoChanged;
    public event Action<int?>? OnTotalLightAmmoChanged;
    public event Action<int?>? OnTotalMediumAmmoChanged;
    public event Action<int?>? OnTotalHeavyAmmoChanged;
    public event Action<float?>? OnHealthChanged;
    public event Action<float?>? OnHealthPercentageChanged;
    public event Action<float?>? OnMaxHealthChanged;

    private HudState()
    {
    }

    public void ForceNotifierRepoll()
    {
        this.OnLeftGunChanged?.Invoke(this.LeftGun);
        this.OnRightGunChanged?.Invoke(this.RightGun);
        this.OnLeftAmmoChanged?.Invoke(this.LeftAmmo);
        this.OnRightAmmoChanged?.Invoke(this.RightAmmo);
        this.OnLeftReserveChanged?.Invoke(this.LeftReserve);
        this.OnRightReserveChanged?.Invoke(this.RightReserve);
        this.OnLeftTotalAmmoChanged?.Invoke(this.LeftTotalAmmo);
        this.OnRightTotalAmmoChanged?.Invoke(this.RightTotalAmmo);
        this.OnTotalLightAmmoChanged?.Invoke(this.TotalLightAmmo);
        this.OnTotalMediumAmmoChanged?.Invoke(this.TotalMediumAmmo);
        this.OnTotalHeavyAmmoChanged?.Invoke(this.TotalHeavyAmmo);
        this.OnHealthChanged?.Invoke(this.Health);
        this.OnHealthPercentageChanged?.Invoke(this.HealthPercentage);
        this.OnMaxHealthChanged?.Invoke(this.MaxHealth);
    }

    #region Notifier Properties

    public Gun? LeftGun
    {
        get;
        set
        {
            if (field != value)
            {
                this.OnLeftGunChanged?.Invoke(value);
                //Logger.Debug(nameof(HudState), nameof(LeftGun) + " changed " + field + " -> " + value);
            }

            field = value;
        }
    }

    public Gun? RightGun
    {
        get;
        set
        {
            if (field != value)
            {
                this.OnRightGunChanged?.Invoke(value);
                //Logger.Debug(nameof(HudState), nameof(RightGun) + " changed " + field + " -> " + value);

            }

            field = value;
        }
    }

    public int? LeftAmmo
    {
        get;
        set
        {
            if (field != value)
            {
                this.OnLeftAmmoChanged?.Invoke(value);
                //Logger.Debug(nameof(HudState), nameof(LeftAmmo) + " changed " + field + " -> " + value);

            }

            field = value;
        }
    }

    public int? RightAmmo
    {
        get;
        set
        {
            if (field != value)
            {
                this.OnRightAmmoChanged?.Invoke(value);
                //Logger.Debug(nameof(HudState), nameof(RightAmmo) + " changed " + field + " -> " + value);

            }

            field = value;
        }
    }

    public int? LeftReserve
    {
        get;
        set
        {
            if (field != value)
            {
                this.OnLeftReserveChanged?.Invoke(value);
                //Logger.Debug(nameof(HudState), nameof(LeftReserve) + " changed " + field + " -> " + value);

            }

            field = value;
        }
    }

    public int? RightReserve
    {
        get;
        set
        {
            if (field != value)
            {
                this.OnRightReserveChanged?.Invoke(value);
                //Logger.Debug(nameof(HudState), nameof(RightReserve) + " changed " + field + " -> " + value);
            }

            field = value;
        }
    }

    public int? LeftTotalAmmo
    {
        get;
        set
        {
            if (field != value)
            {
                this.OnLeftTotalAmmoChanged?.Invoke(value);
                //Logger.Debug(nameof(HudState), nameof(LeftTotalAmmo) + " changed " + field + " -> " + value);
            }

            field = value;
        }
    }

    public int? RightTotalAmmo
    {
        get;
        set
        {
            if (field != value)
            {
                this.OnRightTotalAmmoChanged?.Invoke(value);
                //Logger.Debug(nameof(HudState), nameof(RightTotalAmmo) + " changed " + field + " -> " + value);
            }

            field = value;
        }
    }

    public int? TotalLightAmmo
    {
        get;
        set
        {
            if (field != value)
            {
                this.OnTotalLightAmmoChanged?.Invoke(value);
                //Logger.Debug(nameof(HudState), nameof(TotalLightAmmo) + " changed " + field + " -> " + value);
            }

            field = value;
        }
    }

    public int? TotalMediumAmmo
    {
        get;
        set
        {
            if (field != value)
            {
                this.OnTotalMediumAmmoChanged?.Invoke(value);
                //Logger.Debug(nameof(HudState), nameof(TotalMediumAmmo) + " changed " + field + " -> " + value);
            }

            field = value;
        }
    }

    public int? TotalHeavyAmmo
    {
        get;
        set
        {
            if (field != value)
            {
                this.OnTotalHeavyAmmoChanged?.Invoke(value);
                //Logger.Debug(nameof(HudState), nameof(TotalHeavyAmmo) + " changed " + field + " -> " + value);
            }

            field = value;
        }
    }

    public float? Health
    {
        get;
        set
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (field != value)
            {
                this.OnHealthChanged?.Invoke(value);
                //Logger.Debug(nameof(HudState), nameof(Health) + " changed " + field + " -> " + value);
            }

            field = value;
        }
    }

    public float? HealthPercentage
    {
        get;
        set
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (field != value)
            {
                this.OnHealthPercentageChanged?.Invoke(value);
                //Logger.Debug(nameof(HudState), nameof(HealthPercentage) + " changed " + field + " -> " + value);
            }

            field = value;
        }
    }

    public float? MaxHealth
    {
        get;
        set
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (field != value)
            {
                this.OnMaxHealthChanged?.Invoke(value);
                //Logger.Debug(nameof(HudState), nameof(MaxHealth) + " changed " + field + " -> " + value);
            }

            field = value;
        }
    }

    #endregion // Notifier Properties
}