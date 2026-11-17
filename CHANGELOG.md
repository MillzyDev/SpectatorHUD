# v2.0.0
- HUD v2 Support
  - HUDs can specify "required addons" which are intended to be used to signify features used by other mods other than SpectatorHUD
- Extension API
  - Other mods can integrate with SpectatorHUD to register as an "addon", i.e. adding extra functionality intended to be used in HUDs
  - Debug IMGUI integration support
- Miscellaneous API changes:
  - `HudState` is now a lazily initialised singleton instead of a static class
  - Complete refactor and reimplementation of logging system
  - Access specifiers of some classes are now stricter
  - `GUILayoutManager` has been moved to the `SpectatorHUD.UI` namespace.
  - `SettingsMenu` has been moved to the `SpectatorHUD.UI` namespace.
  - `DebugGUI` has been moved to the `SpectatorHUD.UI` namespace

# v1.0.1
- Fixed a bug where the embedded Half-Life 2 HUD would only be able to load once
- Added extra error and debug logs

# v1.0.0
- Initial Release