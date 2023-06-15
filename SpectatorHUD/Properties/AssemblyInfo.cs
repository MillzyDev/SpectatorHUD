using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using MelonLoader;
using SpectatorHUD;

[assembly: AssemblyTitle("SpectatorHUD")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(null!)]
[assembly: AssemblyProduct("SpectatorHUD")]
[assembly: AssemblyCopyright("Copyright (c) 2023 Frederick (Millzy) Mills")]
[assembly: AssemblyTrademark(null)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion("0.1.0.0")]
[assembly: AssemblyFileVersion("0.1.0")]
[assembly: NeutralResourcesLanguage("en")]

[assembly:
    MelonInfo(typeof(Mod), "SpectatorHUD", "0.1.0", "Millzy",
        "https://github.com/MillzyDev/SpectatorHUD/releases/latest/download/SpectatorHUD.zip")]

[assembly: MelonPriority(-1000000)]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonID("dev.millzy.SpectatorHUD")]

[assembly: VerifyLoaderVersion("0.5.7")]

[assembly: HarmonyDontPatchAll]
