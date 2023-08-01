using FieldInjector;
using MelonLoader;
using SpectatorHUD.Counters;

namespace SpectatorHUD
{
    public class Mod : MelonMod
    {
        public override void OnInitializeMelon()
        {
        }

        public override void OnLateInitializeMelon()
        {
            SerialisationHandler.Inject(typeof(HudManifestSO));
            SerialisationHandler.Inject(typeof(HudConfigSO));
            SerialisationHandler.Inject<Hud>();
            SerialisationHandler.Inject<AmmoCounter>();
            SerialisationHandler.Inject<AmmoReserveCounter>();
            SerialisationHandler.Inject<CurrentAmmoReserveCounter>();
            SerialisationHandler.Inject<HealthCounter>();
        }

        public override void OnApplicationQuit()
        {
        }
    }
}
