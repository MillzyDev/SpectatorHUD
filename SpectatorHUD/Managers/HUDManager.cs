using Ninject;
using UnityEngine;

namespace SpectatorHUD.Managers {
  public class HUDManager : MonoBehaviour {
    private HUDAssetContainer _hudAssetContainer = null!;
    private HUDValueManager _hudValueManager = null!;

    [Inject]
    public void Inject(HUDAssetContainer hudAssetContainer, HUDValueManager hudValueManager) {
      _hudAssetContainer = hudAssetContainer;
      _hudValueManager = hudValueManager;
    }

    private void Start() { }
  }
}
