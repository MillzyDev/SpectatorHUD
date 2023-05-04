using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using MelonLoader;
using UnityEngine;

namespace SpectatorHUD.Managers;

public class AssetManager : IDisposable
{
    private readonly HashSet<AssetBundle> _unloadLater = new();

    public AssetManager()
    {
    }

    public AssetBundle LoadAssetBundleFromMemory(byte[] memory, bool unloadLater = false)
    {
        MelonLogger.Msg(memory.Length);
        var assetBundle = AssetBundle.LoadFromMemory(memory, 0);
        assetBundle!.hideFlags |= HideFlags.DontUnloadUnusedAsset;
        if (unloadLater) _unloadLater.Add(assetBundle);
        return assetBundle;
    }

    public AssetBundle LoadAssetBundleFromStream(Stream stream, bool unloadLater = false)
    {
        using var memoryStream = new MemoryStream((int)stream!.Length);
        stream.CopyTo(memoryStream);
        var memory = memoryStream.ToArray();
        var assetBundle = LoadAssetBundleFromMemory(memory, unloadLater);
        return assetBundle;
    }

    public AssetBundle LoadEmbeddedAssetBundle(Assembly assembly, string path, bool unloadLater = false)
    {
        using var stream = assembly.GetManifestResourceStream(path);
        MelonLogger.Msg(stream == null);
        var assetBundle = LoadAssetBundleFromStream(stream, unloadLater);
        return assetBundle;
    }

    internal AssetBundle LoadInternalAssetBundle(string path, bool unloadLater = false) =>
        LoadEmbeddedAssetBundle(Assembly.GetExecutingAssembly(), path, unloadLater);

    public void Dispose()
    {
        foreach (var assetBundle in _unloadLater)
        {
            assetBundle.Unload(false);
        }
    }
}