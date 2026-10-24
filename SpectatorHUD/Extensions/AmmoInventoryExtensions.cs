using Il2CppSLZ.Marrow;

namespace SpectatorHUD.Extensions;

public static class AmmoInventoryExtensions
{
    extension(AmmoInventory ammoInventory)
    {
        public int GetTotalAmmoForGun(Gun gun)
        {
            return ammoInventory.GetCartridgeCount(gun.defaultCartridge);
        }
        
        public int GetReserveForGun(Gun gun)
        {
            return ammoInventory.GetTotalAmmoForGun(gun) - gun.GetAmmoCount();
        }
    }
}