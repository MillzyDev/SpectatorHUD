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

using System.Diagnostics.Contracts;
using Il2CppSLZ.Marrow;
using SpectatorHUD.Util;

namespace SpectatorHUD;

public static class HudState
{
    public static CartridgeContainer? LeftDefaultCartridge { [Pure] get; set; }
    public static CartridgeContainer? RightDefaultCartridge { [Pure] get; set; }

    public static RigManager? RigManager { get; set; }
    public static AmmoInventory AmmoInventory
    {
        get
        {
            if (field != null)
            {
                return field;
            }

            field = AmmoInventory.Instance;
            LightAmmoKey = field.lightAmmoGroup.KeyName;
            MediumAmmoKey = field.mediumAmmoGroup.KeyName;
            HeavyAmmoKey = field.heavyAmmoGroup.KeyName;

            return field;
        }
    }
    public static string? LightAmmoKey { get; private set; }
    public static string? MediumAmmoKey { get; private set; }
    public static string? HeavyAmmoKey { get; private set; }

    public static event Action<Gun?>? OnLeftGunChanged;
    public static event Action<Gun?>? OnRightGunChanged;
    public static event Action<int?>? OnLeftAmmoChanged;
    public static event Action<int?>? OnRightAmmoChanged;
    public static event Action<int?>? OnLeftReserveChanged;
    public static event Action<int?>? OnRightReserveChanged;
    public static event Action<int?>? OnLeftTotalAmmoChanged;
    public static event Action<int?>? OnRightTotalAmmoChanged;
    public static event Action<int?>? OnTotalLightAmmoChanged;
    public static event Action<int?>? OnTotalMediumAmmoChanged;
    public static event Action<int?>? OnTotalHeavyAmmoChanged;
    public static event Action<float?>? OnHealthChanged;
    public static event Action<float?>? OnHealthPercentageChanged;
    public static event Action<float?>? OnMaxHealthChanged;

    public static void ForceNotifierRepoll()
    {
        OnLeftGunChanged?.Invoke(LeftGun);
        OnRightGunChanged?.Invoke(RightGun);
        OnLeftAmmoChanged?.Invoke(LeftAmmo);
        OnRightAmmoChanged?.Invoke(RightAmmo);
        OnLeftReserveChanged?.Invoke(LeftReserve);
        OnRightReserveChanged?.Invoke(RightReserve);
        OnLeftTotalAmmoChanged?.Invoke(LeftTotalAmmo);
        OnRightTotalAmmoChanged?.Invoke(RightTotalAmmo);
        OnTotalLightAmmoChanged?.Invoke(TotalLightAmmo);
        OnTotalMediumAmmoChanged?.Invoke(TotalMediumAmmo);
        OnTotalHeavyAmmoChanged?.Invoke(TotalHeavyAmmo);
        OnHealthChanged?.Invoke(Health);
        OnHealthPercentageChanged?.Invoke(HealthPercentage);
        OnMaxHealthChanged?.Invoke(MaxHealth);
    }

    #region Notifier Properties

    public static Gun? LeftGun
    {
        get;
        set
        {
            if (field != value)
            {
                OnLeftGunChanged?.Invoke(value);
            }

            field = value;
        }
    }

    public static Gun? RightGun
    {
        get;
        set
        {
            if (field != value)
            {
                OnRightGunChanged?.Invoke(value);
            }

            field = value;
        }
    }

    public static int? LeftAmmo
    {
        get;
        set
        {
            if (field != value)
            {
                OnLeftAmmoChanged?.Invoke(value);
            }

            field = value;
        }
    }

    public static int? RightAmmo
    {
        get;
        set
        {
            if (field != value)
            {
                OnRightAmmoChanged?.Invoke(value);
            }

            field = value;
        }
    }

    public static int? LeftReserve
    {
        get;
        set
        {
            if (field != value)
            {
                OnLeftReserveChanged?.Invoke(value);
            }

            field = value;
        }
    }

    public static int? RightReserve
    {
        get;
        set
        {
            if (field != value)
            {
                OnRightReserveChanged?.Invoke(value);
            }

            field = value;
        }
    }

    public static int? LeftTotalAmmo
    {
        get;
        set
        {
            if (field != value)
            {
                OnLeftTotalAmmoChanged?.Invoke(value);
            }

            field = value;
        }
    }

    public static int? RightTotalAmmo
    {
        get;
        set
        {
            if (field != value)
            {
                OnRightTotalAmmoChanged?.Invoke(value);
            }

            field = value;
        }
    }

    public static int? TotalLightAmmo
    {
        get;
        set
        {
            if (field != value)
            {
                OnTotalLightAmmoChanged?.Invoke(value);
            }

            field = value;
        }
    }

    public static int? TotalMediumAmmo
    {
        get;
        set
        {
            if (field != value)
            {
                OnTotalMediumAmmoChanged?.Invoke(value);
            }

            field = value;
        }
    }

    public static int? TotalHeavyAmmo
    {
        get;
        set
        {
            if (field != value)
            {
                OnTotalHeavyAmmoChanged?.Invoke(value);
            }

            field = value;
        }
    }

    public static float? Health
    {
        get;
        set
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (field != value)
            {
                OnHealthChanged?.Invoke(value);
            }

            field = value;
        }
    }

    public static float? HealthPercentage
    {
        get;
        set
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (field != value)
            {
                OnHealthPercentageChanged?.Invoke(value);
            }

            field = value;
        }
    }

    public static float? MaxHealth
    {
        get;
        set
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (field != value)
            {
                OnMaxHealthChanged?.Invoke(value);
            }

            field = value;
        }
    }

    #endregion // Notifier Properties
}