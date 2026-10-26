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
using Il2CppSLZ.Marrow.Data;

namespace SpectatorHUD.Util;

public class CartridgeContainer
{
    private readonly AmmoInventory _ammoInventory;
    public readonly AmmoCategory ammoCategory;
    public readonly CartridgeData cartridge;
    public readonly string groupKey;

    public CartridgeContainer(CartridgeData cartridgeData)
    {
        this._ammoInventory = HudState.AmmoInventory;
        this.cartridge = cartridgeData;
        this.groupKey = this._ammoInventory.GetGroupByCartridge(cartridgeData);

        this.ammoCategory = AmmoCategoryUtil.GetCategoryFromGroupKey(this.groupKey);
    }
}