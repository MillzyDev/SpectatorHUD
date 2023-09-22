using System;
using System.IO;
using MelonLoader;

namespace SpectatorHUD.Configuration
{
    public class Config
    {
        private static readonly Lazy<Config> s_lazy = new(() => new Config());
        
        private readonly MelonPreferences_Category _config;
        private readonly MelonPreferences_Category _hudOverride;
        private readonly MelonPreferences_Entry<string> _selectedHudEntry;
        private readonly MelonPreferences_Entry<bool> _hudEnabledEntry;
        private readonly MelonPreferences_Entry<bool> _overrideCombinedAmmoCounterEntry;
        private readonly MelonPreferences_Entry<ushort> _combinedAmmoCounterEntry;
        private readonly MelonPreferences_Entry<bool> _overrideHealthCounterDisplayModeEntry;
        private readonly MelonPreferences_Entry<ushort> _healthCounterDisplayModeEntry;

        private Config()
        {
            _config = MelonPreferences.CreateCategory("Config");
            _selectedHudEntry = _config.CreateEntry<string>("sSelectedHud", "Half-Life HUD.hud");
            _hudEnabledEntry = _config.CreateEntry("bHudEnabled", true);
            
            string configPath = Path.Combine(MelonUtils.UserDataDirectory, "config", "config.cfg");
            _config.SetFilePath(configPath);

            _hudOverride = MelonPreferences.CreateCategory("Overrides");
            _overrideCombinedAmmoCounterEntry = _hudOverride.CreateEntry("bOverrideCombinedAmmoCounter", false);
            _combinedAmmoCounterEntry = _hudOverride.CreateEntry<ushort>("uCombinedAmmoCounter", 2);
            _overrideHealthCounterDisplayModeEntry = _hudOverride.CreateEntry("bOverrideHealthCounterDisplayMode", false);
            _healthCounterDisplayModeEntry = _hudOverride.CreateEntry<ushort>("uHealthCounterDisplayMode", 0);
        }

        public static Config Instance
        {
            get => s_lazy.Value;
        }

        public string SelectedHud
        {
            get => _selectedHudEntry.Value;
            set => _selectedHudEntry.Value = value;
        }

        public bool HudEnabled
        {
            get => _hudEnabledEntry.Value;
            set => _hudEnabledEntry.Value = value;
        }

        public bool OverrideCombinedAmmoCounter
        {
            get => _overrideCombinedAmmoCounterEntry.Value;
            set => _overrideCombinedAmmoCounterEntry.Value = value;
        }

        public HudConfigSO.CombinedAmmoCounterConfig CombinedAmmoCounter
        {
            get => (HudConfigSO.CombinedAmmoCounterConfig)_combinedAmmoCounterEntry.Value;
            set => _combinedAmmoCounterEntry.Value = (ushort)value;
        }

        public bool OverrideHealthCounterDisplayMode
        {
            get => _overrideHealthCounterDisplayModeEntry.Value;
            set => _overrideHealthCounterDisplayModeEntry.Value = value;
        }

        public HudConfigSO.HealthCounterConfig HealthCounterDisplayMode
        {
            get => (HudConfigSO.HealthCounterConfig)_healthCounterDisplayModeEntry.Value;
            set => _healthCounterDisplayModeEntry.Value = (ushort)value;
        }

        public void SaveConfig()
        {
            _config.SaveToFile();
        }

        public void SaveOverrides()
        {
            _hudOverride.SaveToFile();
        }
    }
}
