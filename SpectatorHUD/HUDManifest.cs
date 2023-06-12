using Newtonsoft.Json;

namespace SpectatorHUD;

public class HUDManifest
{
#pragma warning disable CS8618
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("author")]
    public string Author { get; set; }
    [JsonProperty("version")]
    public string Version { get; set; }
    [JsonProperty("prefab_asset")]
    public string PrefabAsset { get; set; }
#pragma warning restore CS8618
}