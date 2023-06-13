using System.IO;
using MelonLoader;
using Newtonsoft.Json;

namespace SpectatorHUD;

public class Config
{
    // ReSharper disable once InconsistentNaming
    public static readonly string HUDsDirectoryPath =
        Path.Combine(MelonUtils.UserDataDirectory, "SpectatorHUD", "HUDs");

    private static readonly string ConfigPath = Path.Combine(MelonUtils.UserDataDirectory, "SpectatorHUD", "config.json");

    [JsonProperty("selected_hud")] 
    public string SelectedHud { get; set; } = "alyxhud.hud";
    
    public static Config Load()
    {
        if (!File.Exists(ConfigPath))
        {
            var defaultConfig = new Config();
            var json = JsonConvert.SerializeObject(defaultConfig, Formatting.Indented);
            File.WriteAllText(ConfigPath, json);
            return defaultConfig;
        }

        var file = File.ReadAllText(ConfigPath);
        var config = JsonConvert.DeserializeObject<Config>(file);
        return config!;
    }

    public void Save()
    {
        var json = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(ConfigPath, json);
    }
}