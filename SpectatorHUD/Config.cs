using MelonLoader;
using MelonLoader.Utils;

namespace SpectatorHUD
{
    public sealed class Config
    {
        private static readonly Lazy<Config> Lazy = new(() => new Config());

        private readonly MelonPreferences_Category _config;
        private readonly MelonPreferences_Entry<string?> _hud; 
        
        private Config()
        {
            this._config = MelonPreferences.CreateCategory("Config");
            
            // Add entries
            this._hud = this._config.CreateEntry<string?>("hud", null);
            
            this._config.SetFilePath(Path.Join(MelonEnvironment.UserDataDirectory, "SpectatorHUD", "config.cfg"));
            this._config.LoadFromFile();
        }

        public static Config Instance => Lazy.Value;

        public string? Hud => this._hud.Value;

        public void Save()
        {
            this._config.SaveToFile();
        }
    }
}