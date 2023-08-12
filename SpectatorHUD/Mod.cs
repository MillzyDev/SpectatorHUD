using BoneLib;
using FieldInjector;
using MelonLoader;
using SLZ.Rig;
using SpectatorHUD.Counters;

namespace SpectatorHUD
{
    public class Mod : MelonMod
    {
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

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            RigManager rigManager = Player.rigManager;

            if (!rigManager)
                return;

            // Player exists and hud should load
            HudManager.Instance.LoadHud(rigManager);
        }
    }
}
