using Boneject.MelonLoader;
using Boneject.MelonLoader.Attributes;
using Boneject.Ninject;

namespace SpectatorHUD;

public static class BuildInfo
{
    public const string id = "dev.millzy.SpectatorHUD";
    public const string name = "SpectatorHUD";
    public const string author = "Millzy";
    public const string company = null!;
    public const string version = "0.1.0.0";
    public const string semanticVersion = "0.1.0";
    public const string downloadLink = "https://github.com/MillzyDev/SpectatorHUD/releases/latest/download/SpectatorHUD.zip";
}

public class Mod : InjectableMelonMod
{
    [OnInitialize]
    // ReSharper disable once UnusedMember.Global
    public void OnInitializeMod(Bonejector bonejector)
    {
        
    }
}