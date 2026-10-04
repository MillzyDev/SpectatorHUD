namespace SpectatorHUD
{
    public static class MetaInfo
    {
        public static int? HUDCompatibility;
        public static string? HUDName;
        public static string? HUDAuthor;
        public static int? HUDVersion;

        public static bool HasLeftReserveCounter;
        public static bool HasLeftAmmoCounter;
        public static bool HasRightReserveCounter;
        public static bool HasRightAmmoCounter;
        public static bool HasHealthCounter;
        public static bool HasMaxHealthCounter;
        public static bool HasHealthPercentageCounter;

        public static int ActiveWithGunInLeftHand;
        public static int ActiveWithGunInRightHand;

        public static int AnimationLeftGunHeld;
        public static int AnimationRightGunHeld;
        public static int AnimationLeftGunHeldChanged;
        public static int AnimationRightGunHeldChanged;

        public static int AnimationLeftAmmo;
        public static int AnimationRightAmmo;
        public static int AnimationLeftAmmoChanged;
        public static int AnimationRightAmmoChanged;

        public static int AnimationLeftReserve;
        public static int AnimationRightReserve;
        public static int AnimationLeftReserveChanged;
        public static int AnimationRightReserveChanged;

        public static int AnimationHealth;
        public static int AnimationMaxHealth;
        public static int AnimationHealthPercentage;
        public static int AnimationHealthChanged;
        public static int AnimationMaxHealthChanged;
    }
}