using System.Collections.Generic;
using System.IO;
using System.Linq;
using MelonLoader;
using Newtonsoft.Json;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace SpectatorHUD;

// ReSharper disable once ClassNeverInstantiated.Global
public class HUDAssetContainer
{
    private readonly Config _config;
    private string? _loadedBundle;
    private HUDAsset? _hudAsset;

    public HUDAssetContainer(Config config)
    {
        _config = config;
        
        var bundlesEnum = GetHUDBundles();
        var bundles = bundlesEnum as string[] ?? bundlesEnum.ToArray();
        if (!bundles.Contains(config.SelectedHud))
            config.SelectedHud = bundles.FirstOrDefault() ?? string.Empty;
        config.Save();
    }

    public IEnumerable<string> GetHUDBundles()
    {
        return Directory.GetFiles(Config.HUDsDirectoryPath, "*.hud").Select(Path.GetFileName);
    }

    public AssetBundle LoadHUDBundle(string filename)
    {
        MelonLogger.Msg($"Loading {filename}...");
        
        using var stream = File.OpenRead(Path.Combine(Config.HUDsDirectoryPath, filename));
        using var memoryStream = new MemoryStream((int)stream.Length);
        
        stream.CopyTo(memoryStream);
        var bytes = memoryStream.ToArray();
        
        var assetBundle = AssetBundle.LoadFromMemory(bytes, 0);
        assetBundle!.hideFlags |= HideFlags.DontUnloadUnusedAsset;

        _loadedBundle = filename;
        
        MelonLogger.Msg($"Loaded {filename}.");
        return assetBundle;
    }

    public bool LoadHUDAsset(out HUDAsset? hudAsset)
    {
        hudAsset = null;
        
        if (_hudAsset == null || _loadedBundle != _config.SelectedHud)
        {
            var bundle = LoadHUDBundle(_config.SelectedHud);
            
            MelonLogger.Msg("Loading assets...");

            var assetNames = bundle.GetAllAssetNames();
            var manifestName = assetNames.First(name => name.EndsWith("manifest.json"));

            var manifestAsset = (TextAsset)bundle.LoadAsset(manifestName, Il2CppType.Of<TextAsset>());
            var manifest = JsonConvert.DeserializeObject<HUDManifest>(manifestAsset.ToString());

            if (manifest == null)
            {
                MelonLogger.Error(
                    $"Unable to load {_config.SelectedHud}: there was a problem when trying to parse the manifest.");
                return false;
            }
            
            MelonLogger.Msg($"Loaded {manifest.Name} v{manifest.Version} by {manifest.Name}");
            MelonLogger.Msg("Loading HUD asset...");

            var hudName = assetNames.First(name => name.EndsWith(manifest.PrefabAsset));
            var hudObject = (GameObject)bundle.LoadAsset(hudName, Il2CppType.Of<GameObject>());

            _hudAsset = new HUDAsset(manifest, hudObject);
            
            MelonLogger.Msg("Done.");
        }

        hudAsset = _hudAsset;
        return true;
    }
}