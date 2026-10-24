using Il2CppSLZ.Marrow;

namespace SpectatorHUD.Extensions;

public static class GunExtensions
{
    extension (Gun gun)
    {
        public int GetAmmoCount()
        {
            if (gun._magState != null)
            {
                return gun.chamberedCartridge != null ? gun._magState.AmmoCount + 1 : gun._magState.AmmoCount;
            }

            // Chambered cartridge is not included in the MagState's count
            return gun.chamberedCartridge != null ? 1 : 0;
        }
    }
}