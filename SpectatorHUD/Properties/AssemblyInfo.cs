using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using MelonLoader;
using SpectatorHUD;
using BuildInfo = SpectatorHUD.BuildInfo;

[assembly: AssemblyTitle(BuildInfo.name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(BuildInfo.company)]
[assembly: AssemblyProduct(BuildInfo.name)]
[assembly: AssemblyCopyright("""Copyright (c) 2023 Frederick ("Millzy") Mills""")]
[assembly: AssemblyTrademark(null)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion(BuildInfo.version)]
[assembly: AssemblyFileVersion(BuildInfo.version)]
[assembly: NeutralResourcesLanguage("en")]

[assembly: MelonInfo(typeof(Mod), BuildInfo.name, BuildInfo.semanticVersion, BuildInfo.author, BuildInfo.downloadLink)]

[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonID(BuildInfo.id)]

[assembly: VerifyLoaderVersion("0.5.7")]