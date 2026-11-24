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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BoneLib.BoneMenu;
using BoneLib.Notifications;
using Il2CppSLZ.Marrow.SceneStreaming;
using MelonLoader.Utils;
using SpectatorHUD.Addons;
using SpectatorHUD.Logging;
using SpectatorHUD.Manifest;
using UnityEngine;
using Logger = SpectatorHUD.Logging.Logger;

namespace SpectatorHUD.UI;

internal static class SettingsMenu
{
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
        Logger logger = LogService.Logger;
        List<Tuple<IHudManifest, string?>> hudManifests = new();

        // Get metadata for the default hud.
        IHudManifest? defaultHud = GetManifestFromEmbeddedResource(HudBootstrap.DefaultHud);
        if (defaultHud != null)
        {
            hudManifests.Add(new Tuple<IHudManifest, string?>(defaultHud, null));
        }

        // Collect the metadata for all files and add it to the list of all metadata.
        string path = Path.Join(MelonEnvironment.UserDataDirectory, "SpectatorHUD", "HUDs");
        string[] hudFiles = Directory.GetFiles(path, "*.hud", SearchOption.TopDirectoryOnly);
        IEnumerable<Tuple<IHudManifest, string?>> fileManifests = hudFiles
            .Select(f => new Tuple<IHudManifest?, string?>(GetManifestFromFile(f), f))
            .Where(m => m.Item1 != null)!;
        hudManifests.AddRange(fileManifests);

        var addonRegistry = AddonRegistry.Instance;
        
        foreach ((IHudManifest hud, string? hudPath) in hudManifests)
        {
            List<Addon> missingAddons = hud.GetRequiredAddons()
                .Where(a => !addonRegistry.HasAddonCompatibleWith(a.name, a.version))
                .ToList();
            
            // Make selection text orange if any required addons are missing
            bool hasAllAddons = !missingAddons.Any();
            string hudNameColour = hasAllAddons ? "#FFFFFF" : "#FF8200";
            
            FunctionElement function = parent.CreateFunction(
                $"<color={hudNameColour}>{hud.GetHudName()}</color> <color=#828282>v{hud.GetHudVersion()}</color>",
                Color.white,
                () =>
                {
                    Config.Instance.Hud = hudPath;
                    Notifier.Send(new Notification
                    {
                        Message = new NotificationText(
                            $"HUD set to {hud.GetHudName()} v{hud.GetHudVersion()} by {hud.GetHudAuthor()}. Reload to Apply."),
                        Title = new NotificationText("Spectator HUD", Color.yellow),
                        Type = NotificationType.Success
                    });
                }
            );

            // Set helpful tooltip if any required addons are missing
            if (!hasAllAddons)
            {
                string missingAddonNames = string.Join(", ", missingAddons.Select(a => $"{a.name} v{a.version}"));
                function.ElementTooltip = $"Requires: {missingAddonNames}";
            }
            
            logger.Debug("Created element for: {0} v{1} by {2}", hud.GetHudName(), hud.GetHudVersion(), hud.GetHudAuthor());
        }
    }

    private static IHudManifest? GetManifestFromFile(string path)
    {
        Logger logger = LogService.Logger;
        
        AssetBundle assetBundle = AssetBundle.LoadFromFile(path);
        if (assetBundle == null)
        {
            logger.Error("Failed to load asset bundle from file: " + path);
            return null;
        }

        IHudManifest? manifest = GetManifestFromAssetBundle(assetBundle);
        assetBundle.Unload(false);
        return manifest;
    }

    private static IHudManifest? GetManifestFromEmbeddedResource(string resource)
    {
        Logger logger = LogService.Logger;
        
        Assembly assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream(resource);
        if (stream == null)
        {
            logger.Error("Unable to open stream to embedded resource " + resource + " not found.");
            return null;
        }

        MemoryStream memoryStream = new();
        stream.CopyTo(memoryStream);

        AssetBundle assetBundle = AssetBundle.LoadFromMemory(memoryStream.ToArray());
        if (assetBundle == null)
        {
            logger.Error("Failed to load asset bundle from embedded resource: " + resource);
            return null;
        }
        
        IHudManifest? hud = GetManifestFromAssetBundle(assetBundle);
        assetBundle.Unload(false);
        return hud;
    }

    private static IHudManifest? GetManifestFromAssetBundle(AssetBundle assetBundle)
    {
        Logger logger = LogService.Logger;
        
        var versionAsset = assetBundle.LoadAsset("Hud.Version.asset").TryCast<HudVersion>();
        if (versionAsset == null)
        {
            logger.Error("Hud.Version.asset could not be loaded from asset bundle: " + assetBundle.name);
            return null;
        }

        IHudManifest? hudInfo = null;

        switch (versionAsset.version)
        {
            case 0:
            case 1:
                hudInfo = GetManifestV1FromAssetBundle(assetBundle);
                break;
            case 2:
                hudInfo = GetManifestV2FromAssetBundle(assetBundle);
                break;
            default:
                logger.Error("Attempting to load a HUD of version {0} is unsupported.", versionAsset.version);
                break;
        }
        
        if (hudInfo == null)
        {
            logger.Error("Unable to find a supported HUD Manifest. Ensure the asset is included, or that this mod is updated.");
            return null;
        }
        
        logger.Info("Successfully read v{0} HUD manifest", versionAsset.version);
        logger.Info("Metadata: {0}\tv{1}\tAuthor: {2}", hudInfo.GetHudName(), hudInfo.GetHudVersion(), hudInfo.GetHudAuthor());
        return hudInfo;
    }

    private static IHudManifest? GetManifestV1FromAssetBundle(AssetBundle assetBundle)
    {
        var hudInfo = assetBundle.LoadAsset("Hud.asset").TryCast<HudV1>();
        return hudInfo?.AsManifest();
    }

    private static IHudManifest? GetManifestV2FromAssetBundle(AssetBundle assetBundle)
    {
        var hudInfo = assetBundle.LoadAsset("Hud.asset").TryCast<HudV2>();
        return hudInfo?.AsManifest();
    }
}