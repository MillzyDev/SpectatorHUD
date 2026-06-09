using MelonLoader;
using SpectatorHUD;
using SpectatorHUD.HarmonyPatches;
using BuildInfo = SpectatorHUD.BuildInfo;

[assembly: MelonInfo(typeof(Mod), BuildInfo.Name, BuildInfo.Version, BuildInfo.Author)]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonID(BuildInfo.Id)]
[assembly: HarmonyDontPatchAll]

namespace SpectatorHUD
{
    public sealed class Mod : MelonMod
    {
        public override void OnInitializeMelon()
        {
            Logger.SetInstance(this.LoggerInstance);
            this.InstallPatch(typeof(RigManager_Start));
        }

        private void InstallPatch(Type patchType)
        {
            this.HarmonyInstance.PatchAll(patchType);
        }
    }
}