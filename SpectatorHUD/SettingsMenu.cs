/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This software is licensed under the terms of the Functional Source License,
 *      Version 1.1, ALv2 Future License.  You are permitted to use, copy, modify,
 *      create derivative works, publicly display, and redistribute it under the the
 *      terms of the license.
 *
 *      THE SOFTWARE IS PROVIDED "AS IS" AND WITHOUT WARRANTIES OF ANY KIND, EXPRESS OR
 *      IMPLIED, INCLUDING WITHOUT LIMITATION WARRANTIES OF FITNESS FOR A PARTICULAR
 *      PURPOSE, MERCHANTABILITY, TITLE OR NON-INFRINGEMENT.
 *
 *      You should have received a copy of the Functional Source License, Version 1.1,
 *      ALv2 Future License along with this program.  If not, see https://fsl.software/.
 */

using System.Reflection;
using BoneLib.BoneMenu;
using BoneLib.Notifications;
using Il2CppSLZ.Marrow.SceneStreaming;
using MelonLoader.Utils;
using UnityEngine;

namespace SpectatorHUD;

public static class SettingsMenu
{
    private static List<Page> hudSelection;
    private static Color MyColour => new(0.404f, 0.306f, 0.635f);

    public static void Register()
    {
        Page myCategory = Page.Root.CreatePage("Millzy", MyColour);
        Page mainCategory = myCategory.CreatePage("Spectator HUD", Color.yellow);
        Page selectHudPage = mainCategory.CreatePage("Select HUD", Color.white);
        FillSelectHudPage(selectHudPage);

        mainCategory.CreateFunction("Reload", Color.yellow, SceneStreamer.Reload);
    }

    private static void FillSelectHudPage(Page parent)
    {
        List<Tuple<HudV1, string?>> hudMetadatas = new();

        // Get metadata for the default hud.
        HudV1? defaultHud = GetMetadataFromEmbeddedResource(HudBootstrap.DefaultHud);
        if (defaultHud != null)
        {
            hudMetadatas.Add(new Tuple<HudV1, string?>(defaultHud, null));
        }

        // Collect the metadata for all files and add it to the list of all metadata.
        string path = Path.Join(MelonEnvironment.UserDataDirectory, "SpectatorHUD", "HUDs");
        string[] hudFiles = Directory.GetFiles(path, "*.hud", SearchOption.TopDirectoryOnly);
        IEnumerable<Tuple<HudV1, string?>> fileMetadatas = hudFiles
            .Select(f => new Tuple<HudV1?, string?>(GetMetadataFromFile(f), f))
            .Where(m => m.Item1 != null)!;
        hudMetadatas.AddRange(fileMetadatas);

        foreach ((HudV1 hud, string? hudPath) in hudMetadatas)
        {
            parent.CreateFunction(
                $"{hud.hudName} <color=#828282>v{hud.hudVersion}</color>",
                Color.white,
                () =>
                {
                    Config.Instance.Hud = hudPath;
                    Notifier.Send(new Notification
                    {
                        Message = new NotificationText(
                            $"HUD set to {hud.hudName} v{hud.hudVersion} by {hud.hudAuthor}. Reload to Apply."),
                        Title = new NotificationText("Spectator HUD", Color.yellow),
                        Type = NotificationType.Success
                    });
                }
            );
        }
    }

    private static HudV1? GetMetadataFromFile(string path)
    {
        AssetBundle assetBundle = AssetBundle.LoadFromFile(path);
        if (assetBundle == null)
        {
            Logger.Error("Failed to load asset bundle from " + path);
            return null;
        }

        return GetMetadataFromAssetBundle(assetBundle);
    }

    private static HudV1? GetMetadataFromEmbeddedResource(string resource)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream(resource);
        if (stream == null)
        {
            Logger.Error("Unable to open stream to embedded resource " + resource + " not found.");
            return null;
        }

        MemoryStream memoryStream = new();
        stream.CopyTo(memoryStream);

        AssetBundle assetBundle = AssetBundle.LoadFromMemory(memoryStream.ToArray());
        return GetMetadataFromAssetBundle(assetBundle);
    }

    private static HudV1? GetMetadataFromAssetBundle(AssetBundle assetBundle)
    {
        // TODO: Load version first and check that
        HudV1? hudInfo = assetBundle.LoadAsset("Hud.asset").TryCast<HudV1>();
        if (hudInfo == null)
        {
            Logger.Error("Hud.asset could not be loaded from assetBundle: " + assetBundle.name);
            return null;
        }

        assetBundle.Unload(false);

        switch (hudInfo.spectatorHudVersion.version)
        {
            case 0:
            case 1:
                return hudInfo;
            default:
                Logger.Error(
                    $"{hudInfo.hudName} v{hudInfo.hudVersion} by {hudInfo.hudAuthor} requires SDK v{hudInfo.spectatorHudVersion.version} features. " +
                    "This version of SpectatorHUD supports up to v1"
                );
                return null;
        }
    }
}