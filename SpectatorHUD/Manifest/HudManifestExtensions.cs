namespace SpectatorHUD.Manifest;

public static class HudManifestExtensions
{
    extension(HudV1 hudV1)
    {
        public IHudManifest AsManifest() => new HudManifestV1(hudV1);
    }

    extension(HudV2 hudV2)
    {
        public IHudManifest AsManifest() => new HudManifestV2(hudV2);
    }
}