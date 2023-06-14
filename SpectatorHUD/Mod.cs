using Boneject.MelonLoader;
using Boneject.MelonLoader.Attributes;
using Boneject.Ninject;
using SpectatorHUD.Modules;

namespace SpectatorHUD
{
    public class Mod : InjectableMelonMod
    {
        private Config _config = null!;

        [OnInitialize]
        // ReSharper disable once UnusedMember.Global
        public void OnInitialize(Bonejector bonejector)
        {
            _config = Config.Load();
            bonejector.Load<SHAppModule>(Context.App, _config);
            bonejector.Load<SHPlayerModule>(Context.Player);
        }

        public override void OnApplicationQuit()
        {
            _config.Save();
        }
    }
}
