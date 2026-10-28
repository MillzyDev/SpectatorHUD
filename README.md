![Static Badge](https://img.shields.io/badge/Source-SpectatorHUD-blue?style=for-the-badge&logo=github&link=https%3A%2F%2Fgithub.com%2FMillzyDev%2FSpectatorHUD%2Fblob%2Fmain%2FLICENSE.md)
![License](https://img.shields.io/badge/License-FSL--1.1--ALv2-green?style=for-the-badge&link=https%3A%2F%2Fgithub.com%2FMillzyDev%2FSpectatorHUD%2Fblob%2Fmain%2FLICENSE.md)
![Release](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fthunderstore.io%2Fapi%2Fexperimental%2Fpackage%2FMillzy%2FSpectatorHUD%2F&query=%24.latest.version_number&prefix=v&style=for-the-badge&logo=thunderstore&label=Release&color=teal)
![Downloads](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fthunderstore.io%2Fapi%2Fexperimental%2Fpackage%2FMillzy%2FSpectatorHUD%2F&query=%24.total_downloads&prefix=v&style=for-the-badge&logo=thunderstore&label=Downloads&color=teal)

# Spectator HUD
Spectator HUD is a BONELAB mod that loads custom HUDs to display on the spectator display.

**Responsive custom HUDs for your spectator camera to grace whoever is watching you play**

// GIF SHOWING HL2 HUD

**Animation and Shader integration! IT'S ALIVE!**

// DOOM HUD GIF

## Features
- Custom Unity package for creating custom HUDs
- 12 different TextMeshPro counters
- Enabling/disabling elements when holding/not holding items
- Animator support
  - 27 different animator inputs
  - Extensions to allow for shader control in animations
  - Pseudorandom variables
- Shader integration
  - 12 shader property inputs
  - Linear transformations on inputs
- BoneMenu for selecting installed HUDs
- Debug IMGUI for testing and iterating on your custom HUDs

## Switching HUDs
1. Open BoneMenu
2. Click "Millzy", should be purple
3. Click "Spectator HUD"
4. Click "Select HUD"
5. Find your desired HUD in the list, and click on it.
6. Go to the previous menu.
7. Click "Reload"

## Installing Custom HUDs
HUDs downloaded from Thunderstore, or exported from the Unity Editor should be placed in `<BONELAB FOLDER>/UserData/SpectatorHUD/HUDs/`.
Place the `.hud` file in there, and it will show up in the BoneMenu.

## Creating your own HUD
Instructions for using the **Spectator HUD SDK** to make your own custom HUD can be found on it's GitHub page, [here](https://github.com/MillzyDev/SpectatorHUD-SDK).

## Using the Debug Menu
**In order to use the debug menu, Debug Mode must be enabled for MelonLoader.**  It can be enabled by adding `--melonloader.debug` as a launch argument to the game.

Once in debug mode. Load into the game, in a level where the player spawns in. The debug menu can be opened by pressing `F8`.

The debug menu shows everything about the currently loaded HUD and what values Spectator HUD is currently tracking.
- The version of Spectator HUD you are using;
- The currently loaded HUD's name, version, and author(s);
- The compatibility version of the currently loaded HUD;
- What counters are present on the loaded HUD;
- How many animation and shader events of each category there are.

### Reload HUD Button
There is a button in the debug menu to reload the currently selected HUD from disk. This is useful for working on HUDs in the Unity editor; you can export the HUD, press the button, and Spectator HUD will reload it. No need to restart the game.

There is a stopwatch below the button showing the last time the HUD was reloaded.

## FAQ
### Does it work on Quest?
No.
### Are you adding Quest support?
No. It's not possible. The Quest version doesn't have a spectator view.

---

>     SpectatorHUD
>     Copyright (C) 2026  Millzy
>
>     This software is licensed under the terms of the Functional Source License,
>     Version 1.1, ALv2 Future License.  You are permitted to use, copy, modify,
>     create derivative works, publicly display, and redistribute it under the the
>     terms of the license.
>
>     THE SOFTWARE IS PROVIDED "AS IS" AND WITHOUT WARRANTIES OF ANY KIND, EXPRESS OR
>     IMPLIED, INCLUDING WITHOUT LIMITATION WARRANTIES OF FITNESS FOR A PARTICULAR
>     PURPOSE, MERCHANTABILITY, TITLE OR NON-INFRINGEMENT.