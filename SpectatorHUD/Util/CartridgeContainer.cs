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

namespace SpectatorHUD.Util;

public class CartridgeContainer
{
    private AmmoInventory _ammoInventory;
    public readonly CartridgeData cartridge;
    public readonly string groupKey;
    public readonly AmmoCategory ammoCategory;

    public CartridgeContainer(CartridgeData cartridgeData)
    {
        this._ammoInventory = AmmoInventory.Instance;
        this.cartridge = cartridgeData;
        this.groupKey = this._ammoInventory.GetGroupByCartridge(cartridgeData);
        
        if (this.groupKey == this._ammoInventory.lightAmmoGroup.KeyName)
        {
            this.ammoCategory = AmmoCategory.Light;
        }
        else if (this.groupKey == this._ammoInventory.mediumAmmoGroup.KeyName)
        {
            this.ammoCategory = AmmoCategory.Medium;
        }
        else if (this.groupKey == this._ammoInventory.heavyAmmoGroup.KeyName)
        {
            this.ammoCategory = AmmoCategory.Heavy;
        }
        else
        {
            this.ammoCategory = AmmoCategory.Unknown;
        }
    }
}