using System;
using System.Reflection;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SpectatorHUD.Managers;

// ReSharper disable once ClassNeverInstantiated.Global
public class AssetManager<T> : IDisposable where T : Object
{
    private readonly string _bundleName;
    private readonly string _assetName;

    private AssetBundle? _bundle;
    private T? _asset;
    
    public AssetManager(string bundleName, string assetName)
    {
        _bundleName = bundleName;
        _assetName = assetName;
    }

    private AssetBundle LoadAssetBundle()
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_bundleName);
        using var memStream = new MemoryStream((int)stream!.Length);
        stream.CopyTo(memStream);

        var assetBundle = AssetBundle.LoadFromMemory(memStream.ToArray(), 0);
        assetBundle!.hideFlags |= HideFlags.DontUnloadUnusedAsset;
        return assetBundle;
    }

    private T LoadAsset()
    {
        var asset = Bundle.LoadAsset(_assetName);
        if (asset == null) return null!;
        asset.hideFlags = HideFlags.DontUnloadUnusedAsset;
        return asset.TryCast<T>();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public AssetBundle Bundle
    {
        get
        {
            _bundle ??= LoadAssetBundle();
            return _bundle;
        }
    }

    public T Asset
    {
        get
        {
            _asset ??= LoadAsset();
            return _asset;
        }
    }

    public void Dispose()
    {
        if (_bundle != null) _bundle.Unload(false);
    }
}