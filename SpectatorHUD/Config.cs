using System.IO;
using MelonLoader;
using Newtonsoft.Json;

namespace SpectatorHUD
{
    public class Config
    {
        // ReSharper disable once InconsistentNaming
        public static readonly string HUDsDirectoryPath =
            Path.Combine(MelonUtils.UserDataDirectory, "SpectatorHUD", "HUDs");

        private static readonly string _sConfigPath =
            Path.Combine(MelonUtils.UserDataDirectory, "SpectatorHUD", "config.json");

        [JsonProperty("selected_hud")]
        public string SelectedHud { get; set; } = "alyxhud.hud";

        public static Config Load()
        {
            if (!File.Exists(_sConfigPath))
            {
                var defaultConfig = new Config();
                string json = JsonConvert.SerializeObject(defaultConfig, Formatting.Indented);
                File.WriteAllText(_sConfigPath, json);
                return defaultConfig;
            }

            string file = File.ReadAllText(_sConfigPath);
            var config = JsonConvert.DeserializeObject<Config>(file);
            return config!;
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(_sConfigPath, json);
        }
    }
}
