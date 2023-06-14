using UnityEngine;

namespace SpectatorHUD {
  public class HUDAsset {
    public HUDManifest HUDManifest;
    public GameObject HUDRoot;

    public HUDAsset(HUDManifest hudManifest, GameObject hudRoot) {
      HUDManifest = hudManifest;
      HUDRoot = hudRoot;
    }
  }
}
