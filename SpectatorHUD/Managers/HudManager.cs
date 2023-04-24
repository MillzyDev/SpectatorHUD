using System;
using MelonLoader;
using Ninject;
using SLZ.Bonelab;
using SLZ.Rig;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace SpectatorHUD.Managers;

[RegisterTypeInIl2Cpp]
public class HudManager : MonoBehaviour
{
    private BonelabGameControl? _bonelabGameControl;
    private AssetManager<GameObject>? _uiAssetManager;

    private GameObject? _uiAsset;

    private RigManager? _playerRigManager;
    private Health? _health;
    
    public HudManager(IntPtr ptr) : base(ptr) {}

    [Inject]
    [HideFromIl2Cpp]
    public void Inject(BonelabGameControl bonelabGameControl, AssetManager<GameObject> uiAssetManager)
    {
        _bonelabGameControl = bonelabGameControl;
        _uiAssetManager = uiAssetManager;
    }

    private void Start()
    {
        _uiAsset = Instantiate(_uiAssetManager!.Asset);
        
        _playerRigManager = _bonelabGameControl!.PlayerRigManager;
        _health = _playerRigManager.health;
    }

    private void Update()
    {
    
    }
}