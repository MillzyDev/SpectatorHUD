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