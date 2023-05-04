using Ninject;
using UnityEngine;

namespace SpectatorHUD.Managers;

public class HUDManager : IInitializable
{
    private readonly AssetManager _assetManager;
    private static string s_hudPath = "SpectatorHUD.Resources.alyxhud.hud";
    private static string s_hudName = "AlyxHUD";
    
    [Inject]
    public HUDManager(AssetManager assetManager)
    {
        _assetManager = assetManager;
    }
    
    public void Initialize()
    {
        var hudAssetBundle = _assetManager.LoadInternalAssetBundle(s_hudPath);
        var hudPrefab = hudAssetBundle.LoadAsset(s_hudName).TryCast<GameObject>();
        hudPrefab.hideFlags |= HideFlags.DontUnloadUnusedAsset;
        _ = Object.Instantiate(hudPrefab);
        hudAssetBundle.Unload(false);
    }
}