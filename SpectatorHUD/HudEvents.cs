using Il2CppSLZ.Marrow;

namespace SpectatorHUD
{
    public static class HudEvents
    {
        public delegate void HealthChangedHandler(float value, float previous);

        public delegate void AmmoChangedHandler(int value, int previous);

        public delegate void HeldItemChangedHandler(bool objectHeld, Gun? gunHeld, bool isTriggerGrip);

        public static event HealthChangedHandler? HealthChanged;
        public static event AmmoChangedHandler? LeftAmmoChanged;
        public static event AmmoChangedHandler? RightAmmoChanged;
        public static event HeldItemChangedHandler? LeftHeldItemChanged;
        public static event HeldItemChangedHandler? RightHeldItemChanged;

        public static void OnHealthChanged(float value, float previous) => HealthChanged?.Invoke(value, previous);
        public static void OnLeftAmmoChanged(int value, int previous) => LeftAmmoChanged?.Invoke(value, previous);
        public static void OnRightAmmoChanged(int value, int previous) => RightAmmoChanged?.Invoke(value, previous);

        public static void OnLeftHeldItemChanged(bool objectHeld, Gun? gunHeld, bool isTriggerGrip) =>
            LeftHeldItemChanged?.Invoke(objectHeld, gunHeld, isTriggerGrip);
        
        public static void OnRightHeldItemChanged(bool objectHeld, Gun? gunHeld, bool isTriggerGrip) =>
            RightHeldItemChanged?.Invoke(objectHeld, gunHeld, isTriggerGrip);

    }
}