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
using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Data;
using SpectatorHUD.Extensions;
using SpectatorHUD.Observers;
using SpectatorHUD.Util;

namespace SpectatorHUD;

public static class HudState
{
    public static CartridgeContainer? LeftDefaultCartridge { get; set; }
    public static CartridgeContainer? RightDefaultCartridge { get; set; }

    public static RigManager? RigManager { get; private set; }
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

    /// <summary>
    ///     Configures members for player load and invokes all callbacks to trigger initial values.
    ///     Should be called after the HUD has been loaded.
    /// </summary>
    /// <param name="rigManager">Target player's RigManager</param>
    public static void OpenState(RigManager rigManager)
    {
        RigManager = rigManager;

        RigManager.gameObject.AddComponent<PlayerHealthObserver>();

        ForceUpdateCounts();

        OnLeftGunChanged?.Invoke(null);
        OnRightGunChanged?.Invoke(null);
    }

    public static void ForceUpdateCounts()
    {
        UpdateLeftAmmoCount();
        UpdateRightAmmoCount();
        UpdateLeftReserveCount();
        UpdateRightReserveCount();
        UpdateHealth();
        UpdateMaxHealth();
    }

    public static void OnGachaMagazineInserted()
    {
        Logger.Debug("HudState: OnGachaMagazineInserted called");
        UpdateLeftReserveCount();
        UpdateRightReserveCount();
    }
    

    private static void UpdateLeftAmmoCount()
    {
        Logger.Debug("HudState: Left ammo updated");
        OnLeftAmmoChanged?.Invoke(LeftGun?.GetAmmoCount());
    }

    private static void UpdateRightAmmoCount()
    {
        Logger.Debug("HudState: Right ammo updated");
        OnRightAmmoChanged?.Invoke(RightGun?.GetAmmoCount());
    }

    private static void UpdateLeftReserveCount()
    {
        if (LeftDefaultCartridge == null)
        {
            LeftReserve = null;
            return;
        }

        int? newReserve = AmmoInventory.Instance.GetCartridgeCount(LeftDefaultCartridge.cartridge) - LeftAmmo;
        LeftReserve = newReserve;
    }

    private static void UpdateRightReserveCount()
    {
        if (RightDefaultCartridge == null)
        {
            RightReserve = null;
            return;
        }

        int? newReserve = AmmoInventory.Instance.GetCartridgeCount(RightDefaultCartridge.cartridge) - RightAmmo;
        RightReserve = newReserve;
    }

    private static void UpdateHealth()
    {
        Logger.Debug("HudState: Health updated");
        Health = RigManager?.health.curr_Health;
    }

    private static void UpdateMaxHealth()
    {
        Logger.Debug("HudState: Right reserve updated");
        MaxHealth = RigManager?.health.max_Health;
    }

    #region Notifier Properties

    public static Gun? LeftGun
    {
        get;
        set
        {
            if (field != value) OnLeftGunChanged?.Invoke(value);
            field = value;
        }
    }

    public static Gun? RightGun
    {
        get;
        set
        {
            if (field != value) OnRightGunChanged?.Invoke(value);
            field = value;
        }
    }

    public static int? LeftAmmo
    {
        get;
        set
        {
            if (field != value) OnLeftAmmoChanged?.Invoke(value);
            field = value;
        }
    }

    public static int? RightAmmo
    {
        get;
        set
        {
            if (field != value) OnRightAmmoChanged?.Invoke(value);
            field = value;
        }
    }

    public static int? LeftReserve
    {
        get;
        set
        {
            if (field != value) OnLeftReserveChanged?.Invoke(value);
            field = value;
        }
    }

    public static int? RightReserve
    {
        get;
        set
        {
            if (field != value) OnRightReserveChanged?.Invoke(value);
            field = value;
        }
    }

    public static int? LeftTotalAmmo
    {
        get;
        set
        {
            if (field != value) OnLeftTotalAmmoChanged?.Invoke(value);
            field = value;
        }
    }

    public static int? RightTotalAmmo
    {
        get;
        set
        {
            if (field != value) OnRightTotalAmmoChanged?.Invoke(value);
            field = value;
        }
    }

    public static int? TotalLightAmmo
    {
        get;
        set
        {
            if (field != value) OnTotalLightAmmoChanged?.Invoke(value);
            field = value;
        }
    }

    public static int? TotalMediumAmmo
    {
        get;
        set
        {
            if (field != value) OnTotalMediumAmmoChanged?.Invoke(value);
            field = value;
        }
    }

    public static int? TotalHeavyAmmo
    {
        get;
        set
        {
            if (field != value) OnTotalHeavyAmmoChanged?.Invoke(value);
            field = value;
        }
    }

    public static float? Health
    {
        get;
        set
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (field != value) OnHealthChanged?.Invoke(value);
            field = value;
        }
    }

    public static float? HealthPercentage
    {
        get;
        set
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (field != value) OnHealthPercentageChanged?.Invoke(value);
            field = value;
        }
    }

    public static float? MaxHealth
    {
        get;
        set
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (field != value) OnMaxHealthChanged?.Invoke(value);
            field = value;
        }
    }

    #endregion // Notifier Properties
}