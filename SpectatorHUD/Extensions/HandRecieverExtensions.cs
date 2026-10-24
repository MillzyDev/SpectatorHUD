using Il2CppSLZ.Marrow;

namespace SpectatorHUD.Extensions;

public static class HandRecieverExtensions
{
    extension(HandReciever handReciever)
    {
        public bool TryFindGunFromReceiver(out Gun? gun)
        {
            gun = handReciever.GetComponentInParent<Gun>();
            if (gun == null) return false;

            return gun.triggerGrip == handReciever;
        }
    }
}